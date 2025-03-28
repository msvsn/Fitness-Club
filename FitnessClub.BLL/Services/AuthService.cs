using FitnessClub.BLL.DTO;
using FitnessClub.BLL.Services.Interfaces;
using FitnessClub.DAL.Models;
using FitnessClub.DAL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitnessClub.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<AuthResult> RegisterClientAsync(RegisterClientDTO model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                DateOfBirth = model.DateOfBirth,
                Address = model.Address,
                City = model.City,
                RegistrationDate = DateTime.Now,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return new AuthResult 
                { 
                    Success = false, 
                    Errors = result.Errors.Select(e => e.Description).ToList() 
                };
            }

            await _userManager.AddToRoleAsync(user, "Client");

            var client = new Client
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                DateOfBirth = model.DateOfBirth,
                RegistrationDate = DateTime.Now,
                UserId = user.Id
            };

            var clientRepository = _unitOfWork.GetRepository<Client>();
            clientRepository.Add(client);
            await _unitOfWork.CompleteAsync();

            user.ClientId = client.Id;
            await _userManager.UpdateAsync(user);

            return new AuthResult { Success = true };
        }

        public async Task<AuthResult> RegisterTrainerAsync(RegisterTrainerDTO model)
        {
            // Реалізація реєстрації тренера (тільки для адміністратора)
            throw new NotImplementedException("Реєстрація тренера доступна тільки через адмін панель");
        }

        public async Task<AuthResult> LoginAsync(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new AuthResult 
                { 
                    Success = false, 
                    Errors = new[] { "Користувач з такою електронною поштою не знайдений" }.ToList() 
                };
            }

            if (!user.IsActive)
            {
                return new AuthResult 
                { 
                    Success = false, 
                    Errors = new[] { "Обліковий запис неактивний" }.ToList() 
                };
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (!result.Succeeded)
            {
                return new AuthResult 
                { 
                    Success = false, 
                    Errors = new[] { "Невірний пароль" }.ToList() 
                };
            }

            user.LastLoginDate = DateTime.Now;
            await _userManager.UpdateAsync(user);

            return new AuthResult { Success = true };
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> IsCurrentUserClientAsync()
        {
            var user = await GetCurrentUserAsync();
            return user != null && user.ClientId.HasValue;
        }

        public async Task<bool> IsUserClientAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user != null && user.ClientId.HasValue;
        }

        public async Task<UserDTO> GetCurrentUserAsync()
        {
            var userId = await GetCurrentUserIdAsync();
            if (string.IsNullOrEmpty(userId))
                return null;

            return await GetUserByIdAsync(userId);
        }

        public async Task<UserDTO> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                Address = user.Address,
                City = user.City,
                RegistrationDate = user.RegistrationDate,
                LastLoginDate = user.LastLoginDate,
                Role = roles.FirstOrDefault(),
                ClientId = user.ClientId,
                TrainerId = user.TrainerId,
                IsActive = user.IsActive
            };
        }

        public async Task<UserProfileDTO> GetUserProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return null;

            return new UserProfileDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                Address = user.Address,
                City = user.City
            };
        }

        public async Task<AuthResult> UpdateUserProfileAsync(UserProfileDTO model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return new AuthResult 
                { 
                    Success = false, 
                    Errors = new[] { "Користувач не знайдений" }.ToList() 
                };
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.DateOfBirth = model.DateOfBirth;
            user.Address = model.Address;
            user.City = model.City;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return new AuthResult 
                { 
                    Success = false, 
                    Errors = result.Errors.Select(e => e.Description).ToList() 
                };
            }

            // Якщо користувач хоче змінити пароль
            if (!string.IsNullOrEmpty(model.CurrentPassword) && !string.IsNullOrEmpty(model.NewPassword))
            {
                var passwordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (!passwordResult.Succeeded)
                {
                    return new AuthResult 
                    { 
                        Success = false, 
                        Errors = passwordResult.Errors.Select(e => e.Description).ToList() 
                    };
                }
            }

            // Оновлюємо профіль клієнта або тренера (якщо є)
            if (user.ClientId.HasValue)
            {
                var clientRepository = _unitOfWork.GetRepository<Client>();
                var client = await clientRepository.GetByIdAsync(user.ClientId.Value);
                if (client != null)
                {
                    client.FirstName = model.FirstName;
                    client.LastName = model.LastName;
                    client.PhoneNumber = model.PhoneNumber;
                    client.DateOfBirth = model.DateOfBirth;
                    clientRepository.Update(client);
                    await _unitOfWork.CompleteAsync();
                }
            }
            else if (user.TrainerId.HasValue)
            {
                var trainerRepository = _unitOfWork.GetRepository<Trainer>();
                var trainer = await trainerRepository.GetByIdAsync(user.TrainerId.Value);
                if (trainer != null)
                {
                    trainer.FirstName = model.FirstName;
                    trainer.LastName = model.LastName;
                    trainer.PhoneNumber = model.PhoneNumber;
                    trainerRepository.Update(trainer);
                    await _unitOfWork.CompleteAsync();
                }
            }

            return new AuthResult { Success = true };
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }

        public async Task<string> GetCurrentUserNameAsync()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            
            if (string.IsNullOrEmpty(userName))
            {
                var userId = await GetCurrentUserIdAsync();
                if (!string.IsNullOrEmpty(userId))
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        return $"{user.FirstName} {user.LastName}";
                    }
                }
            }

            return userName;
        }

        public async Task<string> GetCurrentUserIdAsync()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        public async Task<bool> IsCurrentUserInRoleAsync(string role)
        {
            return _httpContextAccessor.HttpContext?.User?.IsInRole(role) ?? false;
        }
    }
} 