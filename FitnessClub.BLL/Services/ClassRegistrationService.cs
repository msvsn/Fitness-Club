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
    public class ClassRegistrationService : AutoMapperGenericService<ClassRegistration, ClassRegistrationDTO>, IClassRegistrationService
    {
        private readonly IClassScheduleService _classScheduleService;

        public ClassRegistrationService(
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            IClassScheduleService classScheduleService) 
            : base(unitOfWork, mapper)
        {
            _classScheduleService = classScheduleService;
        }

        protected override Expression<Func<ClassRegistration, bool>> GetByIdPredicate(int id)
        {
            return registration => registration.Id == id;
        }

        protected override IQueryable<ClassRegistration> ApplyIncludes(IQueryable<ClassRegistration> query)
        {
            return query
                .Include(r => r.Client)
                .Include(r => r.ClassSchedule)
                    .ThenInclude(cs => cs.ClassType)
                .Include(r => r.ClassSchedule)
                    .ThenInclude(cs => cs.Trainer);
        }

        public async Task<List<ClassRegistrationDTO>> GetClassRegistrationsByClientIdAsync(int clientId)
        {
            var registrations = await GetManyByConditionAsync(r => r.ClientId == clientId);
            return registrations.ToList();
        }

        public async Task<List<ClassRegistrationDTO>> GetClassRegistrationsByScheduleIdAsync(int classScheduleId)
        {
            var registrations = await GetManyByConditionAsync(r => r.ClassScheduleId == classScheduleId);
            return registrations.ToList();
        }

        public async Task<ClassRegistrationDTO> RegisterForClassAsync(int clientId, int classScheduleId)
        {
            if (!await CanRegisterForClassAsync(clientId, classScheduleId))
                return null;

            // Перевіряємо існування клієнта
            if (!await _unitOfWork.GetRepository<Client>().ExistsAsync(c => c.Id == clientId))
                return null;

            var registration = new ClassRegistration
            {
                ClientId = clientId,
                ClassScheduleId = classScheduleId,
                RegistrationDate = DateTime.Now,
                Status = "Registered" // Можливі статуси: Registered, Confirmed, Canceled, Completed
            };

            // Використовуємо метод CreateAsync з базового класу для створення та отримання повного об'єкта
            var dto = new ClassRegistrationDTO
            {
                ClientId = registration.ClientId,
                ClassScheduleId = registration.ClassScheduleId,
                RegistrationDate = registration.RegistrationDate,
                Status = registration.Status
            };

            return await CreateAsync(dto);
        }

        public async Task CancelRegistrationAsync(int id)
        {
            await UpdateRegistrationStatusAsync(id, "Canceled");
        }

        public async Task ConfirmRegistrationAsync(int id)
        {
            await UpdateRegistrationStatusAsync(id, "Confirmed");
        }

        private async Task UpdateRegistrationStatusAsync(int id, string status)
        {
            var registration = await GetByIdAsync(id);
            if (registration == null)
                return;
                
            registration.Status = status;
            await UpdateAsync(registration);
        }

        public async Task<bool> CanRegisterForClassAsync(int clientId, int classScheduleId)
        {
            // Перевіряємо, чи є місця в групі
            if (!await _classScheduleService.IsClassAvailableAsync(classScheduleId))
                return false;

            // Перевіряємо, чи клієнт вже зареєстрований на це заняття
            return !await _unitOfWork.GetRepository<ClassRegistration>().ExistsAsync(r => 
                r.ClientId == clientId && 
                r.ClassScheduleId == classScheduleId &&
                r.Status != "Canceled"
            );
        }
    }
} 