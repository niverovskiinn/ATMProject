using System.Threading.Tasks;
using Engine.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Engine.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _db;

        public UnitOfWork(DbContext dbContext)
        {
            _db = dbContext;
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public IRepository<T> Repository<T>() where T : class, IEntity
        {
            return new Repository<T>(_db);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}