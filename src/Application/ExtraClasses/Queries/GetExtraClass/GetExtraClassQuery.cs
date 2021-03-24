using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CrouseMath.Application.ExtraClasses.Queries.GetExtraClass
{
    public class GetExtraClassQuery: IRequest<ExtraClassViewModel>
    {
        public long Id { get; set; }
    }

    public class GetExtraClassQueryHandler : IRequestHandler<GetExtraClassQuery, ExtraClassViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

        public GetExtraClassQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
        {
            _context = context;
            _mapper = mapper;
            _identityService = identityService;
        }

        public async Task<ExtraClassViewModel> Handle(GetExtraClassQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.ExtraClasses.Where(x => x.Id == request.Id)
                .Include(x => x.Subject)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ExtraClass), request.Id);
            }

            var extraClass = _mapper.Map<ExtraClassDto>(entity);

            if(extraClass.TeacherId != null)
            {
                extraClass.TeacherName = await _identityService.GetUserNameAsync(extraClass.TeacherId);
            }

            return new ExtraClassViewModel
            {
                ExtraClass = extraClass
            };
        }
    }
}
