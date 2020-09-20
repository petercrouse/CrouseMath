using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Domain.Entities;
using MediatR;

namespace CrouseMath.Application.Teachers.Queries.GetTeacher
{
    public class GetTeacherQuery : IRequest<TeacherViewModel>
    {
        public long Id { get; set; } 
    }

    public class GetTeacherQueryHandler : IRequestHandler<GetTeacherQuery, TeacherViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTeacherQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeacherViewModel> Handle(GetTeacherQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Teachers.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Teacher), request.Id);
            }

            return new TeacherViewModel
            {
                Teacher = _mapper.Map<TeacherDto>(entity)
            };
        }
    }
}
