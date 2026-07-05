using GoBite.Domain.Entities;
using GoBite.Domain.ProductEntities;

namespace GoBite.Domain.CommentEntities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public string UserId { get; set; } = string.Empty;

        public ApplicationUser User { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
