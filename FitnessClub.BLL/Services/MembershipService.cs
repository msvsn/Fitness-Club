using AutoMapper;
using FitnessClub.BLL.DTO;
using FitnessClub.BLL.Services.Interfaces;
using FitnessClub.DAL.Models;
using FitnessClub.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FitnessClub.BLL.Services
{
    public class MembershipTypeService : AutoMapperGenericService<MembershipType, MembershipTypeDTO>, IMembershipTypeService
    {
        public MembershipTypeService(IUnitOfWork unitOfWork, IMapper mapper) 
            : base(unitOfWork, mapper)
        {
        }

        protected override Expression<Func<MembershipType, bool>> GetByIdPredicate(int id)
        {
            return type => type.Id == id;
        }
    }

    public class MembershipCardService : AutoMapperGenericService<MembershipCard, MembershipCardDTO>, IMembershipCardService
    {
        public MembershipCardService(IUnitOfWork unitOfWork, IMapper mapper) 
            : base(unitOfWork, mapper)
        {
        }

        protected override Expression<Func<MembershipCard, bool>> GetByIdPredicate(int id)
        {
            return card => card.Id == id;
        }

        protected override IQueryable<MembershipCard> ApplyIncludes(IQueryable<MembershipCard> query)
        {
            return query
                .Include(c => c.Client)
                .Include(c => c.MembershipType)
                .Include(c => c.HomeClub);
        }

        public async Task<MembershipCardDTO> IssueMembershipCardAsync(int clientId, int membershipTypeId, int homeClubId)
        {
            // Перевірка існування сутностей
            bool clientExists = await _unitOfWork.GetRepository<Client>().ExistsAsync(c => c.Id == clientId);
            bool typeExists = await _unitOfWork.GetRepository<MembershipType>().ExistsAsync(t => t.Id == membershipTypeId);
            bool clubExists = await _unitOfWork.GetRepository<DAL.Models.FitnessClub>().ExistsAsync(c => c.Id == homeClubId);

            if (!clientExists || !typeExists || !clubExists)
                return null;

            // Отримуємо тип абонемента для визначення тривалості
            var typeRepository = _unitOfWork.GetRepository<MembershipType>();
            var membershipType = await typeRepository.GetByIdAsync(membershipTypeId);

            // Створюємо новий абонемент
            var membershipCard = new MembershipCard
            {
                ClientId = clientId,
                MembershipTypeId = membershipTypeId,
                HomeClubId = homeClubId,
                IssueDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(membershipType.DurationInDays),
                IsActive = true
            };

            // Створюємо абонемент, використовуючи метод базового сервісу, який автоматично завантажить зв'язані дані
            var repository = _unitOfWork.GetRepository<MembershipCard>();
            await repository.AddAsync(membershipCard);
            await _unitOfWork.CompleteAsync();

            return await GetByIdAsync(membershipCard.Id);
        }

        public async Task DeactivateMembershipCardAsync(int id)
        {
            var card = await GetByIdAsync(id);
            if (card == null)
                return;

            // Змінюємо статус активації на неактивний та оновлюємо
            var entity = await _mapper.MapAsync<MembershipCardDTO, MembershipCard>(card);
            entity.IsActive = false;
            
            await UpdateAsync(_mapper.Map<MembershipCard, MembershipCardDTO>(entity));
        }

        public async Task<bool> IsMembershipCardActiveAsync(int id)
        {
            var repository = _unitOfWork.GetRepository<MembershipCard>();
            var card = await repository.GetByIdAsync(id);
            
            return card != null && card.IsActive && card.ExpiryDate > DateTime.Now;
        }

        public async Task<bool> CanVisitClubAsync(int membershipCardId, int fitnessClubId)
        {
            var card = await GetByConditionAsync(c => c.Id == membershipCardId);
            if (card == null || !card.IsActive || card.ExpiryDate < DateTime.Now)
                return false;

            // Якщо абонемент для всіх клубів або для конкретного клубу
            return card.MembershipType.IsNetworkWide || card.HomeClubId == fitnessClubId;
        }

        public async Task ExtendMembershipCardAsync(int id, int days)
        {
            var card = await GetByIdAsync(id);
            if (card == null)
                return;

            // Працюємо з сутністю
            var entity = await _mapper.MapAsync<MembershipCardDTO, MembershipCard>(card);
            
            // Якщо абонемент вже прострочений, продовжуємо від поточної дати
            if (entity.ExpiryDate < DateTime.Now)
                entity.ExpiryDate = DateTime.Now.AddDays(days);
            else
                entity.ExpiryDate = entity.ExpiryDate.AddDays(days);

            // Активуємо абонемент, якщо він був деактивований
            entity.IsActive = true;
            
            await UpdateAsync(_mapper.Map<MembershipCard, MembershipCardDTO>(entity));
        }
    }
} 