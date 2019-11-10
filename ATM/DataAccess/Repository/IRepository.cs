using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Models;

namespace DataAccess.Repository
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Query();
        void Add(T entity);
        void Delete(T entity);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        void Update(T entity);
    }
}