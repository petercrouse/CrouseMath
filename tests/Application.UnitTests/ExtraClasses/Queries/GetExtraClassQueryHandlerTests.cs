using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.ExtraClasses.Queries.GetExtraClass;
using CrouseMath.Infrastructure.Persistence;
using Shouldly;
using Xunit;

namespace CrouseMath.Application.UnitTests.ExtraClasses.Queries
{
    [Collection("QueryCollection")]
    public class GetExtraClassQueryHandlerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetExtraClassQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetExtraClass()
        {
            var sut = new GetExtraClassQuery.GetExtraClassQueryHandler(_context, _mapper);

            var result = await sut.Handle(new GetExtraClassQuery { Id = 1 }, CancellationToken.None);

            result.ShouldBeOfType<ExtraClassViewModel>();
            result.ExtraClass.Id.ShouldBe(1);
        }
    }
}
