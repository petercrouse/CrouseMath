using System;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.ExtraClasses.Commands.UpdateExtraClass;
using Xunit;

namespace CrouseMath.Application.UnitTests.ExtraClasses.Commands.UpdateExtraClass
{
    public class UpdateExtraClassHandlerTests : CommandTestBase
    {
        [Fact]
        public async Task UpdateExtraClass_GivenSuccessfulValidation_ShouldThrowTeacherDoesNotTeachSubjectException()
        {
            // Arrange          
            var sut = new UpdateExtraClassCommand.UpdateExtraClassCommandHandler(Context);
            var id = 2;
            var name = "Staff logic training";
            var date = new DateTime(2056, 1, 1);
            var duration = new TimeSpan(1, 0, 0);
            var size = 10;
            var subjectId = 2;
            var teacherId = 1;
            var price = 100;

            // Assert
            await Assert.ThrowsAsync<TeacherDoesNotTeachSubjectException>(async () => await sut.Handle(new UpdateExtraClassCommand
            {
                Id = id,
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
        public async Task UpdateExtraClass_GivenSuccessfulValidation_ShouldThrowNotFoundException()
        {
            // Arrange          
            var sut = new UpdateExtraClassCommand.UpdateExtraClassCommandHandler(Context);
            var id = 999;
            var name = "Staff logic training";
            var date = new DateTime(2056, 1, 1);
            var duration = new TimeSpan(1, 0, 0);
            var size = 10;
            var subjectId = 2;
            var teacherId = 3;
            var price = 100;

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await sut.Handle(new UpdateExtraClassCommand
            {
                Id = id,
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
        public async Task UpdateExtraClass_GivenSuccessfulValidation_TeacherNotFound_ShouldThrowNotFoundException()
        {
            // Arrange          
            var sut = new UpdateExtraClassCommand.UpdateExtraClassCommandHandler(Context);
            var id = 1;
            var name = "Staff logic training";
            var date = new DateTime(2056, 1, 1);
            var duration = new TimeSpan(1, 0, 0);
            var size = 10;
            var subjectId = 2;
            var teacherId = 999;
            var price = 100;

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await sut.Handle(new UpdateExtraClassCommand
            {
                Id = id,
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
        public async Task UpdateExtraClass_GivenSuccessfulValidation_ClassSizeTooSmall_ShouldThrowClassSizeIsTooSmallForCurrentBookingsException()
        {
            // Arrange          
            var sut = new UpdateExtraClassCommand.UpdateExtraClassCommandHandler(Context);
            var id = 1;
            var name = "Staff logic training";
            var date = new DateTime(2056, 1, 1);
            var duration = new TimeSpan(1, 0, 0);
            var size = 1;
            var subjectId = 2;
            var teacherId = 1;
            var price = 100;

            // Assert
            await Assert.ThrowsAsync<ClassSizeIsTooSmallForCurrentBookingsException>(async () => await sut.Handle(new UpdateExtraClassCommand
            {
                Id = id,
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
