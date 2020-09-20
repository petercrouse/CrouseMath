using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CrouseMath.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CrouseMath.Application.Teachers.Queries.GetTeacherList
{
    public class GetTeacherListQuery : IRequest<TeacherListViewModel>
    {       
    }

    public class GetTeachersQueryHandler : IRequestHandler<GetTeacherListQuery, TeacherListViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTeachersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeacherListViewModel> Handle(GetTeacherListQuery request, CancellationToken cancellationToken)
        {
            return new TeacherListViewModel
            {
                Teachers = await _context.Teachers
                    .ProjectTo<TeacherLookup>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Name)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
