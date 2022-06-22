using Sample.Infrastructure.Persistence.Extensions;
using Sample.SharedKernel.SeedWork;
using Sample.SharedKernel.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Infrastructure.Persistence
{
    public class EfRepositoryBase<TEntity, Tkey> : IRepository<TEntity, Tkey> where TEntity : Entity<Tkey>, IAggregateRoot
    {
        protected readonly SampleDbContext DbContext;

        protected virtual IQueryable<TEntity> ConfigureInclude(IQueryable<TEntity> query)
        {
            return query;
        }

        public EfRepositoryBase(SampleDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        }

        public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            DbContext.Set<TEntity>().Update(entity);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            EntityEntry<TEntity> entry = DbContext.Attach(entity);

            entry.CurrentValues["IsDeleted"] = true;
            entry.CurrentValues["DeletedAt"] = DateTimeOffset.Now;

            DbContext.Update(entity);

            return Task.CompletedTask;
        }

        public Task<TEntity> GetAsync(Tkey id, CancellationToken cancellationToken)
        {
            return DbContext.Set<TEntity>()
                .AsTracking()
                .Apply(ConfigureInclude)
                .SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        }
    }

    public class RepositoryBase<TEntity> : EfRepositoryBase<TEntity, Guid> where TEntity : Entity<Guid>, IAggregateRoot
    {
        public RepositoryBase(SampleDbContext dbContext) : base(dbContext)
        {
        }
    }
}
