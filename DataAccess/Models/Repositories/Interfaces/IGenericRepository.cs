using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebMessangerAPI.DataAccess.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<T?>> GetAllAsync<T>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<T>> select = null,
            string includeProperties = "");

        Task<T?> GetByIdAsync<T>(object id, string EntityId = "Id",
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IQueryable<T>> select = null,
            string includeProperties = "");
        Task<TEntity> InsertAsync(TEntity entity);
        void Update(TEntity entityToUpdate);
        Task<bool> DeleteAsync(object id);
        Task<bool> EntityExistAsync(object id);
    }
}