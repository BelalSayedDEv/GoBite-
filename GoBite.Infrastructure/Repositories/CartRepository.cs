using GoBite.Application.Interfaces.Rrepository;
using GoBite.Domain.CartEntities;
using GoBite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoBite.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly GoBiteDbContext context;

        public CartRepository(GoBiteDbContext context)
        {
            this.context = context;
        }
        public async Task AddCart(Cart cart)
        {
            await context.Carts.AddAsync(cart);
        }

        public async Task<Cart?> GetCartByUserId(string UserId)
        {
            return await context.Carts.FirstOrDefaultAsync(c => c.UserId == UserId);
        }
    }
}
