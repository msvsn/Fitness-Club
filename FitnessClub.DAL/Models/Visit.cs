using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.DAL.Models
{
    public class Visit
    {
        public int Id { get; set; }

        public DateTime VisitDate { get; set; }

        public DateTime? ExitDate { get; set; }

        // Тип відвідування (за абонементом, разове)
        [StringLength(20)]
        public string VisitType { get; set; }

        // Якщо візит за абонементом
        public int? MembershipCardId { get; set; }

        // Якщо разовий візит
        public decimal? SingleVisitPayment { get; set; }

        public int ClientId { get; set; }

        public int FitnessClubId { get; set; }

        // Якщо візит на конкретне заняття
        public int? ClassRegistrationId { get; set; }

        // Зв'язки (1:M)
        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        [ForeignKey("FitnessClubId")]
        public FitnessClub FitnessClub { get; set; }

        [ForeignKey("MembershipCardId")]
        public MembershipCard MembershipCard { get; set; }

        [ForeignKey("ClassRegistrationId")]
        public ClassRegistration ClassRegistration { get; set; }
    }
} 