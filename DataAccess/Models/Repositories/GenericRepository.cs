using WebMessangerAPI.DataAccess.Repositories.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebMessangerAPI.DataAccess.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly MessangerContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public GenericRepository(MessangerContext context)
        {
            this._context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<T>> select = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                query = orderBy(query);

            if (select == null)
                return await query.Cast<T>().ToListAsync();

            return await select(query).ToListAsync();
        }

        //public async Task<TEntity> GetByIdAsync(object id)
        //{
        //    return await _dbSet.FindAsync(id);
        //}
        public async Task<T> GetByIdAsync<T>(object id,
            Func<IQueryable<TEntity>, IQueryable<T>> select, // projection required
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            // Додаємо включення пов'язаних сутностей, якщо потрібно
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            // Фільтруємо за первинним ключем
            query = query.Where(e => EF.Property<object>(e, "Id").Equals(id));

            //if (select != null)
            //    // Проєкція
            //    return await select(query).FirstOrDefaultAsync();
            //else
            //    return (T)(object)await query.FirstOrDefaultAsync();


            if (select == null)
                return await query.Cast<T>().FirstAsync();

            return await select(query).FirstOrDefaultAsync();
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(object id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Remove(entity);
        }

        public async Task DeleteAsync(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public async Task UpdateAsync(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async Task<bool> EntityExistAsync(object id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return false;
            else
                return true;
        }
    }
}