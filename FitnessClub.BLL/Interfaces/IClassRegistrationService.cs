using FitnessClub.BLL.DTO;
using FitnessClub.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessClub.BLL.Services.Interfaces
{
    public interface IClassRegistrationService : IBaseService<ClassRegistration, ClassRegistrationDTO>
    {
        Task<List<ClassRegistrationDTO>> GetClassRegistrationsByClientIdAsync(int clientId);
        Task<List<ClassRegistrationDTO>> GetClassRegistrationsByScheduleIdAsync(int classScheduleId);
        Task<ClassRegistrationDTO> RegisterForClassAsync(int clientId, int classScheduleId);
        Task CancelRegistrationAsync(int id);
        Task ConfirmRegistrationAsync(int id);
        Task<bool> CanRegisterForClassAsync(int clientId, int classScheduleId);
    }
} 