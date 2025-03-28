using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.BLL.Services
{
    /// <summary>
    /// Розширення для роботи з AutoMapper
    /// </summary>
    public static class AutoMapperExtensions
    {
        /// <summary>
        /// Асинхронно маппить колекцію сутностей в колекцію DTO
        /// </summary>
        public static async Task<IEnumerable<TDestination>> MapCollectionAsync<TSource, TDestination>(
            this IMapper mapper,
            IEnumerable<TSource> source)
        {
            if (source == null)
                return Enumerable.Empty<TDestination>();

            return await Task.FromResult(source.Select(mapper.Map<TDestination>));
        }

        /// <summary>
        /// Асинхронно маппить сутність в DTO
        /// </summary>
        public static async Task<TDestination> MapAsync<TSource, TDestination>(
            this IMapper mapper,
            TSource source)
        {
            if (source == null)
                return default;

            return await Task.FromResult(mapper.Map<TDestination>(source));
        }

        /// <summary>
        /// Оновлює сутність даними з DTO
        /// </summary>
        public static void UpdateModel<TSource, TDestination>(
            this IMapper mapper,
            TSource source,
            TDestination destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            mapper.Map(source, destination);
        }
    }
} 