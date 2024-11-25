namespace SmartMenu.Services.AuthAPI.Models.Dto
{
    public class ManagerRegistrationRequestDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int StoreId { get; set; } 
    }
}
