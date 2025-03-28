using System;

namespace FitnessClub.BLL.DTO
{
    public class VisitDTO
    {
        public int Id { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime? ExitDate { get; set; }
        public string VisitType { get; set; }
        
        public int? MembershipCardId { get; set; }
        public decimal? SingleVisitPayment { get; set; }
        
        public int ClientId { get; set; }
        public int FitnessClubId { get; set; }
        public int? ClassRegistrationId { get; set; }
        
        public ClientDTO Client { get; set; }
        public FitnessClubDTO FitnessClub { get; set; }
        public MembershipCardDTO MembershipCard { get; set; }
        public ClassRegistrationDTO ClassRegistration { get; set; }
        
        public string FormattedVisitDate => VisitDate.ToString("dd.MM.yyyy HH:mm");
        public string FormattedExitDate => ExitDate.HasValue ? ExitDate.Value.ToString("dd.MM.yyyy HH:mm") : "Триває";
        public TimeSpan? Duration => ExitDate.HasValue ? ExitDate.Value.Subtract(VisitDate) : null;
        public string FormattedDuration => Duration.HasValue ? $"{Duration.Value.TotalMinutes} хв" : "Триває";
        public bool IsActive => !ExitDate.HasValue;
        public string FormattedVisitType => VisitType == "Абонемент" 
            ? $"За абонементом {MembershipCard?.CardNumber}" 
            : VisitType == "Разове" 
                ? $"Разовий візит ({SingleVisitPayment:C})" 
                : "На заняття";
    }
} 