using Microsoft.AspNetCore.Identity;

namespace SmartMenu.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string? StoreId { get; set; }
    }
}
