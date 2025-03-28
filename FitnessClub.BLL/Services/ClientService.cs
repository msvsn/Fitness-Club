using AutoMapper;
using FitnessClub.BLL.DTO;
using FitnessClub.BLL.Services.Interfaces;
using FitnessClub.DAL.Models;
using FitnessClub.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FitnessClub.BLL.Services
{
    public class ClientService : AutoMapperGenericService<Client, ClientDTO>, IClientService
    {
        public ClientService(IUnitOfWork unitOfWork, IMapper mapper) 
            : base(unitOfWork, mapper)
        {
        }

        protected override Expression<Func<Client, bool>> GetByIdPredicate(int id)
        {
            return client => client.Id == id;
        }

        public async Task<List<MembershipCardDTO>> GetClientMembershipCardsAsync(int clientId)
        {
            var repository = _unitOfWork.GetRepository<MembershipCard>();
            var cards = await repository.Find(card => card.ClientId == clientId)
                .Include(card => card.MembershipType)
                .Include(card => card.HomeClub)
                .ToListAsync();
            
            return await MapToListAsync(cards);
        }

        public async Task<List<VisitDTO>> GetClientVisitsAsync(int clientId)
        {
            var repository = _unitOfWork.GetRepository<Visit>();
            var visits = await repository.Find(visit => visit.ClientId == clientId)
                .Include(visit => visit.FitnessClub)
                .Include(visit => visit.MembershipCard)
                .ToListAsync();
            
            return await MapToListAsync(visits);
        }

        public async Task<List<ClassRegistrationDTO>> GetClientClassRegistrationsAsync(int clientId)
        {
            var repository = _unitOfWork.GetRepository<ClassRegistration>();
            var registrations = await repository.Find(reg => reg.ClientId == clientId)
                .Include(reg => reg.ClassSchedule)
                    .ThenInclude(schedule => schedule.ClassType)
                .Include(reg => reg.ClassSchedule)
                    .ThenInclude(schedule => schedule.Trainer)
                .ToListAsync();
            
            return await MapToListAsync(registrations);
        }
    }
} 