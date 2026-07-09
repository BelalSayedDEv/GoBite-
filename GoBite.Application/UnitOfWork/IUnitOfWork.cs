using GoBite.Application.Interfaces.Rrepository;

namespace GoBite.Application.UnitOfWork
{
    public interface IUnitOfWork
    {
        public ICartRepository Cart { get; }
        public ICartItemRepository CartItem { get; }
        public Task SaveAsync();
    }
}
