using System.Linq;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.ExtraClasses.Queries.GetExtraClassList;
using CrouseMath.Infrastructure.Persistence;
using Shouldly;
using Xunit;

namespace CrouseMath.Application.UnitTests.ExtraClasses.Queries
{
    [Collection("QueryCollection")]
    public class GetExtraClassListQueryHandlerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetExtraClassListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetExtraClassesList()
        {
            var sut = new GetExtraClassListQuery.GetExtraClassesQueryHandler(_context, _mapper);

            var result = await sut.Handle(new GetExtraClassListQuery(), CancellationToken.None);

            result.ShouldBeOfType<ExtraClassListViewModel>();

            result.ExtraClasses.Count().ShouldBe(3);
        }
    }
}
