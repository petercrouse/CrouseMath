using CrouseMath.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using CrouseMath.Domain.Entities;

namespace CrouseMath.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser
                {UserName = "administrator@localhost", Email = "administrator@localhost"};

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "Administrator1!");
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.Students.Any())
            {
                context.Students.AddRange(new[]
                {
                    new Student {LastName = "Baggins", FirstName = "Frodo", Email = "fbaggins@theshire.com"},
                    new Student {LastName = "Baggins", FirstName = "Bilbo", Email = "bbaggins@theshire,com"},
                    new Student {LastName = "Gam Gee", FirstName = "Sam", Email = "sgamegee@theshire.com"},
                    new Student {LastName = "BrandyBuck", FirstName = "Merry", Email = "mbrandybuck@theshire.com"}
                });

                context.Subjects.AddRange(new[]
                {
                    new Subject {Name = "Magic"},
                    new Subject {Name = "Staff Logic"}
                });

                context.Teachers.AddRange(new[]
                {
                    new Teacher
                    {
                        LastName = "The Grey",
                        FirstName = "Gandalf",
                        Email = "gandalf@wizzardcouncil.com",
                    },
                    new Teacher
                    {
                        LastName = "The White",
                        FirstName = "Saruman",
                        Email = "saurman@wizzardcouncil.com",
                    }
                });

                await context.SaveChangesAsync();
            }
        }
    }
}