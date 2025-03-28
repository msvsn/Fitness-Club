using FitnessClub.BLL.DTO;
using FitnessClub.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessClub.BLL.Services.Interfaces
{
    public interface IClientService : IBaseService<Client, ClientDTO>
    {
        // Специфічні методи, що стосуються клієнта
        Task<List<MembershipCardDTO>> GetClientMembershipCardsAsync(int clientId);
        Task<List<VisitDTO>> GetClientVisitsAsync(int clientId);
        Task<List<ClassRegistrationDTO>> GetClientClassRegistrationsAsync(int clientId);
    }
}