namespace FitnessClub.BLL.DTO
{
    public class MembershipTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public bool IsNetworkWide { get; set; }
        public string FormattedPrice => $"{Price:C}";
        public string FormattedDuration => DurationInDays >= 30 
            ? $"{DurationInDays / 30} місяць(ів)" 
            : $"{DurationInDays} днів";
    }
} 