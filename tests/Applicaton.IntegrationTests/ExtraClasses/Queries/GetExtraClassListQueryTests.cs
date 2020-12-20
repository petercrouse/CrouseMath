using System.Linq;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.ExtraClasses.Queries.GetExtraClassList;
using CrouseMath.Infrastructure.Persistence;
using CrouseMath.Application.IntegrationTests;
using NUnit.Framework;
using CrouseMath.Domain.Entities;
using System;
using FluentAssertions;
using CrouseMath.Application.Subjects.Commands.CreateSubject;

namespace CrouseMath.Application.UnitTests.ExtraClasses.Queries
{
    using static Testing;
    public class GetExtraClassListQueryTests : TestBase
    {
        [Test]
        public async Task GetExtraClassesList()
        {
            var subjectId = await SendAsync(new CreateSubjectCommand { Name = "StaffLogic" });

            await AddAsync(new ExtraClass
            {
                Name = "Staff logic training",
                Date = new DateTime(2056, 1, 1),
                Duration = new TimeSpan(1, 0, 0),
                Size = 10,
                SubjectId = subjectId,
                TeacherId = "111111",
                Price = 100,
            });

            await AddAsync(new ExtraClass
            {
                Name = "Staff logic Theory",
                Date = new DateTime(2056, 1, 1),
                Duration = new TimeSpan(1, 0, 0),
                Size = 10,
                SubjectId = subjectId,
                TeacherId = "111111",
                Price = 100,
            });

            var query = new GetExtraClassListQuery();

            var result = await SendAsync(query);

            result.Should().BeOfType<ExtraClassListViewModel>();
            result.ExtraClasses.Should().HaveCount(2);
        }
    }
}
