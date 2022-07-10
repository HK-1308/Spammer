using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestTask.Data.Models;
using TestTask.Models;

namespace TestTask
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Job> Job { get; set; } 
        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleId = Guid.NewGuid().ToString();
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "User", NormalizedName = "USER"},
                new IdentityRole {Id = adminRoleId, Name = "Administrator", NormalizedName="ADMINISTRATOR"});


            string adminId = Guid.NewGuid().ToString();
            var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = adminId,
                UserName = "vtokarionok@gmail.com",
                NormalizedUserName = "VTOKARIONOK@GMAIL.COM",
                Email = "vtokarionok@gmail.com",
                NormalizedEmail = "VTOKARIONOK@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "fkj73hgV$"),
                SecurityStamp = string.Empty
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminId
            });

        }
    }
}
