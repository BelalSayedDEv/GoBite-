using GoBite.Domain.CartEntities;

namespace GoBite.Application.Interfaces.Rrepository
{
    public interface ICartItemRepository
    {
        public Task<List<CartItem>> GetCartItemsAsync(int CartId);
        public Task<CartItem?> GetCartItemAsync(int CartItemId);
        public Task AddCartItemAsync(CartItem cartItem);
        public Task RemoveCartItem(int CartItemId);
        public Task AddAddonItemAsync(CartItemAddon addon);
        public Task RemoveAddonItem(int CartItemAddonId);
        public Task<CartItemAddon?> GetCartItemAddonAsync(int CartItemAddonId);
        public Task AddCartItemIngrediantToCartItemIngrediant(int IngrediantId);
        public Task RemoveItemRemovedIngrediant(int CartItemRemovedId);

    }
}
