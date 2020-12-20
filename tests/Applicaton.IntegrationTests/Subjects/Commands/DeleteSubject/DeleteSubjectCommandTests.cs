using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.ExtraClasses.Commands.CreateExtraClass;
using CrouseMath.Application.IntegrationTests;
using CrouseMath.Application.Subjects.Commands.CreateSubject;
using CrouseMath.Application.Subjects.Commands.DeleteSubject;
using CrouseMath.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CrouseMath.Application.UnitTests.Subjects.Commands.DeleteSubject
{
    using static Testing;

    public class DeleteSubjectCommandTests : TestBase
    {
        [Test]
        public async Task DeleteSubjectCommand_ShouldThrowDeleteFailureExceptionAsync()
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

            var command = new DeleteSubjectCommand { Id = subjectId };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<DeleteFailureException>();
        }

        [Test]
        public void DeleteSubjectCommand_ShouldThrowNotFoundException()
        {
            var command = new DeleteSubjectCommand { Id = 99 };

            FluentActions.Invoking(() =>
             SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task DeleteSubjectCommand_ShouldDeleteSuccessfully()
        {
            var command = new CreateSubjectCommand { Name = "StaffLogic" };

            var subjectId = await SendAsync(command);

            await SendAsync(new DeleteSubjectCommand { Id = subjectId });

            var subject = await FindAsync<Subject>(subjectId);
            subject.Should().BeNull();
        }
    }
}
