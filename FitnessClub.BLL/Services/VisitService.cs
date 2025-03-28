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
    public class VisitService : AutoMapperGenericService<Visit, VisitDTO>, IVisitService
    {
        private readonly IMembershipCardService _membershipCardService;

        public VisitService(
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            IMembershipCardService membershipCardService) 
            : base(unitOfWork, mapper)
        {
            _membershipCardService = membershipCardService;
        }

        protected override Expression<Func<Visit, bool>> GetByIdPredicate(int id)
        {
            return visit => visit.Id == id;
        }

        protected override IQueryable<Visit> ApplyIncludes(IQueryable<Visit> query)
        {
            return query
                .Include(v => v.Client)
                .Include(v => v.FitnessClub)
                .Include(v => v.MembershipCard)
                .Include(v => v.ClassRegistration);
        }

        public async Task<List<VisitDTO>> GetVisitsByClientIdAsync(int clientId)
        {
            var visits = await GetManyByConditionAsync(v => v.ClientId == clientId);
            return visits.ToList();
        }

        public async Task<List<VisitDTO>> GetVisitsByFitnessClubIdAsync(int fitnessClubId)
        {
            var visits = await GetManyByConditionAsync(v => v.FitnessClubId == fitnessClubId);
            return visits.ToList();
        }

        public async Task<List<VisitDTO>> GetVisitsByMembershipCardIdAsync(int membershipCardId)
        {
            var visits = await GetManyByConditionAsync(v => v.MembershipCardId == membershipCardId);
            return visits.ToList();
        }

        public async Task<VisitDTO> GetVisitByClassRegistrationIdAsync(int classRegistrationId)
        {
            return await GetByConditionAsync(v => v.ClassRegistrationId == classRegistrationId);
        }

        // Базовий метод для реєстрації відвідування
        private async Task<VisitDTO> RegisterVisitAsync(Visit visit)
        {
            // Базова перевірка
            if (visit.ClientId <= 0 || visit.FitnessClubId <= 0)
                return null;

            // Перевіряємо існування клієнта та клубу
            var clientExists = await _unitOfWork.GetRepository<Client>().ExistsAsync(c => c.Id == visit.ClientId);
            var clubExists = await _unitOfWork.GetRepository<DAL.Models.FitnessClub>().ExistsAsync(c => c.Id == visit.FitnessClubId);

            if (!clientExists || !clubExists)
                return null;

            // Створюємо DTO для виклику базового методу
            var dto = _mapper.Map<VisitDTO>(visit);
            return await CreateAsync(dto);
        }

        public async Task<VisitDTO> RegisterVisitByMembershipCardAsync(int clientId, int fitnessClubId, int membershipCardId)
        {
            // Перевіряємо, чи може клієнт відвідати цей клуб з цим абонементом
            if (!await CanVisitByMembershipCardAsync(clientId, fitnessClubId, membershipCardId))
                return null;

            var visit = new Visit
            {
                ClientId = clientId,
                FitnessClubId = fitnessClubId,
                MembershipCardId = membershipCardId,
                VisitDate = DateTime.Now,
                ExitDate = null,
                VisitType = "Абонемент",
                SingleVisitPayment = null
            };

            return await RegisterVisitAsync(visit);
        }

        public async Task<VisitDTO> RegisterSingleVisitAsync(int clientId, int fitnessClubId, decimal payment)
        {
            var visit = new Visit
            {
                ClientId = clientId,
                FitnessClubId = fitnessClubId,
                MembershipCardId = null,
                VisitDate = DateTime.Now,
                ExitDate = null,
                VisitType = "Разове",
                SingleVisitPayment = payment
            };

            return await RegisterVisitAsync(visit);
        }

        public async Task<VisitDTO> RegisterVisitByClassRegistrationAsync(int clientId, int fitnessClubId, int classRegistrationId)
        {
            // Перевіряємо реєстрацію на заняття
            var regRepository = _unitOfWork.GetRepository<ClassRegistration>();
            var registration = await regRepository
                .Find(r => r.Id == classRegistrationId && r.ClientId == clientId && r.Status == "Confirmed")
                .Include(r => r.ClassSchedule)
                .FirstOrDefaultAsync();
            
            if (registration == null || registration.ClassSchedule.FitnessClubId != fitnessClubId)
                return null;

            var visit = new Visit
            {
                ClientId = clientId,
                FitnessClubId = fitnessClubId,
                ClassRegistrationId = classRegistrationId,
                VisitDate = DateTime.Now,
                ExitDate = null,
                VisitType = "Заняття",
                SingleVisitPayment = null
            };

            var result = await RegisterVisitAsync(visit);
            if (result != null)
            {
                // Оновлюємо статус реєстрації
                registration.Status = "Completed";
                regRepository.Update(registration);
                await _unitOfWork.CompleteAsync();
            }

            return result;
        }

        public async Task CompleteVisitAsync(int id)
        {
            var visit = await GetByIdAsync(id);
            if (visit == null || visit.ExitDate.HasValue)
                return;
                
            visit.ExitDate = DateTime.Now;
            await UpdateAsync(visit);
        }

        public async Task<bool> CanVisitByMembershipCardAsync(int clientId, int fitnessClubId, int membershipCardId)
        {
            // Перевіряємо, чи абонемент належить клієнту
            var cardRepository = _unitOfWork.GetRepository<MembershipCard>();
            var card = await cardRepository.FirstOrDefaultAsync(c => c.Id == membershipCardId && c.ClientId == clientId);
            
            if (card == null)
                return false;

            // Перевіряємо, чи дійсний абонемент для цього клубу
            return await _membershipCardService.CanVisitClubAsync(membershipCardId, fitnessClubId);
        }
    }
} 