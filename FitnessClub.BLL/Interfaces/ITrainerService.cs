using FitnessClub.BLL.DTO;
using FitnessClub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessClub.BLL.Services.Interfaces
{
    public interface ITrainerService : IBaseService<Trainer, TrainerDTO>
    {
        // Специфічні методи, що стосуються тренера
        Task<List<ClassScheduleDTO>> GetTrainerScheduleAsync(int trainerId);
        Task<bool> IsTrainerAvailableAsync(int trainerId, DateTime startTime, DateTime endTime);
    }
} 