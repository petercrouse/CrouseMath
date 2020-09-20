using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Domain.Entities;
using MediatR;

namespace CrouseMath.Application.Subjects.Queries.GetSubject
{
    public class GetSubjectQuery : IRequest<SubjectViewModel>
    {
        public long Id { get; set; }
    }

    public class GetSubjectQueryHandler : IRequestHandler<GetSubjectQuery, SubjectViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSubjectQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SubjectViewModel> Handle(GetSubjectQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Subjects.FindAsync(request.Id);

            if(entity == null)
            {
                throw new NotFoundException(nameof(Subject), request.Id);
            }

            return new SubjectViewModel
            {
                Subject = _mapper.Map<SubjectDto>(entity)
            };               
        }
    }
}
