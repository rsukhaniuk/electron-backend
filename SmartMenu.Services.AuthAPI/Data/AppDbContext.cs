using SmartMenu.Services.AuthAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SmartMenu.Services.AuthAPI.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed roles
            var adminRoleId = Guid.NewGuid().ToString();
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = adminRoleId,
                Name = "ADMIN",
                NormalizedName = "ADMIN"
            });

            // Seed admin user
            var adminUserId = Guid.NewGuid().ToString();
            var hasher = new PasswordHasher<ApplicationUser>();
            var adminUser = new ApplicationUser
            {
                Id = adminUserId,
                UserName = "admin@system.com",
                NormalizedUserName = "ADMIN@SYSTEM.COM",
                Email = "admin@system.com",
                NormalizedEmail = "ADMIN@SYSTEM.COM",
                EmailConfirmed = true,
                Name = "System Admin",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@123");

            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);

            // Assign admin role to admin user
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUserId
            });
        }
    }
}
