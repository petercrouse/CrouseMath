using MediatR;
using Moq;
using System.Threading;
using CrouseMath.Application.Students.Commands.CreateStudent;
using Xunit;

namespace CrouseMath.Application.UnitTests.Students.Commands.CreateStudent
{
    public class CreateStudentCommandTests : CommandTestBase
    {
        [Fact]
        public async void CreateStudentCommandHandler_ShouldRaiseStudentCreatedNotification()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sut = new CreateStudentCommand.CreateStudentCommandHandler(Context, mediatorMock.Object);
            var firstName = "Merry";
            var lastName = "Brandybuck";
            var email = "mbrandybuck@theshire.com";

            // Act
            var result = await sut.Handle(new CreateStudentCommand {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            }, CancellationToken.None);

            // Assert
            mediatorMock.Verify(m => m.Publish(It.Is<StudentCreated>(sc => sc.FirstName == firstName), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
