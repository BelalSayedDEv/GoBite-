using GoBite.Domain.CartEntities;

namespace GoBite.Application.Interfaces.Rrepository
{
    public interface ICartRepository
    {
        public Task<Cart?> GetCartByUserId(string UserId);

        public Task AddCart(Cart cart);
    }
}
