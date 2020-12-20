using System.Threading.Tasks;
using CrouseMath.Application.IntegrationTests;
using CrouseMath.Application.Subjects.Queries.GetSubjects;
using CrouseMath.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CrouseMath.Application.UnitTests.Subjects.Queries
{
    using static Testing;

    public class GetSubjectListQueryTests : TestBase
    {
        [Test]
        public async Task GetSubjectList()
        {
            await AddAsync(new Subject { Name = "StaffLogic" });
            await AddAsync(new Subject { Name = "Wizardy" });

            var query = new GetSubjectListQuery();

            var result = await SendAsync(query);

            result.Should().BeOfType<SubjectListViewModel>();

            result.Subjects.Should().HaveCount(2);
        }

    }
}
