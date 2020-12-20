using System;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Bookings.Commands.CreateBooking;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.ExtraClasses.Commands.CreateExtraClass;
using CrouseMath.Application.ExtraClasses.Commands.UpdateExtraClass;
using CrouseMath.Application.IntegrationTests;
using CrouseMath.Application.Subjects.Commands.CreateSubject;
using CrouseMath.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CrouseMath.Application.UnitTests.ExtraClasses.Commands.UpdateExtraClass
{
    using static Testing;

    public class UpdateExtraClassTests : TestBase
    {
        [Test]
        public async Task UpdateExtraClass_ShouldUpdateSuccessfully()
        {
            // Arrange          
            var name = "Staff logic training";
            var date = new DateTime(2056, 1, 1);
            var duration = new TimeSpan(1, 0, 0);
            var size = 3;
            var price = 100;

            var teacherId = await RunAsUserAsync("teacher@local", "teacher1234!");

            var subjectId = await SendAsync(new CreateSubjectCommand { Name = "StaffLogic" });
            var extraClassId = await SendAsync(new CreateExtraClassCommand
            {
                SubjectId = subjectId,
                Date = date,
                Duration = duration,
                Name = name,
                Price = price,
                Size = size,
            });

            var newName = "Staff Logic Theory";

            var command = new UpdateExtraClassCommand
            {
                Id = extraClassId,
                SubjectId = subjectId,
                TeacherId = teacherId,
                Date = date,
                Size = size,
                Duration = duration,
                Name = newName,
                Price = price
            };

            await SendAsync(command);

            var extraClass = await FindAsync<ExtraClass>(extraClassId);

            extraClass.Name.Should().Be(newName);
            extraClass.Price.Should().Be(price);
            extraClass.Size.Should().Be(size);
            extraClass.SubjectId.Should().Be(subjectId);
            extraClass.TeacherId.Should().Be(teacherId);
            extraClass.LastModifiedBy.Should().Be(teacherId);
        }

        [Test]
        public void UpdateExtraClass_GivenSuccessfulValidation_ShouldThrowNotFoundException()
        {
            var command = new UpdateExtraClassCommand
            {
                Id = 999,
                Name = "Staff logic training",
                Date = new DateTime(2056, 1, 1),
                Duration = new TimeSpan(1, 0, 0),
                Size = 10,
                SubjectId = 1,
                TeacherId = "111111",
                Price = 100,
            };

            // Assert
            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task UpdateExtraClass_GivenSuccessfulValidation_TeacherNotFound_ShouldThrowNotFoundException()
        {
            var teacherId = await RunAsUserAsync("teacher@local", "teacher1234!");

            var subjectId = await SendAsync(new CreateSubjectCommand { Name = "StaffLogic" });
            var extraClassId = await SendAsync(new CreateExtraClassCommand
            {
                SubjectId = subjectId,
                Date = DateTime.Now,
                Duration = TimeSpan.FromMinutes(60),
                Name = "StaffLogic",
                Price = 100,
                Size = 2,     
            });

            var command = new UpdateExtraClassCommand
            {
                Id = extraClassId,
                Date = DateTime.Now,
                Duration = TimeSpan.FromMinutes(60),
                Name = "StaffLogic",
                Price = 100,
                Size = 2,
                TeacherId = "boop@local"
            };

            // Assert
            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task UpdateExtraClass_GivenSuccessfulValidation_ClassSizeTooSmall_ShouldThrowClassSizeIsTooSmallForCurrentBookingsException()
        {
            // Arrange          
            var name = "Staff logic training";
            var date = new DateTime(2056, 1, 1);
            var duration = new TimeSpan(1, 0, 0);
            var size = 3;
            var price = 100;

            var teacherId = await RunAsUserAsync("teacher@local", "teacher1234!");

            var subjectId = await SendAsync(new CreateSubjectCommand { Name = "StaffLogic" });
            var extraClassId = await SendAsync(new CreateExtraClassCommand
            {
                SubjectId = subjectId,
                Date = date,
                Duration = duration,
                Name = name,
                Price = price,
                Size = size,
            });

            await SendAsync(new CreateBookingCommand { ExtraClassId = extraClassId});

            await RunAsDefaultUserAsync();

            await SendAsync(new CreateBookingCommand { ExtraClassId = extraClassId });

            //Assert
            var command = new UpdateExtraClassCommand
            {
                Date = date,
                Size = 1,
                SubjectId = subjectId,
                Duration = duration,
                Id = extraClassId,
                Name = name,
                Price = price,
                TeacherId = teacherId
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ClassSizeIsTooSmallForCurrentBookingsException>();
        }
    }
}
