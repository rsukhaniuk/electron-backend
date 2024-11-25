using SmartMenu.Services.AuthAPI.Data;
using SmartMenu.Services.AuthAPI.Models;
using SmartMenu.Services.AuthAPI.Models.Dto;
using SmartMenu.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe.V2;

namespace SmartMenu.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IStoreService _storeService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext db, IJwtTokenGenerator jwtTokenGenerator,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IStoreService storeService)
        {
                _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
            _storeService = storeService;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    //create role if it does not exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;

        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user,loginRequestDto.Password);

            if(user==null || isValid == false)
            {
                return new LoginResponseDto() { User = null,Token="" };
            }

            //if user was found , Generate JWT Token
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user,roles);

            UserDto userDTO = new()
            {
                Email = user.Email,
                ID = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDTO,
                Token = token
            };

            return loginResponseDto;
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber,
                StoreId = registrationRequestDto.Role == "MANAGER" ? registrationRequestDto.StoreId : null
            };

            try
            {
                var result =await  _userManager.CreateAsync(user,registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);

                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        ID = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber
                    };

                    return "";

                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }

            }
            catch (Exception ex)
            {

            }
            return "Error Encountered";
        }

        public async Task<bool> EditAccount(EditAccountDto editAccountDto)
        {
            var user = await _userManager.FindByIdAsync(editAccountDto.UserId);
            if (user == null) return false;

            // Update name if provided
            if (!string.IsNullOrEmpty(editAccountDto.NewName))
                user.Name = editAccountDto.NewName;

            // Update phone number if provided
            if (!string.IsNullOrEmpty(editAccountDto.NewPhoneNumber))
                user.PhoneNumber = editAccountDto.NewPhoneNumber;

            // Update email if provided
            if (!string.IsNullOrEmpty(editAccountDto.NewEmail) && !editAccountDto.NewEmail.Equals(user.Email, StringComparison.OrdinalIgnoreCase))
            {
                // Check if the new email already exists
                var existingUser = await _userManager.FindByEmailAsync(editAccountDto.NewEmail);
                if (existingUser != null)
                {
                    return false; // Email is already taken
                }

                user.Email = editAccountDto.NewEmail;
                user.NormalizedEmail = editAccountDto.NewEmail.ToUpper();
                user.UserName = editAccountDto.NewEmail; // Optional: Update UserName if it mirrors Email
            }

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> DeleteAccount(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            // Perform the account deletion
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> ChangePassword(string userId, ChangePasswordDto changePasswordDto)
        {
            // Ensure the new password matches the confirmation password
            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmNewPassword)
            {
                return false;
            }

            // Find the user by ID
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            // Verify and update the password
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            return result.Succeeded;
        }

        public async Task<EditAccountDto?> GetUserDetailsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return null;

            return new EditAccountDto
            {
                UserId = user.Id,
                NewName = user.Name,
                NewEmail = user.Email,
                NewPhoneNumber = user.PhoneNumber
            };
        }

        public async Task<IEnumerable<UserDto>> GetUsersByRoleAsync(string role)
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync(role);

            return usersInRole.Select(user => new UserDto
            {
                ID = user.Id,
                Email = user.Email,
                Name = user.UserName,
                PhoneNumber = user.PhoneNumber
            }).ToList();
        }
    }
}
