using GoBite.Domain.Entities;

namespace GoBite.Application.Interfaces;

public interface IAuthRepository
{
    Task AddRefreshToken(RefreshToken refreshToken);
    Task<RefreshToken?> GetTokenByToken(string token);
    Task AddOtpCode(OtpCode otpCode);
    Task<OtpCode?> GetValidOtp(string userId, string code);
    Task RevokeAllForUser(string userId);
    Task Save();
}
