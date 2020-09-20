using System;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.ExtraClasses.Commands.CreateExtraClass;
using CrouseMath.Application.UnitTests;
using Xunit;

namespace CrouseMath.Application.UnitTests.ExtraClasses.Commands.CreateExtraClass
{
    public class CreateExtraClassHandlerTests : CommandTestBase
    {
        [Fact]
        public async Task CreateExtraClass_GivenSuccessfulValidation_ShouldThrowTeacherDoesNotTeachSubjectException()
        {
            // Arrange          
            var sut = new CreateExtraClassCommand.CreateExtraClassCommandHandler(Context);
            var name = "Staff logic training";
            var date = new DateTime(2056, 1, 1);
            var duration = new TimeSpan(1, 0, 0);
            var size = 10;
            var subjectId = 2;
            var teacherId = 1;
            var price = 100;

            // Assert
            await Assert.ThrowsAsync<TeacherDoesNotTeachSubjectException>(async () => await sut.Handle(new CreateExtraClassCommand
            {
                Name = name,
                Date = date,
                Duration = duration,
                Size = size,
                SubjectId = subjectId,
                TeacherId = teacherId,
                Price = price
            }, CancellationToken.None));           
        }

        [Fact]
        public async Task CreateExtraClass_GivenSuccessfulValidation_ShouldThrowNotFoundException()
        {
            // Arrange          
            var sut = new CreateExtraClassCommand.CreateExtraClassCommandHandler(Context);
            var name = "Staff logic training";
            var date = new DateTime(2056, 1, 1);
            var duration = new TimeSpan(1, 0, 0);
            var size = 10;
            var subjectId = 2;
            var teacherId = 3;
            var price = 100;

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await sut.Handle(new CreateExtraClassCommand
            {
                Name = name,
                Date = date,
                Duration = duration,
                Size = size,
                SubjectId = subjectId,
                TeacherId = teacherId,
                Price = price
            }, CancellationToken.None));
        }
    }
}
