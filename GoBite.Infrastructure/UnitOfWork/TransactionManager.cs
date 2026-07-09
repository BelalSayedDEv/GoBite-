using GoBite.Application.UnitOfWork;
using GoBite.Infrastructure.Data;

namespace GoBite.Infrastructure.UnitOfWork
{
    public class TransactionManager : ITransactionManager
    {
        private readonly GoBiteDbContext context;

        public TransactionManager(GoBiteDbContext context)
        {
            this.context = context;
        }
        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            await context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await context.Database.CommitTransactionAsync(cancellationToken);
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            await context.Database.RollbackTransactionAsync(cancellationToken);
        }
    }
}
