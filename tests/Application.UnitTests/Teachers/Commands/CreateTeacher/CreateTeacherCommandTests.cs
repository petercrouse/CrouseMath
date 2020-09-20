using System.Threading;
using Xunit;

namespace CrouseMath.Application.UnitTests.Teachers.Commands.CreateTeacher
{
    public class CreateTeacherCommandTests : CommandTestBase
    {
        [Fact]
        public async void CreateTeacherCommandHandler_ShouldAddTeacherToContext()
        {
            // Arrange
            var sut = new CreateTeacherCommandHandler(Context);
            var firstName = "Saruman";
            var lastName = "The White";
            var email = "sthewhite@wizzardcouncil.com";

            // Act
            var result = await sut.Handle(new CreateTeacherCommand
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            }, CancellationToken.None);

            // Assert
            Context.Teachers.Where(t => t.FirstName == firstName).FirstOrDefault().FirstName.ShouldBe("Saruman");
        }
    }
}
