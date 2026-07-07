using GoBite.Application.Interfaces.Rrepository;
using GoBite.Domain.Entities;
using GoBite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoBite.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly GoBiteDbContext context;

    public AuthRepository(GoBiteDbContext context)
    {
        this.context = context;
    }

    public async Task AddRefreshToken(RefreshToken refreshToken)
    {
        await context.RefreshTokens.AddAsync(refreshToken);
    }

    public async Task<RefreshToken?> GetTokenByToken(string token)
    {
        return await context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token);
    }

    public async Task AddOtpCode(OtpCode otpCode)
    {
        await context.OtpCodes.AddAsync(otpCode);
    }

    public async Task<OtpCode?> GetValidOtp(string userId, string code)
    {
        return await context.OtpCodes
            .Where(o => o.UserId == userId
                     && o.Code == code
                     && !o.IsUsed
                     && o.ExpiresAt > DateTime.UtcNow)
            .OrderByDescending(o => o.CreatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task RevokeAllForUser(string userId)
    {
        var tokens = await context.RefreshTokens
            .Where(rt => rt.UserId == userId && !rt.IsRevoked)
            .ToListAsync();

        foreach (var token in tokens)
            token.IsRevoked = true;
    }

    public async Task Save()
    {
        await context.SaveChangesAsync();
    }
}
