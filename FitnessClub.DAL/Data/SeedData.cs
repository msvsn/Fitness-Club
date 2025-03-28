using FitnessClub.DAL.Constants;
using FitnessClub.DAL.Models;
using System.Collections.Generic;

namespace FitnessClub.DAL.Data
{
    /// <summary>
    /// Клас для зберігання тестових даних для заповнення бази даних
    /// </summary>
    public static class SeedData
    {
        /// <summary>
        /// Отримати локації для заповнення бази даних
        /// </summary>
        public static List<Location> GetLocations()
        {
            return new List<Location>
            {
                new Location { 
                    Name = "Центральна локація", 
                    Address = "вул. Центральна, 123", 
                    City = "Київ", 
                    PhoneNumber = "+380441234567" 
                },
                new Location { 
                    Name = "Північна локація", 
                    Address = "вул. Північна, 45", 
                    City = "Київ", 
                    PhoneNumber = "+380442345678" 
                },
                new Location { 
                    Name = "Південна локація", 
                    Address = "вул. Південна, 78", 
                    City = "Київ", 
                    PhoneNumber = "+380443456789" 
                }
            };
        }

        /// <summary>
        /// Отримати фітнес клуби для заповнення бази даних
        /// </summary>
        public static List<Models.FitnessClub> GetFitnessClubs(List<Location> locations)
        {
            return new List<Models.FitnessClub>
            {
                new Models.FitnessClub { 
                    Name = $"{AppConstants.FitnessClub.DefaultPrefix} Центр", 
                    Description = "Найбільший фітнес клуб у центрі міста з повним спектром послуг.", 
                    LocationId = locations[0].Id 
                },
                new Models.FitnessClub { 
                    Name = $"{AppConstants.FitnessClub.DefaultPrefix} Північ", 
                    Description = "Сучасний фітнес клуб на півночі міста.", 
                    LocationId = locations[1].Id 
                },
                new Models.FitnessClub { 
                    Name = $"{AppConstants.FitnessClub.DefaultPrefix} Південь", 
                    Description = "Затишний фітнес клуб на півдні міста.", 
                    LocationId = locations[2].Id 
                }
            };
        }

        /// <summary>
        /// Отримати типи класів для заповнення бази даних
        /// </summary>
        public static List<ClassType> GetClassTypes()
        {
            return new List<ClassType>
            {
                new ClassType { 
                    Name = "Йога", 
                    Description = "Заняття з йоги для початківців та досвідчених", 
                    MaxCapacity = AppConstants.ClassSchedule.DefaultMaxCapacity, 
                    Duration = 60, 
                    SingleVisitPrice = 150.0m 
                },
                new ClassType { 
                    Name = "Пілатес", 
                    Description = "Пілатес для зміцнення м'язів кора", 
                    MaxCapacity = 15, 
                    Duration = 45, 
                    SingleVisitPrice = 180.0m 
                },
                new ClassType { 
                    Name = "Кардіо", 
                    Description = "Інтенсивне кардіо тренування", 
                    MaxCapacity = AppConstants.ClassSchedule.DefaultMaxCapacity, 
                    Duration = 45, 
                    SingleVisitPrice = 200.0m 
                },
                new ClassType { 
                    Name = "Силове тренування", 
                    Description = "Силові тренування з використанням ваги", 
                    MaxCapacity = 10, 
                    Duration = 60, 
                    SingleVisitPrice = 230.0m 
                },
                new ClassType { 
                    Name = "Розтяжка", 
                    Description = "Клас на гнучкість та розтяжку", 
                    MaxCapacity = AppConstants.ClassSchedule.DefaultMaxCapacity, 
                    Duration = 50, 
                    SingleVisitPrice = 170.0m 
                }
            };
        }

        /// <summary>
        /// Отримати тренерів для заповнення бази даних
        /// </summary>
        public static List<Trainer> GetTrainers()
        {
            return new List<Trainer>
            {
                new Trainer { 
                    FirstName = "Олександр", 
                    LastName = "Петренко", 
                    Email = "trainer1@fitness.com", 
                    PhoneNumber = "+380661234567", 
                    Bio = "Сертифікований тренер з йоги з 10-річним досвідом", 
                    Specialization = "Йога" 
                },
                new Trainer { 
                    FirstName = "Ірина", 
                    LastName = "Коваленко", 
                    Email = "trainer2@fitness.com", 
                    PhoneNumber = "+380672345678", 
                    Bio = "Майстер спорту з гімнастики, викладає пілатес 5 років", 
                    Specialization = "Пілатес" 
                },
                new Trainer { 
                    FirstName = "Микола", 
                    LastName = "Сидоренко", 
                    Email = "trainer3@fitness.com", 
                    PhoneNumber = "+380683456789", 
                    Bio = "Персональний тренер з кардіо та силових тренувань", 
                    Specialization = "Кардіо, Силові тренування" 
                },
                new Trainer { 
                    FirstName = "Марія", 
                    LastName = "Іваненко", 
                    Email = "trainer4@fitness.com", 
                    PhoneNumber = "+380694567890", 
                    Bio = "Інструктор з розтяжки, має досвід роботи з професійними спортсменами", 
                    Specialization = "Розтяжка" 
                },
                new Trainer { 
                    FirstName = "Василь", 
                    LastName = "Мельник", 
                    Email = "trainer5@fitness.com", 
                    PhoneNumber = "+380635678901", 
                    Bio = "Універсальний тренер з досвідом у різних напрямках фітнесу", 
                    Specialization = "Кардіо, Силові тренування, Йога" 
                }
            };
        }

        /// <summary>
        /// Отримати типи членства для заповнення бази даних
        /// </summary>
        public static List<MembershipType> GetMembershipTypes()
        {
            return new List<MembershipType>
            {
                new MembershipType { 
                    Name = "Базовий", 
                    Description = "Базове членство з обмеженим доступом до послуг", 
                    Price = AppConstants.MembershipCard.BasicPrice, 
                    DurationDays = AppConstants.MembershipCard.DefaultDurationDays,
                    VisitsLimit = AppConstants.MembershipCard.DefaultVisitsLimit
                },
                new MembershipType { 
                    Name = "Стандарт", 
                    Description = "Стандартне членство з доступом до всіх послуг в обмежений час", 
                    Price = AppConstants.MembershipCard.StandardPrice, 
                    DurationDays = AppConstants.MembershipCard.DefaultDurationDays,
                    VisitsLimit = AppConstants.MembershipCard.DefaultVisitsLimit * 2
                },
                new MembershipType { 
                    Name = "Преміум", 
                    Description = "Преміум членство з необмеженим доступом до всіх послуг клубу", 
                    Price = AppConstants.MembershipCard.PremiumPrice, 
                    DurationDays = AppConstants.MembershipCard.DefaultDurationDays * 2,
                    VisitsLimit = null  // null означає необмежену кількість відвідувань
                }
            };
        }
    }
} 