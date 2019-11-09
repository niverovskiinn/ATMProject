using System;
using System.Threading.Tasks;
using DataAccess.Repository;
using Models;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T: class,IEntity;
        Task SaveChangesAsync();
    }
}