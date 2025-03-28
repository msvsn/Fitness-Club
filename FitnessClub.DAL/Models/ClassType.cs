using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace FitnessClub.DAL.Models
{
    public class ClassType
    {
        public ClassType()
        {
            // Ініціалізуємо колекції і non-nullable властивості
            Name = string.Empty;
            Description = string.Empty;
            ClassSchedules = new List<ClassSchedule>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int MaxCapacity { get; set; }

        public int Duration { get; set; } // Тривалість в хвилинах

        public decimal SingleVisitPrice { get; set; }

        // Навігаційні властивості
        public ICollection<ClassSchedule> ClassSchedules { get; set; }
    }
} 