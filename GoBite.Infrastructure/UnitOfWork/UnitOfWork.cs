using GoBite.Application.Interfaces.Rrepository;
using GoBite.Application.UnitOfWork;
using GoBite.Infrastructure.Data;

namespace GoBite.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GoBiteDbContext context;

        public ICartRepository Cart { get; }




        public UnitOfWork(GoBiteDbContext context, ICartRepository Cart)
        {
            this.context = context;

            this.Cart = Cart;
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
