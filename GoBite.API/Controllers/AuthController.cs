using Ardalis.Result;
using GoBite.API.Model;
using GoBite.Application.DTOs.Auth;
using GoBite.Application.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GoBite.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await authService.Register(request);

        if (result.Status == ResultStatus.Conflict)
            return Conflict(ApiResponse<object>.Failure("Email is already registered"));

        if (result.Status == ResultStatus.Error)
            return BadRequest(ApiResponse<object>.Failure(string.Join("; ", result.Errors)));

        return Created("", ApiResponse<AuthResponse>.Success(result.Value));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await authService.Login(request);

        if (result.Status == ResultStatus.Unauthorized)
            return Unauthorized(ApiResponse<object>.Failure("Invalid email or password"));

        if (result.Status == ResultStatus.Forbidden)
            return StatusCode(403, ApiResponse<object>.Failure("Account is deactivated"));

        return Ok(ApiResponse<AuthResponse>.Success(result.Value));
    }



    [Authorize]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await authService.Refresh(userId, request);

        if (result.Status == ResultStatus.NotFound)
            return NotFound(ApiResponse<object>.Failure("Token not found"));

        if (result.Status == ResultStatus.Conflict)
            return Conflict(ApiResponse<object>.Failure("Token conflict"));

        return Ok(ApiResponse<AuthResponse>.Success(result.Value));
    }



    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
    {
        var result = await authService.ForgotPassword(request);

        if (result.Status == ResultStatus.NotFound)
            return NotFound(ApiResponse<object>.Failure("Email not found"));

        return Ok(ApiResponse<object>.Success(null, "OTP sent to your email"));
    }



    [HttpPost("verify-otp")]
    public async Task<IActionResult> VerifyOtp(VerifyOtpRequest request)
    {
        var result = await authService.VerifyOtp(request);

        if (result.Status == ResultStatus.NotFound)
            return NotFound(ApiResponse<object>.Failure("Email not found"));

        if (result.Status == ResultStatus.Error)
            return BadRequest(ApiResponse<object>.Failure(string.Join("; ", result.Errors)));

        return Ok(ApiResponse<object>.Success(null, result.Value?.AccessToken));
    }



    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
        var result = await authService.ResetPassword(request);

        if (result.Status == ResultStatus.NotFound)
            return NotFound(ApiResponse<object>.Failure("Email not found"));

        if (result.Status == ResultStatus.Error)
            return BadRequest(ApiResponse<object>.Failure(string.Join("; ", result.Errors)));

        return Ok(ApiResponse<object>.Success(null, "Password reset successfully"));
    }



    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
            return Unauthorized();

        await authService.Logout(userId);

        return Ok(ApiResponse<object>.Success(null, "Logged out successfully"));
    }
}
