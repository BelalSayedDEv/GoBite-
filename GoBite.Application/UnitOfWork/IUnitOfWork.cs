using GoBite.Application.Interfaces.Rrepository;

namespace GoBite.Application.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICartRepository Cart { get; }

        public Task SaveAsync();
    }
}
