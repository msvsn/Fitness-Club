using FitnessClub.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FitnessClub.DAL.Repositories
{
    /// <summary>
    /// Універсальний репозиторій для роботи з сутностями
    /// </summary>
    /// <typeparam name="TEntity">Тип сутності</typeparam>
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly FitnessClubDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(FitnessClubDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Отримати всі сутності
        /// </summary>
        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        /// <summary>
        /// Фільтрувати сутності за предикатом
        /// </summary>
        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        /// <summary>
        /// Отримати сутність за ідентифікатором
        /// </summary>
        public virtual TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Отримати сутність за ідентифікатором асинхронно
        /// </summary>
        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Додати сутність
        /// </summary>
        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>
        /// Додати декілька сутностей
        /// </summary>
        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        /// <summary>
        /// Оновити сутність
        /// </summary>
        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Видалити сутність
        /// </summary>
        public virtual void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// Видалити декілька сутностей
        /// </summary>
        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        /// <summary>
        /// Перевірити чи існує сутність за предикатом
        /// </summary>
        public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Any(predicate);
        }
    }
} 