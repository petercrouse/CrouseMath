using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.ExtraClasses.Queries.GetExtraClass;
using CrouseMath.Infrastructure.Persistence;
using CrouseMath.Application.IntegrationTests;
using NUnit.Framework;
using CrouseMath.Application.Subjects.Commands.CreateSubject;
using System;
using CrouseMath.Application.ExtraClasses.Commands.CreateExtraClass;
using FluentAssertions;

namespace CrouseMath.Application.UnitTests.ExtraClasses.Queries
{
    using static Testing;

    public class GetExtraClassQueryTests : TestBase
    {
        [Test]
        public async Task GetExtraClass()
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

            var query = new GetExtraClassQuery { Id = extraClassId };

            var result = await SendAsync(query);

            result.Should().BeOfType<ExtraClassViewModel>();
            result.ExtraClass.Id.Should().Be(extraClassId);
            result.ExtraClass.Name.Should().Be(name);
        }
    }
}
