using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Students.Queries.GetStudent;
using CrouseMath.Infrastructure.Persistence;
using Shouldly;
using Xunit;

namespace CrouseMath.Application.UnitTests.Students.Queries
{
    [Collection("QueryCollection")]
    public class GetStudentQueryHandlerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStudentQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetStudent()
        {
            var sut = new GetStudentQuery.GetStudentQueryHandler(_context, _mapper);

            var result = await sut.Handle(new GetStudentQuery { Id = 1 }, CancellationToken.None);

            result.ShouldBeOfType<StudentViewModel>();
            result.Student.Id.ShouldBe(1);
        }

        [Fact]
        public async Task GetStudent_ThrowNotFoundException()
        {
            var sut = new GetStudentQuery.GetStudentQueryHandler(_context, _mapper);

            //Assert
            await Assert.ThrowsAnyAsync<NotFoundException>(async () => await sut.Handle(new GetStudentQuery { Id = 99 }, CancellationToken.None));
        }
    }
}
