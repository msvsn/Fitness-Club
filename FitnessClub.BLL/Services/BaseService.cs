using FitnessClub.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FitnessClub.BLL.Services
{
    public abstract class BaseService<TEntity> where TEntity : class
    {
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected virtual IQueryable<TEntity> GetQuery()
        {
            return _unitOfWork.GetRepository<TEntity>().GetAll();
        }

        protected virtual async Task<IEnumerable<TEntity>> GetAllEntitiesAsync()
        {
            return await _unitOfWork.GetRepository<TEntity>().GetAllAsync();
        }

        protected virtual async Task<TEntity> GetEntityByIdAsync(int id)
        {
            return await _unitOfWork.GetRepository<TEntity>().GetByIdAsync(id);
        }

        protected virtual async Task<TEntity> GetEntityByConditionAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _unitOfWork.GetRepository<TEntity>().FirstOrDefaultAsync(predicate);
        }

        protected virtual async Task<IEnumerable<TEntity>> GetEntitiesByConditionAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _unitOfWork.GetRepository<TEntity>().FindAsync(predicate);
        }

        protected virtual async Task AddEntityAsync(TEntity entity)
        {
            await _unitOfWork.GetRepository<TEntity>().AddAsync(entity);
            await _unitOfWork.CompleteAsync();
        }

        protected virtual async Task UpdateEntityAsync(TEntity entity)
        {
            _unitOfWork.GetRepository<TEntity>().Update(entity);
            await _unitOfWork.CompleteAsync();
        }

        protected virtual async Task RemoveEntityAsync(TEntity entity)
        {
            _unitOfWork.GetRepository<TEntity>().Remove(entity);
            await _unitOfWork.CompleteAsync();
        }

        protected virtual async Task<bool> EntityExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _unitOfWork.GetRepository<TEntity>().ExistsAsync(predicate);
        }

        protected virtual async Task<int> CountEntitiesAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await _unitOfWork.GetRepository<TEntity>().CountAsync(predicate);
        }
    }
} 