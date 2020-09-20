using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CrouseMath.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CrouseMath.Application.Subjects.Queries.GetSubjects
{
    public class GetSubjectListQuery : IRequest<SubjectListViewModel>
    {
    }

    public class GetSubjectsQueryHandler : IRequestHandler<GetSubjectListQuery, SubjectListViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSubjectsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SubjectListViewModel> Handle(GetSubjectListQuery request, CancellationToken cancellationToken)
        {
            return new SubjectListViewModel
            {
                Subjects = await _context.Subjects
                    .ProjectTo<SubjectLookup>(_mapper.ConfigurationProvider)
                    .OrderBy(s => s.Name)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
