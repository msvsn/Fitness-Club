using FitnessClub.BLL.DTO;
using FitnessClub.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessClub.BLL.Services.Interfaces
{
    public interface IVisitService : IBaseService<Visit, VisitDTO>
    {
        Task<List<VisitDTO>> GetVisitsByClientIdAsync(int clientId);
        Task<List<VisitDTO>> GetVisitsByFitnessClubIdAsync(int fitnessClubId);
        Task<List<VisitDTO>> GetVisitsByMembershipCardIdAsync(int membershipCardId);
        Task<VisitDTO> GetVisitByClassRegistrationIdAsync(int classRegistrationId);
        Task<VisitDTO> RegisterVisitByMembershipCardAsync(int clientId, int fitnessClubId, int membershipCardId);
        Task<VisitDTO> RegisterSingleVisitAsync(int clientId, int fitnessClubId, decimal payment);
        Task<VisitDTO> RegisterVisitByClassRegistrationAsync(int clientId, int fitnessClubId, int classRegistrationId);
        Task CompleteVisitAsync(int id);
        Task<bool> CanVisitByMembershipCardAsync(int clientId, int fitnessClubId, int membershipCardId);
    }
} 