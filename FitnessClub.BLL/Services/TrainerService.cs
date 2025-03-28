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
    public class TrainerService : AutoMapperGenericService<Trainer, TrainerDTO>, ITrainerService
    {
        private readonly IClassScheduleService _classScheduleService;

        public TrainerService(
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            IClassScheduleService classScheduleService = null) 
            : base(unitOfWork, mapper)
        {
            _classScheduleService = classScheduleService;
        }

        protected override Expression<Func<Trainer, bool>> GetByIdPredicate(int id)
        {
            return trainer => trainer.Id == id;
        }

        public async Task<List<ClassScheduleDTO>> GetTrainerScheduleAsync(int trainerId)
        {
            // Якщо є сервіс розкладу, делегуємо йому виконання
            if (_classScheduleService != null)
            {
                return await _classScheduleService.GetClassSchedulesByTrainerIdAsync(trainerId);
            }
            
            // Якщо немає, виконуємо самостійно
            var repository = _unitOfWork.GetRepository<ClassSchedule>();
            var schedules = await repository
                .Find(schedule => schedule.TrainerId == trainerId)
                .Include(schedule => schedule.ClassType)
                .Include(schedule => schedule.FitnessClub)
                .ToListAsync();
            
            return (await _mapper.MapCollectionAsync<ClassSchedule, ClassScheduleDTO>(schedules)).ToList();
        }

        public async Task<bool> IsTrainerAvailableAsync(int trainerId, DateTime startTime, DateTime endTime)
        {
            // Перевіряємо, що початок < кінець
            if (startTime >= endTime)
                return false;

            // Отримуємо день тижня для перевірки
            int dayOfWeek = (int)startTime.DayOfWeek == 0 ? 7 : (int)startTime.DayOfWeek; // 1-7, де 1 = Понеділок, 7 = Неділя
            
            // Конвертуємо DateTime в TimeSpan для порівняння з розкладом
            TimeSpan startTimeSpan = new TimeSpan(startTime.Hour, startTime.Minute, 0);
            TimeSpan endTimeSpan = new TimeSpan(endTime.Hour, endTime.Minute, 0);

            var repository = _unitOfWork.GetRepository<ClassSchedule>();
            
            // Перевіряємо, чи є вже заняття у тренера в цей час
            return !await repository.ExistsAsync(s => 
                s.TrainerId == trainerId && 
                s.DayOfWeek == dayOfWeek && 
                ((s.StartTime <= startTimeSpan && s.EndTime > startTimeSpan) || 
                 (s.StartTime < endTimeSpan && s.EndTime >= endTimeSpan) ||
                 (s.StartTime >= startTimeSpan && s.EndTime <= endTimeSpan)));
        }
    }
} 