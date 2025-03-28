using System;

namespace FitnessClub.BLL.DTO
{
    /// <summary>
    /// DTO інформації про користувача
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Ідентифікатор користувача
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Ім'я користувача
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Прізвище користувача
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Повне ім'я користувача
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Електронна пошта користувача
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Номер телефону користувача
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Дата народження користувача
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Адреса користувача
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Місто користувача
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Дата реєстрації
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Останній вхід в систему
        /// </summary>
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// Роль користувача
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Ідентифікатор клієнта (якщо є)
        /// </summary>
        public int? ClientId { get; set; }

        /// <summary>
        /// Ідентифікатор тренера (якщо є)
        /// </summary>
        public int? TrainerId { get; set; }

        /// <summary>
        /// Чи активний користувач
        /// </summary>
        public bool IsActive { get; set; }
    }
} 