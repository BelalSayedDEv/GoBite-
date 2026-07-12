using GoBite.Domain.CommentEntities;

namespace GoBite.Application.Interfaces.Rrepository;

public interface ICommentRepository
{
    Task<Comment?> GetByIdAsync(int id);
    Task<List<Comment>> GetByProductIdAsync(int productId);
    Task<List<Comment>> GetByUserIdAsync(string userId);
    Task AddAsync(Comment comment);
    Task DeleteAsync(Comment comment);
    Task<bool> ExistsAsync(int id);
}
