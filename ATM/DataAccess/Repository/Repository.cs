using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.Repository
{
    internal class Repository<T> : IRepository<T> where T : class, IEntity
    {
        //protected readonly DbContext DbContext;
        protected readonly DbSet<T> DataSet;

        public Repository(DbContext dbContext)
        {
          //  DbContext = dbContext;
            DataSet = dbContext.Set<T>();
        }

        public IQueryable<T> Query()
        {
            return DataSet;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DataSet.ToListAsync();
        }

        public void Add(T entity)
        {
            DataSet.Add(entity);
        }

        public void Delete(T entity)
        {
            DataSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> expression)
        {
            return await DataSet.Where(expression).ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await DataSet.SingleOrDefaultAsync(expression);
        }

        public void Update(T entity)
        {
            DataSet.Update(entity);
        }
    }
}