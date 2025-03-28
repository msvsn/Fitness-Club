using System;

namespace FitnessClub.BLL.DTO
{
    public class ClassScheduleDTO
    {
        public int Id { get; set; }
        public int DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsRecurring { get; set; }
        public int FitnessClubId { get; set; }
        public int TrainerId { get; set; }
        public int ClassTypeId { get; set; }
        
        public ClassTypeDTO ClassType { get; set; }
        public FitnessClubDTO FitnessClub { get; set; }
        public TrainerDTO Trainer { get; set; }
        
        public string FormattedDayOfWeek 
        { 
            get 
            {
                return DayOfWeek switch
                {
                    1 => "Понеділок",
                    2 => "Вівторок",
                    3 => "Середа",
                    4 => "Четвер",
                    5 => "П'ятниця",
                    6 => "Субота",
                    7 => "Неділя",
                    _ => "Невідомий день"
                };
            }
        }
        
        public string FormattedTimeRange
        {
            get
            {
                return $"{StartTime.ToString(@"hh\:mm")} - {EndTime.ToString(@"hh\:mm")}";
            }
        }
        
        public string FormattedDuration
        {
            get
            {
                var duration = EndTime - StartTime;
                return $"{duration.TotalMinutes} хв";
            }
        }
    }
} 