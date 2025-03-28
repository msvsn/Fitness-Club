using FitnessClub.BLL.DTO;
using FitnessClub.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessClub.BLL.Services.Interfaces
{
    public interface IMembershipTypeService : IBaseService<MembershipType, MembershipTypeDTO>
    {
        // Специфічні методи для роботи з типами абонементів
    }

    public interface IMembershipCardService : IBaseService<MembershipCard, MembershipCardDTO>
    {
        Task<MembershipCardDTO> IssueMembershipCardAsync(int clientId, int membershipTypeId, int homeClubId);
        Task DeactivateMembershipCardAsync(int id);
        Task<bool> IsMembershipCardActiveAsync(int id);
        Task<bool> CanVisitClubAsync(int membershipCardId, int fitnessClubId);
        Task ExtendMembershipCardAsync(int id, int days);
    }
}