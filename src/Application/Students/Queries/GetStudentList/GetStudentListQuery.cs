using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CrouseMath.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CrouseMath.Application.Students.Queries.GetStudentList
{
    public class GetStudentListQuery : IRequest<StudentListViewModel>
    {        
    }

    public class GetStudentsQueryHandler : IRequestHandler<GetStudentListQuery, StudentListViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStudentsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StudentListViewModel> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            return new StudentListViewModel
            {
                Students = await _context.Students
                    .ProjectTo<StudentLookup>(_mapper.ConfigurationProvider)
                    .OrderBy(n => n.Name)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}