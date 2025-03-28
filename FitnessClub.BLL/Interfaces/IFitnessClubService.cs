using FitnessClub.BLL.DTO;
using FitnessClub.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessClub.BLL.Services.Interfaces
{
    public interface IFitnessClubService : IBaseService<DAL.Models.FitnessClub, FitnessClubDTO>
    {
        Task<List<FitnessClubDTO>> GetFitnessClubsByLocationIdAsync(int locationId);
    }

    public interface ILocationService : IBaseService<Location, LocationDTO>
    {
        // Специфічні методи для роботи з локаціями можна додати тут
    }
} 