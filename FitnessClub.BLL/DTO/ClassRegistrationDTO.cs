using System;

namespace FitnessClub.BLL.DTO
{
    public class ClassRegistrationDTO
    {
        public int Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Status { get; set; }
        
        public int ClientId { get; set; }
        public int ClassScheduleId { get; set; }
        
        public ClientDTO Client { get; set; }
        public ClassScheduleDTO ClassSchedule { get; set; }
        public VisitDTO Visit { get; set; }
        
        public string FormattedRegistrationDate => RegistrationDate.ToString("dd.MM.yyyy HH:mm");
        public bool IsConfirmed => Status == "Підтверджено";
        public bool IsCancelled => Status == "Скасовано";
        public bool IsPending => Status == "В очікуванні";
    }
} 