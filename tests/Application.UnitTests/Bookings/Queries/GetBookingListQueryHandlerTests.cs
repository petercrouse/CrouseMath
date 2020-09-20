using System.Linq;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Bookings.Queries.GetBookingList;
using CrouseMath.Application.UnitTests;
using CrouseMath.Infrastructure.Persistence;
using Shouldly;
using Xunit;

namespace CrouseMath.Application.UnitTests.Bookings.Queries
{
    [Collection("QueryCollection")]
    public class GetBookingListQueryHandlerTests
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public GetBookingListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetBookingList()
        {
            var sut = new GetBookingListQuery.GetBookingsQueryHandler(_context, _mapper);

            var result = await sut.Handle(new GetBookingListQuery(), CancellationToken.None);

            result.ShouldBeOfType<BookingListViewModel>();

            result.Bookings.Count().ShouldBe(4);
        }
    }
}
