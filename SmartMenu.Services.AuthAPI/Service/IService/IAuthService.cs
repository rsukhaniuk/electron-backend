using SmartMenu.Services.AuthAPI.Models.Dto;

namespace SmartMenu.Services.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
        Task<bool> EditAccount(EditAccountDto editAccountDto);
        Task<bool> DeleteAccount(string email);
        Task<bool> ChangePassword(string userId, ChangePasswordDto changePasswordDto);
        Task<EditAccountDto?> GetUserDetailsAsync(string userId);


    }
}
