using Application.Interfaces.GenericRepository;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Implementations.Repositories
{
    public class GenericRepository(ApplicationDbContext _dbContext) : IGenericRepository
    {
        public async Task<IEnumerable<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "") where TEntity : class
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        public async Task<int> InsertAsync<TEntity>(TEntity entity) where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _dbContext.Set<TEntity>().AddAsync(entity);

            await _dbContext.SaveChangesAsync();

            var ret = 0;

            var key = typeof(TEntity).GetProperties().FirstOrDefault(p =>
                p.CustomAttributes.Any(attr =>
                    attr.AttributeType == typeof(KeyAttribute)));

            if (key != null)
            {
                ret = (int)(key.GetValue(entity, null) ?? 0);
            }

            return ret;
        }

        public async Task UpdateAsync<TEntity>(TEntity entityToUpdate) where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(entityToUpdate);

            await _dbContext.SaveChangesAsync();
        }

    }
}
