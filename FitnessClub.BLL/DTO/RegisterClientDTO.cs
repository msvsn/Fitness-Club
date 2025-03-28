using System;
using System.ComponentModel.DataAnnotations;

namespace FitnessClub.BLL.DTO
{
    /// <summary>
    /// DTO для реєстрації нового клієнта
    /// </summary>
    public class RegisterClientDTO
    {
        /// <summary>
        /// Ім'я клієнта
        /// </summary>
        [Required(ErrorMessage = "Ім'я обов'язкове")]
        [MinLength(2, ErrorMessage = "Ім'я повинно бути не менше 2 символів")]
        public string FirstName { get; set; }

        /// <summary>
        /// Прізвище клієнта
        /// </summary>
        [Required(ErrorMessage = "Прізвище обов'язкове")]
        [MinLength(2, ErrorMessage = "Прізвище повинно бути не менше 2 символів")]
        public string LastName { get; set; }

        /// <summary>
        /// Електронна пошта клієнта
        /// </summary>
        [Required(ErrorMessage = "Електронна пошта обов'язкова")]
        [EmailAddress(ErrorMessage = "Невірний формат електронної пошти")]
        public string Email { get; set; }

        /// <summary>
        /// Номер телефону клієнта
        /// </summary>
        [Required(ErrorMessage = "Номер телефону обов'язковий")]
        [Phone(ErrorMessage = "Невірний формат номеру телефону")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Дата народження клієнта
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Адреса клієнта
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Місто клієнта
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Пароль для облікового запису
        /// </summary>
        [Required(ErrorMessage = "Пароль обов'язковий")]
        [MinLength(6, ErrorMessage = "Пароль повинен бути не менше 6 символів")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Підтвердження пароля
        /// </summary>
        [Required(ErrorMessage = "Підтвердження пароля обов'язкове")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
} 