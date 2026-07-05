using GoBite.Application.Contracts;
using GoBite.Application.DTOs.Auth;
using GoBite.Application.Interfaces;
using GoBite.Presentation.Model;
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

        if (result.Outcome == AuthOutcome.EmailAlreadyExists)
            return Conflict(ApiResponse<object>.Failure(result.Message!));

        if (result.Outcome == AuthOutcome.Unauthorized)
            return BadRequest(ApiResponse<object>.Failure(result.Message!, result.Errors));

        return Created("", ApiResponse<AuthResponse>.Success(result.Data));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await authService.Login(request);

        if (result.Outcome == AuthOutcome.Unauthorized)
            return Unauthorized(ApiResponse<object>.Failure(result.Message!));

        if (result.Outcome == AuthOutcome.Forbidden)
            return StatusCode(403, ApiResponse<object>.Failure(result.Message!));

        return Ok(ApiResponse<AuthResponse>.Success(result.Data));
    }



    [Authorize]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await authService.Refresh(userId, request);

        switch (result.Outcome)
        {
            case AuthOutcome.TokenNotFound:
            case AuthOutcome.UserNotOwnToken:
                return NotFound(ApiResponse<object>.Failure(result.Message!));

            case AuthOutcome.TokenUsed:
            case AuthOutcome.TokenRevoked:
            case AuthOutcome.TokenExpired:
                return Conflict(ApiResponse<object>.Failure(result.Message!));
        }

        return Ok(ApiResponse<AuthResponse>.Success(result.Data));
    }



    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
    {
        var result = await authService.ForgotPassword(request);

        if (result.Outcome == AuthOutcome.EmailNotFound)
            return NotFound(ApiResponse<object>.Failure(result.Message!));

        return Ok(ApiResponse<object>.Success(null, result.Message));
    }




    [HttpPost("verify-otp")]
    public async Task<IActionResult> VerifyOtp(VerifyOtpRequest request)
    {
        var result = await authService.VerifyOtp(request);

        if (result.Outcome == AuthOutcome.EmailNotFound)
            return NotFound(ApiResponse<object>.Failure(result.Message!));

        if (result.Outcome == AuthOutcome.InvalidOtp)
            return BadRequest(ApiResponse<object>.Failure(result.Message!));

        return Ok(ApiResponse<object>.Success(null, result.Message));
    }




    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
        var result = await authService.ResetPassword(request);

        if (result.Outcome == AuthOutcome.EmailNotFound)
            return NotFound(ApiResponse<object>.Failure(result.Message!));

        if (result.Outcome == AuthOutcome.InvalidResetToken)
            return BadRequest(ApiResponse<object>.Failure(result.Message!, result.Errors));

        return Ok(ApiResponse<object>.Success(null, result.Message));
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
