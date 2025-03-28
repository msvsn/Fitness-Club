using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.DAL.Models
{
    public class FitnessClub
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int LocationId { get; set; }

        // Зв'язок з локацією (1:M)
        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        // Навігаційні властивості
        public ICollection<MembershipCard> MembershipCards { get; set; }
        public ICollection<ClassSchedule> ClassSchedules { get; set; }
        public ICollection<Visit> Visits { get; set; }
    }
} 