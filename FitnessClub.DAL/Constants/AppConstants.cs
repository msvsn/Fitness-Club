namespace FitnessClub.DAL.Constants
{
    /// <summary>
    /// Клас для зберігання констант застосунку
    /// </summary>
    public static class AppConstants
    {
        // Константи для класів розкладу
        public static class ClassSchedule
        {
            public const int MinDayOfWeek = 1;
            public const int MaxDayOfWeek = 7;
            public const int DefaultMaxCapacity = 20;
            public const int DefaultDuration = 60; // хвилин
        }
        
        // Константи для карток членства
        public static class MembershipCard
        {
            public const int DefaultVisitsLimit = 12;
            public const int DefaultDurationDays = 30;
            public const decimal BasicPrice = 500m;
            public const decimal StandardPrice = 800m;
            public const decimal PremiumPrice = 1200m;
        }
        
        // Константи для фітнес-клубів
        public static class FitnessClub
        {
            public const string DefaultPrefix = "Fitness Pro";
        }
        
        // Константи для користувачів
        public static class User
        {
            public const string AdminRole = "Admin";
            public const string ClientRole = "Client";
            public const string TrainerRole = "Trainer";
            public const string ManagerRole = "Manager";
        }
        
        // Константи для ініціалізації даних
        public static class DbInitializer
        {
            public const int InitialLocationsCount = 3;
            public const int InitialClubsCount = 3;
            public const int InitialClassTypesCount = 5;
            public const int InitialTrainersCount = 5;
        }
    }
} 