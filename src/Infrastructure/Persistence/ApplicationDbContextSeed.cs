﻿using CrouseMath.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using CrouseMath.Domain.Entities;
using System;

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
            if (!context.ExtraClasses.Any())
            {
                context.Subjects.AddRange(new[]
                {
                    new Subject {Name = "Magic"},
                    new Subject {Name = "Staff Logic"}
                });

                context.ExtraClasses.Add(new ExtraClass
                {
                    Name = "Math Algebra",
                    Size = 5,
                    SubjectId = 1,
                    Date = DateTime.Now,
                    Duration = new TimeSpan(1, 0, 0),
                    IsClassFull = false,
                    Price = 100,
                });

                await context.SaveChangesAsync();
            }
        }
    }
}