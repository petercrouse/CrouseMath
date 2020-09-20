using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CrouseMath.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CrouseMath.Application.ExtraClasses.Queries.GetExtraClassList
{
    public class GetExtraClassListQuery : IRequest<ExtraClassListViewModel>
    {
        public class GetExtraClassesQueryHandler : IRequestHandler<GetExtraClassListQuery, ExtraClassListViewModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetExtraClassesQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ExtraClassListViewModel> Handle(GetExtraClassListQuery request, CancellationToken cancellationToken)
            {
                return new ExtraClassListViewModel
                {
                    ExtraClasses = await _context.ExtraClasses.ProjectTo<ExtraClassLookup>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken)
                };
            }
        }
    }
}
