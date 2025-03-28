using AutoMapper;
using FitnessClub.BLL.Services.Interfaces;
using FitnessClub.DAL.Models;
using FitnessClub.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FitnessClub.BLL.Services
{
    /// <summary>
    /// Базовий клас для сервісів, які використовують AutoMapper для маппінгу
    /// </summary>
    /// <typeparam name="TEntity">Тип сутності</typeparam>
    /// <typeparam name="TDto">Тип DTO</typeparam>
    public abstract class AutoMapperGenericService<TEntity, TDto> : IBaseService<TEntity, TDto>
        where TEntity : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        protected AutoMapperGenericService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Предикат для пошуку сутності за ідентифікатором
        /// </summary>
        protected abstract Expression<Func<TEntity, bool>> GetByIdPredicate(int id);

        /// <summary>
        /// Отримати запит для подальшої обробки
        /// </summary>
        protected virtual IQueryable<TEntity> GetQuery()
        {
            return _unitOfWork.GetRepository<TEntity>().GetAll();
        }

        /// <summary>
        /// Застосувати включення пов'язаних сутностей
        /// </summary>
        protected virtual IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query)
        {
            return query; // За замовчуванням нічого не включаємо
        }

        /// <summary>
        /// Отримати всі сутності
        /// </summary>
        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var repository = _unitOfWork.GetRepository<TEntity>();
            var query = ApplyIncludes(repository.GetAll());
            var entities = await query.ToListAsync();
            return await _mapper.MapCollectionAsync<TEntity, TDto>(entities);
        }

        /// <summary>
        /// Отримати сутність за ідентифікатором
        /// </summary>
        public virtual async Task<TDto> GetByIdAsync(int id)
        {
            var repository = _unitOfWork.GetRepository<TEntity>();
            var query = ApplyIncludes(repository.Find(GetByIdPredicate(id)));
            var entity = await query.FirstOrDefaultAsync();
            
            if (entity == null)
                return default;
            
            return await _mapper.MapAsync<TEntity, TDto>(entity);
        }

        /// <summary>
        /// Отримати сутність за умовою
        /// </summary>
        public virtual async Task<TDto> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var repository = _unitOfWork.GetRepository<TEntity>();
            var query = ApplyIncludes(repository.Find(predicate));
            var entity = await query.FirstOrDefaultAsync();
            
            if (entity == null)
                return default;
            
            return await _mapper.MapAsync<TEntity, TDto>(entity);
        }

        /// <summary>
        /// Отримати список сутностей за умовою
        /// </summary>
        public virtual async Task<IEnumerable<TDto>> GetManyByConditionAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var repository = _unitOfWork.GetRepository<TEntity>();
            var query = ApplyIncludes(repository.Find(predicate));
            var entities = await query.ToListAsync();
            return await _mapper.MapCollectionAsync<TEntity, TDto>(entities);
        }

        /// <summary>
        /// Конвертувати список сутностей у список DTO
        /// </summary>
        protected async Task<List<TDto>> MapToListAsync<TSource>(IEnumerable<TSource> entities) where TSource : class
        {
            var dtos = await _mapper.MapCollectionAsync<TSource, TDto>(entities);
            return dtos.ToList();
        }

        /// <summary>
        /// Створити нову сутність
        /// </summary>
        public virtual async Task<TDto> CreateAsync(TDto dto)
        {
            var entity = await _mapper.MapAsync<TDto, TEntity>(dto);
            var repository = _unitOfWork.GetRepository<TEntity>();
            
            await repository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            // Завантажуємо щойно створену сутність з усіма зв'язками
            var query = ApplyIncludes(repository.Find(GetByIdPredicate(GetEntityId(entity))));
            var createdEntity = await query.FirstOrDefaultAsync();
            
            return await _mapper.MapAsync<TEntity, TDto>(createdEntity ?? entity);
        }

        /// <summary>
        /// Отримати ідентифікатор сутності
        /// </summary>
        protected virtual int GetEntityId(TEntity entity)
        {
            // За замовчуванням припускаємо, що у сутності є властивість Id
            var idProperty = typeof(TEntity).GetProperty("Id");
            return (int)idProperty?.GetValue(entity);
        }

        /// <summary>
        /// Оновити існуючу сутність
        /// </summary>
        public virtual async Task UpdateAsync(TDto dto)
        {
            var entity = await _mapper.MapAsync<TDto, TEntity>(dto);
            var repository = _unitOfWork.GetRepository<TEntity>();
            
            repository.Update(entity);
            await _unitOfWork.CompleteAsync();
        }

        /// <summary>
        /// Видалити сутність за ідентифікатором
        /// </summary>
        public virtual async Task DeleteAsync(int id)
        {
            var repository = _unitOfWork.GetRepository<TEntity>();
            var entity = await repository.Find(GetByIdPredicate(id)).FirstOrDefaultAsync();
            
            if (entity != null)
            {
                repository.Remove(entity);
                await _unitOfWork.CompleteAsync();
            }
        }

        /// <summary>
        /// Перевірити існування сутності за умовою
        /// </summary>
        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var repository = _unitOfWork.GetRepository<TEntity>();
            return await repository.ExistsAsync(predicate);
        }

        /// <summary>
        /// Підрахувати кількість сутностей за умовою
        /// </summary>
        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            var repository = _unitOfWork.GetRepository<TEntity>();
            return await repository.CountAsync(predicate);
        }
    }
} 