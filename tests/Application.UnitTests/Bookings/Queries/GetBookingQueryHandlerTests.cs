using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CrouseMath.Application.UnitTests.Bookings.Queries
{
    [Collection("QueryCollection")]
    public class GetBookingQueryHandlerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBookingQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetBooking()
        {
            var sut = new GetBookingQueryHandler(_context, _mapper);

            var result = await sut.Handle(new GetBookingQuery { Id = 1 }, CancellationToken.None);

            result.ShouldBeOfType<BookingViewModel>();
            result.Booking.Id.ShouldBe(1);
            result.Booking.ExtraClassName.ShouldBe("How to be a wizzard");
        }

        [Fact]
        public async Task GetBooking_ShouldThrowNotFoundException()
        {
            var sut = new GetBookingQueryHandler(_context, _mapper);

            //Assert
            await Assert.ThrowsAnyAsync<NotFoundException>(async () => await sut.Handle(new GetBookingQuery { Id = 99 }, CancellationToken.None));
        }
    }
}
