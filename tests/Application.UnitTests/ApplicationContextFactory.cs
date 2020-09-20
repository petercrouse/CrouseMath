using System;
using System.Collections.Generic;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Domain.Entities;
using CrouseMath.Infrastructure.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CrouseMath.Application.UnitTests
{
    public class ApplicationContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;

            var mockDateTime = new Mock<IDateTime>()
                .Setup(x => x.Now ).Returns(DateTime.Now);
            
            var context = new ApplicationDbContext(options, );

            context.Database.EnsureCreated();

            context.Students.AddRange(new[]
            {
                new Student {Id = 1, LastName = "Baggins", FirstName = "Frodo", Email = "fbaggins@theshire.com"},
                new Student {Id = 2, LastName = "Baggins", FirstName = "Bilbo", Email = "bbaggins@theshire,com"},
                new Student {Id = 3, LastName = "Gam Gee", FirstName = "Sam", Email = "sgamegee@theshire.com"},
                new Student
                {
                    Id = 4, LastName = "BrandyBuck", FirstName = "Merry", Email = "mbrandybuck@theshire.com"
                }
            });

            context.Teachers.AddRange(new[]
            {
                new Teacher
                {
                    Id = 1,
                    LastName = "The Grey",
                    FirstName = "Gandalf",
                    Email = "gandalf@wizzardcouncil.com",
                    TeachingSubjects = new List<TeacherSubject>
                    {
                        new TeacherSubject
                        {
                            TeacherId = 1,
                            SubjectId = 1
                        }
                    }
                },
                new Teacher
                {
                    Id = 2,
                    LastName = "The White",
                    FirstName = "Saruman",
                    Email = "saurman@wizzardcouncil.com",
                    TeachingSubjects = new List<TeacherSubject>
                    {
                        new TeacherSubject
                        {
                            TeacherId = 2,
                            SubjectId = 2
                        },
                        new TeacherSubject
                        {
                            TeacherId = 2,
                            SubjectId = 1
                        }
                    }
                }
            });

            context.Subjects.AddRange(new[]
            {
                new Subject {Id = 1, Name = "Magic"},
                new Subject {Id = 2, Name = "Staff Logic"}
            });

            context.Bookings.AddRange(new[]
            {
                new Booking {Id = 1, StudentId = 1, ExtraClassId = 1, BookingPrice = 100},
                new Booking {Id = 2, StudentId = 2, ExtraClassId = 1, BookingPrice = 100},
                new Booking {Id = 3, StudentId = 4, ExtraClassId = 1, BookingPrice = 100},
                new Booking {Id = 4, StudentId = 1, ExtraClassId = 3, BookingPrice = 100}
            });

            context.ExtraClasses.AddRange(new[]
            {
                new ExtraClass
                {
                    Id = 1, TeacherId = 1, SubjectId = 1, Size = 2, Duration = new TimeSpan(1, 00, 00),
                    Price = 100, Date = new DateTime(2555, 1, 1), IsClassFull = true, Name = "How to be a wizzard"
                },
                new ExtraClass
                {
                    Id = 2, TeacherId = 2, SubjectId = 2, Size = 4, Duration = new TimeSpan(1, 00, 00),
                    Price = 100, Date = new DateTime(2555, 1, 1), IsClassFull = false, Name = "Staff Logic"
                },
                new ExtraClass
                {
                    Id = 3, TeacherId = 2, SubjectId = 2, Size = 4, Duration = new TimeSpan(1, 00, 00),
                    Price = 100, Date = new DateTime(2555, 1, 1), IsClassFull = true, Name = "Wizzard Magic"
                }
            });

            context.SaveChanges();

            return context;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.CloseConnection();
            context.Dispose();
        }
    }
}