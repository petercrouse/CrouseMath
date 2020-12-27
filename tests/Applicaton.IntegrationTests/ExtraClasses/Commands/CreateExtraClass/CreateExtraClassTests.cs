using System;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.ExtraClasses.Commands.CreateExtraClass;
using CrouseMath.Application.Subjects.Commands.CreateSubject;
using CrouseMath.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CrouseMath.Application.IntegrationTests.ExtraClasses.Commands.CreateExtraClass
{
    using static Testing;

    public class CreateExtraClassTests : TestBase
    {
        [Test]
        public async Task CreateExtraClass_GivenSuccessfulValidation_ShouldCreateSuccessfully()
        {
            // Arrange
            var userId = await RunAsDefaultUserAsync();
            var subjectId = await SendAsync(new CreateSubjectCommand { Name = "Wizardry" });

            var name = "Staff logic training";
            var date = new DateTime(2056, 1, 1);
            var duration = new TimeSpan(1, 0, 0);
            var size = 10;
            var price = 100;
           
            var command = new CreateExtraClassCommand
            {
                Name = name,
                Date = date,
                Duration = duration,
                Size = size,
                SubjectId = subjectId,
                Price = price
            };

            //Actual
            var extraClassId = await SendAsync(command);

            // Assert
            var extraClass = await FindAsync<ExtraClass>(extraClassId);

            extraClass.Name.Should().Be(name);
            extraClass.Date.Should().Be(date);
            extraClass.SubjectId.Should().Be(subjectId);
            extraClass.Duration.Should().Be(duration);
            extraClass.Size.Should().Be(size);
            extraClass.Price.Should().Be(price);
            extraClass.CreatedBy.Should().Be(userId);
            extraClass.Created.Should().BeCloseTo(DateTime.Now, 10000);
        }

        [Test]
        public void CreateExtraClass_GivenSuccessfulValidation_SubjectDoesNotExist_ShouldThrowNotFoundException()
        {
            // Arrange          
            var name = "Staff logic training";
            var date = new DateTime(2056, 1, 1);
            var duration = new TimeSpan(1, 0, 0);
            var size = 10;
            var subjectId = 99;
            var price = 100;

            var command = new CreateExtraClassCommand
            {
                Name = name,
                Date = date,
                Duration = duration,
                Size = size,
                SubjectId = subjectId,
                Price = price
            };

            //Assert
            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateExtraClassCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }
    }
}
