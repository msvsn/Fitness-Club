using AutoMapper;
using System;
using System.Linq.Expressions;

namespace FitnessClub.BLL.Services
{
    /// <summary>
    /// Базовий клас для маппінгу сутностей за допомогою AutoMapper
    /// </summary>
    /// <typeparam name="TEntity">Тип сутності</typeparam>
    /// <typeparam name="TDto">Тип DTO</typeparam>
    public class AutoMapperEntityMapper<TEntity, TDto> : IEntityMapper<TEntity, TDto>
        where TEntity : class
    {
        protected readonly IMapper _mapper;

        public AutoMapperEntityMapper(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Перетворення з сутності в DTO
        /// </summary>
        public virtual TDto MapToDto(TEntity entity)
        {
            return entity != null ? _mapper.Map<TDto>(entity) : default;
        }

        /// <summary>
        /// Перетворення з DTO в сутність
        /// </summary>
        public virtual TEntity MapFromDto(TDto dto)
        {
            return dto != null ? _mapper.Map<TEntity>(dto) : default;
        }

        /// <summary>
        /// Оновлення сутності з даних DTO
        /// </summary>
        public virtual void UpdateEntity(TEntity entity, TDto dto)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            _mapper.Map(dto, entity);
        }
    }
} 