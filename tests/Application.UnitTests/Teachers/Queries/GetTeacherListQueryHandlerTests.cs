using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CrouseMath.Application.UnitTests.Teachers.Queries
{
    [Collection("QueryCollection")]
    public class GetTeacherListQueryHandlerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTeacherListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetTeacherList()
        {
            var sut = new GetTeachersQueryHandler(_context);

            var result = await sut.Handle(new GetTeacherListQuery(), CancellationToken.None);

            result.ShouldBeOfType<TeacherListViewModel>();

            result.Teachers.Count().ShouldBe(2);
        }
    }
}
