using GoBite.Application.Interfaces.Rrepository;
using GoBite.Domain.CommentEntities;
using GoBite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoBite.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly GoBiteDbContext context;

    public CommentRepository(GoBiteDbContext context)
    {
        this.context = context;
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await context.Set<Comment>().FindAsync(id);
    }

    public async Task<List<Comment>> GetByProductIdAsync(int productId)
    {
        return await context.Set<Comment>()
            .Where(c => c.ProductId == productId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<Comment>> GetByUserIdAsync(string userId)
    {
        return await context.Set<Comment>()
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task AddAsync(Comment comment)
    {
        await context.Set<Comment>().AddAsync(comment);
    }

    public Task DeleteAsync(Comment comment)
    {
        context.Set<Comment>().Remove(comment);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await context.Set<Comment>().AnyAsync(c => c.Id == id);
    }
}
