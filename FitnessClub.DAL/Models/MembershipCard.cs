using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.DAL.Models
{
    public class MembershipCard
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string CardNumber { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool IsActive { get; set; }

        public int ClientId { get; set; }

        public int MembershipTypeId { get; set; }

        public int? HomeClubId { get; set; }

        // Зв'язки (1:M)
        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        [ForeignKey("MembershipTypeId")]
        public MembershipType MembershipType { get; set; }

        [ForeignKey("HomeClubId")]
        public FitnessClub HomeClub { get; set; }

        // Навігаційні властивості
        public ICollection<Visit> Visits { get; set; }
    }
} 