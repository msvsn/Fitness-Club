using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FitnessClub.BLL.Services.Interfaces
{
    /// <summary>
    /// Базовий інтерфейс для всіх сервісів з CRUD операціями
    /// </summary>
    /// <typeparam name="TEntity">Тип сутності</typeparam>
    /// <typeparam name="TDto">Тип DTO</typeparam>
    public interface IBaseService<TEntity, TDto> where TEntity : class
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(int id);
        Task<TDto> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TDto>> GetManyByConditionAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TDto> CreateAsync(TDto dto);
        Task UpdateAsync(TDto dto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
    }
}