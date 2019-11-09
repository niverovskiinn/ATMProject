using System.Threading.Tasks;
using DataAccess.Repository;
using Models;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public IRepository<T> Repository<T>() where T : class, IEntity
        {
            throw new System.NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}