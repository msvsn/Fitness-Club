using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessClub.DAL.Models
{
    public class Trainer
    {
        public Trainer()
        {
            ClassSchedules = new List<ClassSchedule>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(500)]
        public string Bio { get; set; }

        [StringLength(200)]
        public string Specialization { get; set; }

        // Навігаційні властивості
        public ICollection<ClassSchedule> ClassSchedules { get; set; }

        // Зв'язок з обліковим записом користувача
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        // Повне ім'я тренера
        public string FullName => $"{FirstName} {LastName}";
    }
} 