namespace FitnessClub.BLL.Services
{
    /// <summary>
    /// Інтерфейс для перетворення між сутностями та DTO
    /// </summary>
    /// <typeparam name="TEntity">Тип сутності</typeparam>
    /// <typeparam name="TDto">Тип DTO</typeparam>
    public interface IEntityMapper<TEntity, TDto>
        where TEntity : class
    {
        /// <summary>
        /// Перетворення з сутності в DTO
        /// </summary>
        TDto MapToDto(TEntity entity);

        /// <summary>
        /// Перетворення з DTO в сутність
        /// </summary>
        TEntity MapFromDto(TDto dto);

        /// <summary>
        /// Оновлення сутності з даних DTO
        /// </summary>
        void UpdateEntity(TEntity entity, TDto dto);
    }
} 