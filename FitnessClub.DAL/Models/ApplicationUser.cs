using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace FitnessClub.DAL.Models
{
    /// <summary>
    /// Клас користувача системи, розширює стандартний IdentityUser
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        // Додаткові властивості користувача
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public DateTime? LastLoginDate { get; set; }
        
        // Зв'язок з клієнтом (якщо користувач є клієнтом)
        public int? ClientId { get; set; }
        public Client Client { get; set; }
        
        // Зв'язок з тренером (якщо користувач є тренером)
        public int? TrainerId { get; set; }
        public Trainer Trainer { get; set; }
        
        // Властивість для відстеження статусу користувача
        public bool IsActive { get; set; } = true;
        
        /// <summary>
        /// Повне ім'я користувача
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";
    }
} 