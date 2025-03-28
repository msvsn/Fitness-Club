using System;
using System.ComponentModel.DataAnnotations;

namespace FitnessClub.BLL.DTO
{
    /// <summary>
    /// DTO для оновлення профілю користувача
    /// </summary>
    public class UserProfileDTO
    {
        /// <summary>
        /// Ідентифікатор користувача
        /// </summary>
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// Ім'я користувача
        /// </summary>
        [Required(ErrorMessage = "Ім'я обов'язкове")]
        [MinLength(2, ErrorMessage = "Ім'я повинно бути не менше 2 символів")]
        public string FirstName { get; set; }

        /// <summary>
        /// Прізвище користувача
        /// </summary>
        [Required(ErrorMessage = "Прізвище обов'язкове")]
        [MinLength(2, ErrorMessage = "Прізвище повинно бути не менше 2 символів")]
        public string LastName { get; set; }

        /// <summary>
        /// Номер телефону користувача
        /// </summary>
        [Phone(ErrorMessage = "Невірний формат номеру телефону")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Дата народження користувача
        /// </summary>
        [DataType(DataType.Date)]
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
        /// Поточний пароль користувача (потрібен для зміни пароля)
        /// </summary>
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        /// <summary>
        /// Новий пароль користувача
        /// </summary>
        [MinLength(6, ErrorMessage = "Пароль повинен бути не менше 6 символів")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        /// <summary>
        /// Підтвердження нового пароля
        /// </summary>
        [Compare("NewPassword", ErrorMessage = "Паролі не співпадають")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
} 