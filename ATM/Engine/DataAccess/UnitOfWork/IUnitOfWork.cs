using System;
using System.Threading.Tasks;
using Engine.DataAccess.Repository;
using Models;

namespace Engine.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T: class,IEntity;
        Task SaveChangesAsync();
    }
}