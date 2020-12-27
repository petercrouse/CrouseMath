using System.Threading.Tasks;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Subjects.Commands.CreateSubject;
using CrouseMath.Application.Subjects.Queries.GetSubject;
using FluentAssertions;
using NUnit.Framework;

namespace CrouseMath.Application.IntegrationTests.Subjects.Queries
{
    using static Testing;

    public class GetSubjectQueryTests : TestBase
    {

        [Test]
        public async Task GetSubject()
        {
            var subjectId = await SendAsync(new CreateSubjectCommand { Name = "Wizardry"});

            var result = await SendAsync(new GetSubjectQuery { Id = subjectId });

            result.Should().BeOfType<SubjectViewModel>();
            result.Subject.Name.Should().Be("Wizardry");
        }

        [Test]
        public void GetSubjectQuery_ShouldThrowNotFoundException()
        {
            var command = new GetSubjectQuery { Id = 99 };

            FluentActions.Invoking(() =>
             SendAsync(command)).Should().Throw<NotFoundException>();
        }
    }
}
