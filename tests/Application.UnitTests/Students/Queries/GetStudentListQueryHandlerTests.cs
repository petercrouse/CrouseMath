using System.Linq;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Students.Queries.GetStudentList;
using CrouseMath.Infrastructure.Persistence;
using Shouldly;
using Xunit;

namespace CrouseMath.Application.UnitTests.Students.Queries
{
    [Collection("QueryCollection")]
    public class GetStudentListQueryHandlerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStudentListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetStudentList()
        {
            var sut = new GetStudentListQuery.GetStudentsQueryHandler(_context, _mapper);

            var result = await sut.Handle(new GetStudentListQuery(), CancellationToken.None);

            result.ShouldBeOfType<StudentListViewModel>();

            result.Students.Count().ShouldBe(4);
        }
    }
}
