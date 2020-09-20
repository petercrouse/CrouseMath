using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.ExtraClasses.Commands.DeleteExtraClass;
using Xunit;

namespace CrouseMath.Application.UnitTests.ExtraClasses.Commands.DeleteExtraClass
{
    public class DeleteExtraClassHandlerTests : CommandTestBase
    {
        [Fact]
        public async Task DeleteExtraClassCommandHandler_GivenSuccessfulValidation_ShouldThrowDeleteFailureException()
        {
            //Arrange
            var sut = new DeleteExtraClassCommand.DeleteExtraClassCommandHandler(Context);

            // Assert
            await Assert.ThrowsAsync<DeleteFailureException>(async () => await sut.Handle(new DeleteExtraClassCommand { Id = 1 }, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteExtraClass_GivenSuccessfulValidation_ShouldThrowNotFoundException()
        {
            // Arrange     
            var mediatorMock = new Mock<IMediator>();
            var sut = new DeleteExtraClassCommand.DeleteExtraClassCommandHandler(Context);            

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await sut.Handle(new DeleteExtraClassCommand { Id = 99 }, CancellationToken.None));
        }
    }
}
