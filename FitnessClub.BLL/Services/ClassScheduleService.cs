using AutoMapper;
using FitnessClub.BLL.DTO;
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
    public class ClassScheduleService : AutoMapperGenericService<ClassSchedule, ClassScheduleDTO>, IClassScheduleService
    {
        public ClassScheduleService(IUnitOfWork unitOfWork, IMapper mapper) 
            : base(unitOfWork, mapper)
        {
        }

        protected override Expression<Func<ClassSchedule, bool>> GetByIdPredicate(int id)
        {
            return schedule => schedule.Id == id;
        }

        // Перевизначаємо метод для додавання стандартних Include
        protected override IQueryable<ClassSchedule> ApplyIncludes(IQueryable<ClassSchedule> query)
        {
            return query
                .Include(s => s.ClassType)
                .Include(s => s.Trainer)
                .Include(s => s.FitnessClub);
        }

        public async Task<List<ClassScheduleDTO>> GetClassSchedulesByFitnessClubIdAsync(int fitnessClubId)
        {
            var entities = await GetManyByConditionAsync(s => s.FitnessClubId == fitnessClubId);
            return entities.ToList();
        }

        public async Task<List<ClassScheduleDTO>> GetClassSchedulesByDayOfWeekAsync(int fitnessClubId, int dayOfWeek)
        {
            var repository = _unitOfWork.GetRepository<ClassSchedule>();
            var query = ApplyIncludes(
                repository.Find(s => s.FitnessClubId == fitnessClubId && s.DayOfWeek == dayOfWeek)
            ).OrderBy(s => s.StartTime);
            
            var schedules = await query.ToListAsync();
            return await MapToListAsync(schedules);
        }

        public async Task<List<ClassScheduleDTO>> GetClassSchedulesByTrainerIdAsync(int trainerId)
        {
            var entities = await GetManyByConditionAsync(s => s.TrainerId == trainerId);
            return entities.ToList();
        }

        public async Task<int> GetCurrentRegistrationsCountAsync(int classScheduleId)
        {
            var repository = _unitOfWork.GetRepository<ClassRegistration>();
            return await repository.CountAsync(r => 
                r.ClassScheduleId == classScheduleId && 
                (r.Status == "Confirmed" || r.Status == "Registered")
            );
        }

        public async Task<bool> IsClassAvailableAsync(int classScheduleId)
        {
            var scheduleRepository = _unitOfWork.GetRepository<ClassSchedule>();
            var schedule = await scheduleRepository
                .Find(s => s.Id == classScheduleId)
                .Include(s => s.ClassType)
                .FirstOrDefaultAsync();

            if (schedule == null)
                return false;

            // Перевіряємо кількість зареєстрованих відвідувачів
            int registrationsCount = await GetCurrentRegistrationsCountAsync(classScheduleId);
            
            // Якщо кількість реєстрацій менша за максимальну місткість
            return registrationsCount < schedule.ClassType.MaxCapacity;
        }
    }
} 