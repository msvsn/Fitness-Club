using FitnessClub.BLL.DTO;
using FitnessClub.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessClub.BLL.Services.Interfaces
{
    public interface IClassScheduleService : IBaseService<ClassSchedule, ClassScheduleDTO>
    {
        Task<List<ClassScheduleDTO>> GetClassSchedulesByFitnessClubIdAsync(int fitnessClubId);
        Task<List<ClassScheduleDTO>> GetClassSchedulesByDayOfWeekAsync(int fitnessClubId, int dayOfWeek);
        Task<List<ClassScheduleDTO>> GetClassSchedulesByTrainerIdAsync(int trainerId);
        Task<int> GetCurrentRegistrationsCountAsync(int classScheduleId);
        Task<bool> IsClassAvailableAsync(int classScheduleId);
    }
}