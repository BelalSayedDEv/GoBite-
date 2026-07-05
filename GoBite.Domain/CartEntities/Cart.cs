using GoBite.Domain.Entities;

namespace GoBite.Domain.CartEntities
{
    public class Cart
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation Properties
        public ApplicationUser User { get; set; } = null!;

        public ICollection<CartItem> CartItems { get; set; }
            = new List<CartItem>();
    }
}
