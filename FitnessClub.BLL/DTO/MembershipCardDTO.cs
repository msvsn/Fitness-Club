using System;

namespace FitnessClub.BLL.DTO
{
    public class MembershipCardDTO
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public int ClientId { get; set; }
        public int MembershipTypeId { get; set; }
        public int? HomeClubId { get; set; }

        public ClientDTO Client { get; set; }
        public MembershipTypeDTO MembershipType { get; set; }
        public FitnessClubDTO HomeClub { get; set; }

        public int DaysLeft => (ExpiryDate.Date - DateTime.Today).Days;
        public string Status => IsActive ? "Активний" : "Неактивний";
        public string FormattedIssueDate => IssueDate.ToShortDateString();
        public string FormattedExpiryDate => ExpiryDate.ToShortDateString();
    }
} 