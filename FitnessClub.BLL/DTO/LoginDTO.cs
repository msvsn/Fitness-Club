using System.ComponentModel.DataAnnotations;

namespace FitnessClub.BLL.DTO
{
    /// <summary>
    /// DTO для входу в систему
    /// </summary>
    public class LoginDTO
    {
        /// <summary>
        /// Електронна пошта користувача
        /// </summary>
        [Required(ErrorMessage = "Електронна пошта обов'язкова")]
        [EmailAddress(ErrorMessage = "Невірний формат електронної пошти")]
        public string Email { get; set; }

        /// <summary>
        /// Пароль користувача
        /// </summary>
        [Required(ErrorMessage = "Пароль обов'язковий")]
        [MinLength(6, ErrorMessage = "Пароль повинен бути не менше 6 символів")]
        public string Password { get; set; }

        /// <summary>
        /// Запам'ятати користувача
        /// </summary>
        public bool RememberMe { get; set; }
    }
} 