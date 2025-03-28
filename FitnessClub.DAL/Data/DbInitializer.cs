using FitnessClub.DAL.Constants;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace FitnessClub.DAL.Data
{
    public static class DbInitializer
    {
        /// <summary>
        /// Ініціалізує базу даних початковими даними
        /// </summary>
        public static void Initialize(FitnessClubDbContext context, ILogger logger = null)
        {
            try
            {
                // Перевіримо, чи база даних порожня
                if (context.Locations.Any() || context.FitnessClubs.Any() || context.ClassTypes.Any())
                {
                    logger?.LogInformation("База даних уже містить дані. Пропускаємо ініціалізацію.");
                    return; // База даних уже ініціалізована
                }

                logger?.LogInformation("Починаємо ініціалізацію бази даних...");
                
                // Додаємо локації
                var locations = SeedData.GetLocations();
                context.Locations.AddRange(locations);
                context.SaveChanges();
                logger?.LogInformation($"Додано {locations.Count} локацій");
                
                // Додаємо фітнес клуби
                var fitnessClubs = SeedData.GetFitnessClubs(locations);
                context.FitnessClubs.AddRange(fitnessClubs);
                context.SaveChanges();
                logger?.LogInformation($"Додано {fitnessClubs.Count} фітнес клубів");
                
                // Додаємо типи класів
                var classTypes = SeedData.GetClassTypes();
                context.ClassTypes.AddRange(classTypes);
                context.SaveChanges();
                logger?.LogInformation($"Додано {classTypes.Count} типів класів");
                
                // Додаємо тренерів
                var trainers = SeedData.GetTrainers();
                context.Trainers.AddRange(trainers);
                context.SaveChanges();
                logger?.LogInformation($"Додано {trainers.Count} тренерів");
                
                // Додаємо типи членства
                var membershipTypes = SeedData.GetMembershipTypes();
                context.MembershipTypes.AddRange(membershipTypes);
                context.SaveChanges();
                logger?.LogInformation($"Додано {membershipTypes.Count} типів членства");
                
                // Додаємо розклад занять
                // Створюємо розклад для кожного фітнес клубу
                var random = new Random();
                
                foreach (var club in fitnessClubs)
                {
                    // Для кожного дня тижня
                    for (int day = AppConstants.ClassSchedule.MinDayOfWeek; day <= AppConstants.ClassSchedule.MaxDayOfWeek; day++)
                    {
                        // Додаємо 2-3 заняття на кожен день
                        int classesPerDay = random.Next(2, 4);
                        
                        for (int i = 0; i < classesPerDay; i++)
                        {
                            var classType = classTypes[random.Next(classTypes.Count)];
                            var trainer = trainers[random.Next(trainers.Count)];
                            
                            // Генеруємо випадковий час (з 8:00 до 19:00)
                            int hour = random.Next(8, 20);
                            
                            // Час початку та тривалість
                            var startTime = new TimeSpan(hour, 0, 0);
                            var endTime = startTime.Add(TimeSpan.FromMinutes(classType.Duration));
                            
                            context.ClassSchedules.Add(new Models.ClassSchedule
                            {
                                ClassTypeId = classType.Id,
                                DayOfWeek = day,
                                StartTime = startTime,
                                EndTime = endTime,
                                FitnessClubId = club.Id,
                                TrainerId = trainer.Id,
                                IsRecurring = true
                            });
                        }
                    }
                }
                
                context.SaveChanges();
                logger?.LogInformation("Додано розклади занять");
                
                logger?.LogInformation("Ініціалізацію бази даних завершено успішно");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Помилка при ініціалізації бази даних");
                throw;
            }
        }
    }
} 