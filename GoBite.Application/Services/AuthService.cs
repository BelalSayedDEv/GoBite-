using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Ardalis.Result;
using GoBite.Application.DTOs.Auth;
using GoBite.Application.Interfaces.Rrepository;
using GoBite.Application.Interfaces.Service;
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

    public async Task<Result<AuthResponse>> Register(RegisterRequest request)
    {
        var existingUser = await userManager.FindByEmailAsync(request.Email);

        if (existingUser != null)
            return Result<AuthResponse>.Conflict();

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

        var identityResult = await userManager.CreateAsync(user, request.Password);

        if (!identityResult.Succeeded)
        {
            return Result<AuthResponse>.Error(
                string.Join(", ", identityResult.Errors.Select(e => e.Description)));
        }

        return await GenerateAuthResponse(user);
    }

    public async Task<Result<AuthResponse>> Login(LoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Result<AuthResponse>.Unauthorized();

        if (!user.IsActive)
            return Result<AuthResponse>.Forbidden();

        var found = await userManager.CheckPasswordAsync(user, request.Password);
        if (!found)
            return Result<AuthResponse>.Unauthorized();

        return await GenerateAuthResponse(user);
    }

    public async Task<Result<AuthResponse>> Refresh(string userId, RefreshTokenRequest request)
    {
        var storedToken = await authRepository.GetTokenByToken(request.RefreshToken);

        if (storedToken == null)
            return Result<AuthResponse>.NotFound();

        if (storedToken.UserId != userId)
            return Result<AuthResponse>.NotFound();

        if (storedToken.IsUsed)
            return Result<AuthResponse>.Conflict();

        if (storedToken.IsRevoked)
            return Result<AuthResponse>.Conflict();

        if (DateTime.UtcNow > storedToken.ExpiresAt)
            return Result<AuthResponse>.Conflict();

        storedToken.IsUsed = true;
        storedToken.IsRevoked = true;

        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return Result<AuthResponse>.Unauthorized();

        return await GenerateAuthResponse(user);
    }

    public async Task<Result<AuthResponse>> ForgotPassword(ForgotPasswordRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Result<AuthResponse>.NotFound();

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

        return Result<AuthResponse>.Success(null!);
    }

    public async Task<Result<AuthResponse>> VerifyOtp(VerifyOtpRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Result<AuthResponse>.NotFound();

        var validOtp = await authRepository.GetValidOtp(user.Id, request.Otp);
        if (validOtp == null)
            return Result<AuthResponse>.Error("Invalid or expired OTP");

        var resetToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));

        validOtp.IsUsed = true;
        validOtp.ResetToken = resetToken;
        await authRepository.Save();

        return Result<AuthResponse>.Success(new AuthResponse
        {
            AccessToken = resetToken,
        });
    }

    public async Task<Result<AuthResponse>> ResetPassword(ResetPasswordRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Result<AuthResponse>.NotFound();

        var identityResetToken = await userManager.GeneratePasswordResetTokenAsync(user);
        var result = await userManager.ResetPasswordAsync(user, identityResetToken, request.NewPassword);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return Result<AuthResponse>.Error(string.Join(", ", errors));
        }

        return Result<AuthResponse>.Success(null!);
    }

    public async Task Logout(string userId)
    {
        await authRepository.RevokeAllForUser(userId);
        await authRepository.Save();
    }

    private async Task<Result<AuthResponse>> GenerateAuthResponse(ApplicationUser user)
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

        return Result<AuthResponse>.Success(new AuthResponse
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = refreshToken.Token,
            Expiration = DateTime.UtcNow.AddMinutes(15),
        });
    }

    private static string GenerateOtp()
    {
        return (RandomNumberGenerator.GetInt32(100000, 999999)).ToString();
    }
}
