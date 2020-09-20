using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CrouseMath.Application.UnitTests.Teachers.Queries
{
    [Collection("QueryCollection")]
    public class GetTeacherQueryHandlerTests
    {
        private readonly ApplicationDbContext _context;

        public GetTeacherQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetTeacher()
        {
            var sut = new GetTeacherQueryHandler(_context);

            var result = await sut.Handle(new GetTeacherQuery { Id = 1 }, CancellationToken.None);

            result.ShouldBeOfType<TeacherViewModel>();
            result.Teacher.Id.ShouldBe(1);
        }
    }
}
