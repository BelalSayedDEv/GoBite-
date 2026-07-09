using GoBite.Application.Interfaces.Rrepository;
using GoBite.Domain.CartEntities;

namespace GoBite.Infrastructure.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        public Task AddAddonItemAsync(CartItemAddon addon)
        {
            throw new NotImplementedException();
        }

        public Task AddCartItemAsync(CartItem cartItem)
        {
            throw new NotImplementedException();
        }

        public Task AddCartItemIngrediantToCartItemIngrediant(int IngrediantId)
        {
            throw new NotImplementedException();
        }

        public Task<CartItemAddon?> GetCartItemAddonAsync(int CartItemAddonId)
        {
            throw new NotImplementedException();
        }

        public Task<CartItem?> GetCartItemAsync(int CartItemId)
        {
            throw new NotImplementedException();
        }

        public Task<List<CartItem>> GetCartItemsAsync(int CartId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAddonItem(int CartItemAddonId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveCartItem(int CartItemId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveItemRemovedIngrediant(int CartItemRemovedId)
        {
            throw new NotImplementedException();
        }
    }
}
