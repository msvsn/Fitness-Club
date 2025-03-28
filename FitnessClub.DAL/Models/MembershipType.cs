using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace FitnessClub.DAL.Models
{
    public class MembershipType
    {
        public MembershipType()
        {
            // Ініціалізуємо non-nullable властивості
            Name = string.Empty;
            Description = string.Empty;
            MembershipCards = new List<MembershipCard>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int DurationDays { get; set; }

        public decimal Price { get; set; }

        // Чи є цей абонемент мережевим (можна відвідувати будь-який клуб мережі)
        public bool IsNetworkWide { get; set; }

        // Додаємо можливість обмеження кількості відвідувань
        public int? VisitsLimit { get; set; }

        // Навігаційні властивості
        public ICollection<MembershipCard> MembershipCards { get; set; }
    }
} 