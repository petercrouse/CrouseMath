using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.IntegrationTests;
using CrouseMath.Application.Subjects.Commands.CreateSubject;
using CrouseMath.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CrouseMath.Application.UnitTests.Subjects.Commands.CreateSubject
{
    using static Testing;

    public class CreateSubjectCommandTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateSubjectCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task CreateSubjectCommandHandler_ShouldAddSubjectToContext()
        {
            var command = new CreateSubjectCommand{Name = "StaffLogic"};

            var subjectId = await SendAsync(command);

            var subject = await FindAsync<Subject>(subjectId);

            subject.Name.Should().Be(command.Name); 
        }
    }
}
