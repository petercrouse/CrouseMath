using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CrouseMath.Application.Subjects.Queries.GetSubjects;
using CrouseMath.Infrastructure.Persistence;
using Shouldly;
using Xunit;

namespace CrouseMath.Application.UnitTests.Subjects.Queries
{
    [Collection("QueryCollection")]
    public class GetSubjectListQueryHandlerTests
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSubjectListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetSubjectList()
        {
            var sut = new GetSubjectListQuery.GetSubjectsQueryHandler(_context, _mapper);

            var result = await sut.Handle(new GetSubjectListQuery(), CancellationToken.None);

            result.ShouldBeOfType<SubjectListViewModel>();

            result.Subjects.Count().ShouldBe(2);
        }

    }
}
