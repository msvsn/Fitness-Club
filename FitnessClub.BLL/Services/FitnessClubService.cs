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
    public class FitnessClubService : AutoMapperGenericService<DAL.Models.FitnessClub, FitnessClubDTO>, IFitnessClubService
    {
        public FitnessClubService(IUnitOfWork unitOfWork, IMapper mapper) 
            : base(unitOfWork, mapper)
        {
        }

        protected override Expression<Func<DAL.Models.FitnessClub, bool>> GetByIdPredicate(int id)
        {
            return club => club.Id == id;
        }

        public async Task<List<FitnessClubDTO>> GetFitnessClubsByLocationIdAsync(int locationId)
        {
            var repository = _unitOfWork.GetRepository<DAL.Models.FitnessClub>();
            var clubs = await repository
                .Find(c => c.LocationId == locationId)
                .Include(c => c.Location)
                .ToListAsync();

            return (await _mapper.MapCollectionAsync<DAL.Models.FitnessClub, FitnessClubDTO>(clubs)).ToList();
        }
    }

    public class LocationService : AutoMapperGenericService<Location, LocationDTO>, ILocationService
    {
        public LocationService(IUnitOfWork unitOfWork, IMapper mapper) 
            : base(unitOfWork, mapper)
        {
        }

        protected override Expression<Func<Location, bool>> GetByIdPredicate(int id)
        {
            return location => location.Id == id;
        }
    }
} 