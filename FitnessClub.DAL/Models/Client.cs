using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessClub.DAL.Models
{
    public class Client
    {
        public Client()
        {
            MembershipCards = new List<MembershipCard>();
            ClassRegistrations = new List<ClassRegistration>();
            Visits = new List<Visit>();
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

        public DateTime? DateOfBirth { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        // Навігаційні властивості
        public ICollection<MembershipCard> MembershipCards { get; set; }
        public ICollection<ClassRegistration> ClassRegistrations { get; set; }
        public ICollection<Visit> Visits { get; set; }

        // Зв'язок з обліковим записом користувача
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        // Розрахункове повне ім'я
        public string FullName => $"{FirstName} {LastName}";
    }
} 