using System;
using System.ComponentModel.DataAnnotations;

namespace FitnessClub.BLL.DTO
{
    /// <summary>
    /// DTO для реєстрації нового тренера
    /// </summary>
    public class RegisterTrainerDTO
    {
        /// <summary>
        /// Ім'я тренера
        /// </summary>
        [Required(ErrorMessage = "Ім'я обов'язкове")]
        [MinLength(2, ErrorMessage = "Ім'я повинно бути не менше 2 символів")]
        public string FirstName { get; set; }

        /// <summary>
        /// Прізвище тренера
        /// </summary>
        [Required(ErrorMessage = "Прізвище обов'язкове")]
        [MinLength(2, ErrorMessage = "Прізвище повинно бути не менше 2 символів")]
        public string LastName { get; set; }

        /// <summary>
        /// Електронна пошта тренера
        /// </summary>
        [Required(ErrorMessage = "Електронна пошта обов'язкова")]
        [EmailAddress(ErrorMessage = "Невірний формат електронної пошти")]
        public string Email { get; set; }

        /// <summary>
        /// Номер телефону тренера
        /// </summary>
        [Required(ErrorMessage = "Номер телефону обов'язковий")]
        [Phone(ErrorMessage = "Невірний формат номеру телефону")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Біографія тренера
        /// </summary>
        [StringLength(500, ErrorMessage = "Біографія не повинна перевищувати 500 символів")]
        public string Bio { get; set; }

        /// <summary>
        /// Спеціалізація тренера
        /// </summary>
        [StringLength(200, ErrorMessage = "Спеціалізація не повинна перевищувати 200 символів")]
        public string Specialization { get; set; }

        /// <summary>
        /// Дата народження тренера
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Адреса тренера
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Місто тренера
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