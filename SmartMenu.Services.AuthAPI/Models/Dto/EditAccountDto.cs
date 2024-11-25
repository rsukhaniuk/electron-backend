namespace SmartMenu.Services.AuthAPI.Models.Dto
{
    public class EditAccountDto
    {
        public string UserId { get; set; } // The unique identifier of the user
        public string? NewName { get; set; }
        public string? NewPhoneNumber { get; set; }
        public string? NewEmail { get; set; } // New email address to update
        public string? NewRole { get; set; } // New role to update
    }
}
