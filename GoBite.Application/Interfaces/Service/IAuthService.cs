using Ardalis.Result;
using GoBite.Application.DTOs.Auth;

namespace GoBite.Application.Interfaces.Service;

public interface IAuthService
{
    Task<Result<AuthResponse>> Register(RegisterRequest request);
    Task<Result<AuthResponse>> Login(LoginRequest request);
    Task<Result<AuthResponse>> Refresh(string userId, RefreshTokenRequest request);
    Task<Result<AuthResponse>> ForgotPassword(ForgotPasswordRequest request);
    Task<Result<AuthResponse>> VerifyOtp(VerifyOtpRequest request);
    Task<Result<AuthResponse>> ResetPassword(ResetPasswordRequest request);
    Task Logout(string userId);
}
