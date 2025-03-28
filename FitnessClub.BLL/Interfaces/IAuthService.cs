using FitnessClub.BLL.DTO;
using System.Threading.Tasks;

namespace FitnessClub.BLL.Services.Interfaces
{
    /// <summary>
    /// Інтерфейс для сервісу автентифікації
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Реєстрація нового клієнта
        /// </summary>
        /// <param name="model">Дані для реєстрації</param>
        /// <returns>Результат реєстрації</returns>
        Task<AuthResult> RegisterClientAsync(RegisterClientDTO model);
        
        /// <summary>
        /// Реєстрація нового тренера (доступно тільки через адмін панель)
        /// </summary>
        /// <param name="model">Дані для реєстрації</param>
        /// <returns>Результат реєстрації</returns>
        Task<AuthResult> RegisterTrainerAsync(RegisterTrainerDTO model);
        
        /// <summary>
        /// Вхід користувача
        /// </summary>
        /// <param name="model">Дані для входу</param>
        /// <returns>Результат входу</returns>
        Task<AuthResult> LoginAsync(LoginDTO model);
        
        /// <summary>
        /// Вихід користувача
        /// </summary>
        Task LogoutAsync();
        
        /// <summary>
        /// Перевірка чи користувач є клієнтом
        /// </summary>
        /// <returns>true якщо користувач є клієнтом, інакше false</returns>
        Task<bool> IsCurrentUserClientAsync();
        
        /// <summary>
        /// Перевірка чи вказаний користувач є клієнтом
        /// </summary>
        /// <param name="userId">Ідентифікатор користувача</param>
        /// <returns>true якщо користувач є клієнтом, інакше false</returns>
        Task<bool> IsUserClientAsync(string userId);
        
        /// <summary>
        /// Отримання даних поточного авторизованого користувача
        /// </summary>
        Task<UserDTO> GetCurrentUserAsync();
        
        /// <summary>
        /// Отримання даних користувача за його ID
        /// </summary>
        Task<UserDTO> GetUserByIdAsync(string userId);
        
        /// <summary>
        /// Отримання профілю користувача для редагування
        /// </summary>
        /// <param name="userId">Ідентифікатор користувача</param>
        /// <returns>Дані профілю користувача</returns>
        Task<UserProfileDTO> GetUserProfileAsync(string userId);
        
        /// <summary>
        /// Оновлення профілю користувача
        /// </summary>
        /// <param name="model">Дані для оновлення</param>
        /// <returns>Результат оновлення</returns>
        Task<AuthResult> UpdateUserProfileAsync(UserProfileDTO model);
        
        /// <summary>
        /// Перевірка чи користувач автентифікований
        /// </summary>
        /// <returns>true якщо користувач автентифікований, інакше false</returns>
        Task<bool> IsAuthenticatedAsync();
        
        /// <summary>
        /// Отримання імені поточного користувача
        /// </summary>
        /// <returns>Ім'я користувача</returns>
        Task<string> GetCurrentUserNameAsync();
        
        /// <summary>
        /// Отримання ідентифікатора поточного користувача
        /// </summary>
        /// <returns>Ідентифікатор користувача</returns>
        Task<string> GetCurrentUserIdAsync();
        
        /// <summary>
        /// Перевірка чи поточний користувач має вказану роль
        /// </summary>
        /// <param name="role">Назва ролі</param>
        /// <returns>true якщо користувач має вказану роль, інакше false</returns>
        Task<bool> IsCurrentUserInRoleAsync(string role);
    }
} 