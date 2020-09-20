using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Domain.Entities;
using MediatR;

namespace CrouseMath.Application.ExtraClasses.Queries.GetExtraClass
{
    public class GetExtraClassQuery: IRequest<ExtraClassViewModel>
    {
        public int Id { get; set; }
    }

    public class GetExtraClassQueryHandler : IRequestHandler<GetExtraClassQuery, ExtraClassViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetExtraClassQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExtraClassViewModel> Handle(GetExtraClassQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.ExtraClasses.FindAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ExtraClass), request.Id);
            }

            return new ExtraClassViewModel
            {
                ExtraClass = _mapper.Map<ExtraClassDto>(entity)
            };
        }
    }
}
