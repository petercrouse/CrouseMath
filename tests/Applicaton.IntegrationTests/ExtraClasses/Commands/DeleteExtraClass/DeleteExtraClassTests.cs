using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.ExtraClasses.Commands.DeleteExtraClass;
using CrouseMath.Application.IntegrationTests;
using NUnit.Framework;
using FluentAssertions;
using CrouseMath.Domain.Entities;
using System;
using System.Collections.Generic;
using CrouseMath.Application.ExtraClasses.Commands.CreateExtraClass;
using CrouseMath.Application.Bookings.Commands.CreateBooking;
using CrouseMath.Application.Subjects.Commands.CreateSubject;

namespace CrouseMath.Application.UnitTests.ExtraClasses.Commands.DeleteExtraClass
{
    using static Testing;

    public class DeleteExtraClassTests : TestBase
    {
        [Test]
        public async Task DeleteExtraClass_ShouldDeleteSuccessfully()
        {
            var subjectId = await SendAsync(new CreateSubjectCommand { Name = "StaffLogic" });
            var extraClassId = await SendAsync(new CreateExtraClassCommand
            {
                SubjectId = subjectId,
                Date = DateTime.Now,
                Duration = TimeSpan.FromMinutes(60),
                Name = "StaffLogic",
                Price = 100,
                Size = 2
            });

            var command = new DeleteExtraClassCommand { Id = extraClassId };

            await SendAsync(command);

            var extraClass = await FindAsync<ExtraClass>(extraClassId);
            extraClass.Should().BeNull();
        }

        [Test]
        public void DeleteExtraClassCommandHandler_GivenSuccessfulValidation_ShouldThrowNotFoundException()
        {
            var command = new DeleteExtraClassCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task DeleteExtraClass_GivenSuccessfulValidation_ShouldThrowDeleteFailureException()
        {
            var subjectId = await SendAsync(new CreateSubjectCommand{ Name = "Wizardry" });

            var extraClassId = await SendAsync(new CreateExtraClassCommand
            {
                SubjectId = subjectId,
                Date = DateTime.Now,
                Duration = TimeSpan.FromMinutes(60),
                Name = "StaffLogic",
                Price = 100,
                Size = 2
            });

            await SendAsync(new CreateBookingCommand { ExtraClassId = extraClassId });

            var command = new DeleteExtraClassCommand { Id = extraClassId };

            // Assert
            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<DeleteFailureException>();
        }
    }
}
