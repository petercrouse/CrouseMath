using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CrouseMath.Application.Subjects.Queries.GetSubject;
using CrouseMath.Infrastructure.Persistence;
using Shouldly;
using Xunit;

namespace CrouseMath.Application.UnitTests.Subjects.Queries
{
    [Collection("QueryCollection")]
    public class GetSubjectQueryHandlerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSubjectQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetSubject()
        {
            var sut = new GetSubjectQuery.GetSubjectQueryHandler(_context, _mapper);

            var result = await sut.Handle(new GetSubjectQuery { Id = 1 }, CancellationToken.None);

            result.ShouldBeOfType<SubjectViewModel>();
            result.Subject.Id.ShouldBe(1);
        }
    }
}
