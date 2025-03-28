using System;

namespace FitnessClub.BLL.DTO
{
    public class LocationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string FullAddress => $"{Address}, {City}";
    }
} 