using System;
using System.Threading.Tasks;
using FitnessClub.DAL.Repositories;

namespace FitnessClub.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        Task<int> CompleteAsync();
    }
} 