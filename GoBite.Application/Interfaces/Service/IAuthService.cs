using GoBite.Application.Contracts;
using GoBite.Application.DTOs.Auth;

namespace GoBite.Application.Interfaces.Service;

public interface IAuthService
{
    Task<AuthResult> Register(RegisterRequest request);
    Task<AuthResult> Login(LoginRequest request);
    Task<AuthResult> Refresh(string userId, RefreshTokenRequest request);
    Task<AuthResult> ForgotPassword(ForgotPasswordRequest request);
    Task<AuthResult> VerifyOtp(VerifyOtpRequest request);
    Task<AuthResult> ResetPassword(ResetPasswordRequest request);
    Task Logout(string userId);
}
