using System;

namespace FitnessClub.BLL.DTO
{
    public class ClassTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxCapacity { get; set; }
        public decimal SingleVisitPrice { get; set; }
        public string FormattedPrice => $"{SingleVisitPrice:C}";
        public int Duration { get; set; } // Тривалість в хвилинах
    }
} 