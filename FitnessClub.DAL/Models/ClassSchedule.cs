using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.DAL.Models
{
    public class ClassSchedule
    {
        public ClassSchedule()
        {
            // Ініціалізуємо колекції і non-nullable властивості
            ClassType = null!;
            FitnessClub = null!;
            Trainer = null!;
            ClassRegistrations = new List<ClassRegistration>();
        }

        public int Id { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        // День тижня (1-7, де 1 - понеділок, 7 - неділя)
        public int DayOfWeek { get; set; }

        public bool IsRecurring { get; set; }

        public int ClassTypeId { get; set; }

        public int FitnessClubId { get; set; }

        public int TrainerId { get; set; }

        // Зв'язки (1:M)
        [ForeignKey("ClassTypeId")]
        public ClassType ClassType { get; set; }

        [ForeignKey("FitnessClubId")]
        public FitnessClub FitnessClub { get; set; }

        [ForeignKey("TrainerId")]
        public Trainer Trainer { get; set; }

        // Навігаційні властивості
        public ICollection<ClassRegistration> ClassRegistrations { get; set; }
    }
} 