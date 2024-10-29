using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories
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

        public virtual async Task<IEnumerable<T?>> GetAllAsync<T>(
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

        public virtual async Task<T?> GetByIdAsync<T>(object id, string EntityId = "Id",
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IQueryable<T>> select = null,
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
            query = query.Where(e => EF.Property<object>(e, EntityId).Equals(id));

            //if (select != null)
            //    // Проєкція
            //    return await select(query).FirstOrDefaultAsync();
            //else
            //    return (T)(object)await query.FirstOrDefaultAsync();


            if (select == null)
                return await query.Cast<T?>().FirstOrDefaultAsync();


            return await select(query).FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual async Task<bool> DeleteAsync(object id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity != null)
            {
                _dbSet.Remove(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }



        public virtual async Task<bool> EntityExistAsync(object id)
        {
            var entity = await _dbSet.FindAsync(id);
            return (entity != null) ? true : false;
        }
    }
}