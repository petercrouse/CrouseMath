using System;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.IntegrationTests;
using CrouseMath.Application.Subjects.Commands.CreateSubject;
using CrouseMath.Application.Subjects.Commands.UpdateSubject;
using CrouseMath.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CrouseMath.Application.UnitTests.Subjects.Commands.UpdateSubject
{
    using static Testing;

    public class UpdateSubjectCommandTests : TestBase
    {
        [Test]
        public void UpdateSubjectCommand_ShouldThrowNotFoundException()
        {
            var command = new UpdateSubjectCommand
            {
                Id = 99,
                Name = "VoidMagic"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task UpdateSubjectCommand_ShouldUpdateSubject()
        {
            var userId = await RunAsDefaultUserAsync();

            var subjectId = await SendAsync(new CreateSubjectCommand { Name = "StaffLogic" });

            var command = new UpdateSubjectCommand
            {
                Name = "VoidMagic"
            };

            await SendAsync(command);

            var subject = await FindAsync<Subject>(subjectId);

            subject.Name.Should().Be("VoidMagic");
            subject.LastModifiedBy.Should().NotBeNull();
            subject.LastModifiedBy.Should().Be(userId);
            subject.LastModified.Should().NotBeNull();
            subject.LastModified.Should().BeCloseTo(DateTime.Now, 10000);
        }
    }
}
