using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GoBite.Application.Contracts;
using GoBite.Application.DTOs.Auth;
using GoBite.Application.Interfaces;
using GoBite.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GoBite.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IAuthRepository authRepository;
    private readonly IEmailService emailService;
    private readonly IConfiguration configuration;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        IAuthRepository authRepository,
        IEmailService emailService,
        IConfiguration configuration)
    {
        this.userManager = userManager;
        this.authRepository = authRepository;
        this.emailService = emailService;
        this.configuration = configuration;
    }

    public async Task<AuthResult> Register(RegisterRequest request)
    {
        var existingUser = await userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
            return new AuthResult
            {
                Outcome = AuthOutcome.EmailAlreadyExists,
                Message = "Email is already registered"
            };

        var user = new ApplicationUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email,
            PhoneNumber = request.PhoneNumber,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return new AuthResult
            {
                Outcome = AuthOutcome.Unauthorized,
                Message = "Registration failed",
                Errors = errors
            };
        }

        return await GenerateAuthResponse(user);
    }

    public async Task<AuthResult> Login(LoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return new AuthResult
            {
                Outcome = AuthOutcome.Unauthorized,
                Message = "Invalid email or password"
            };

        if (!user.IsActive)
            return new AuthResult
            {
                Outcome = AuthOutcome.Forbidden,
                Message = "Account is deactivated"
            };

        var found = await userManager.CheckPasswordAsync(user, request.Password);
        if (!found)
            return new AuthResult
            {
                Outcome = AuthOutcome.Unauthorized,
                Message = "Invalid email or password"
            };

        return await GenerateAuthResponse(user);
    }

    public async Task<AuthResult> Refresh(string userId, RefreshTokenRequest request)
    {
        var storedToken = await authRepository.GetTokenByToken(request.RefreshToken);

        if (storedToken == null)
            return new AuthResult
            {
                Outcome = AuthOutcome.TokenNotFound,
                Message = "Token not found"
            };

        if (storedToken.UserId != userId)
            return new AuthResult
            {
                Outcome = AuthOutcome.UserNotOwnToken,
                Message = "Token not found"
            };

        if (storedToken.IsUsed)
            return new AuthResult
            {
                Outcome = AuthOutcome.TokenUsed,
                Message = "Token is used"
            };

        if (storedToken.IsRevoked)
            return new AuthResult
            {
                Outcome = AuthOutcome.TokenRevoked,
                Message = "Token is revoked"
            };

        if (DateTime.UtcNow > storedToken.ExpiresAt)
            return new AuthResult
            {
                Outcome = AuthOutcome.TokenExpired,
                Message = "Token is expired"
            };

        storedToken.IsUsed = true;
        storedToken.IsRevoked = true;

        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return new AuthResult
            {
                Outcome = AuthOutcome.Unauthorized,
                Message = "User not found"
            };

        return await GenerateAuthResponse(user);
    }

    public async Task<AuthResult> ForgotPassword(ForgotPasswordRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return new AuthResult
            {
                Outcome = AuthOutcome.EmailNotFound,
                Message = "Email not found"
            };

        var otp = GenerateOtp();

        var otpEntity = new OtpCode
        {
            UserId = user.Id,
            Code = otp,
            ExpiresAt = DateTime.UtcNow.AddMinutes(5),
            CreatedAt = DateTime.UtcNow
        };

        await authRepository.AddOtpCode(otpEntity);
        await authRepository.Save();

        await emailService.SendOtpEmailAsync(request.Email, otp);

        return new AuthResult
        {
            Outcome = AuthOutcome.Authorized,
            Message = "OTP sent to your email"
        };
    }

    public async Task<AuthResult> VerifyOtp(VerifyOtpRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return new AuthResult
            {
                Outcome = AuthOutcome.EmailNotFound,
                Message = "Email not found"
            };

        var validOtp = await authRepository.GetValidOtp(user.Id, request.Otp);
        if (validOtp == null)
            return new AuthResult
            {
                Outcome = AuthOutcome.InvalidOtp,
                Message = "Invalid or expired OTP"
            };

        var resetToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));

        validOtp.IsUsed = true;
        validOtp.ResetToken = resetToken;
        await authRepository.Save();

        return new AuthResult
        {
            Outcome = AuthOutcome.Authorized,
            Message = resetToken
        };
    }

    public async Task<AuthResult> ResetPassword(ResetPasswordRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return new AuthResult
            {
                Outcome = AuthOutcome.EmailNotFound,
                Message = "Email not found"
            };

        var identityResetToken = await userManager.GeneratePasswordResetTokenAsync(user);
        var result = await userManager.ResetPasswordAsync(user, identityResetToken, request.NewPassword);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return new AuthResult
            {
                Outcome = AuthOutcome.InvalidResetToken,
                Message = "Password reset failed",
                Errors = errors
            };
        }

        return new AuthResult
        {
            Outcome = AuthOutcome.Authorized,
            Message = "Password reset successfully"
        };
    }

    public async Task Logout(string userId)
    {
        await authRepository.RevokeAllForUser(userId);
        await authRepository.Save();
    }

    private async Task<AuthResult> GenerateAuthResponse(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("userId", user.Id),
        };

        var roles = await userManager.GetRolesAsync(user);
        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: credentials);

        var refreshToken = new RefreshToken
        {
            Token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64)),
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            UserId = user.Id,
        };

        await authRepository.AddRefreshToken(refreshToken);
        await authRepository.Save();

        return new AuthResult
        {
            Data = new AuthResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken.Token,
                Expiration = DateTime.UtcNow.AddMinutes(15),
            },
            Outcome = AuthOutcome.Authorized
        };
    }

    private static string GenerateOtp()
    {
        return (RandomNumberGenerator.GetInt32(100000, 999999)).ToString();
    }
}
