using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessClub.DAL.Models
{
    public class Location
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        // Навігаційні властивості
        public ICollection<FitnessClub> FitnessClubs { get; set; }
    }
} 