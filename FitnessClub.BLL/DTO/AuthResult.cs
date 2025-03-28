using System.Collections.Generic;

namespace FitnessClub.BLL.DTO
{
    /// <summary>
    /// Результат операції аутентифікації
    /// </summary>
    public class AuthResult
    {
        public AuthResult()
        {
            Errors = new List<string>();
        }

        /// <summary>
        /// Успішна операція чи ні
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Токен доступу (якщо потрібен)
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Список помилок, що виникли під час операції
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// ID користувача
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Роль користувача
        /// </summary>
        public string Role { get; set; }
    }
} 