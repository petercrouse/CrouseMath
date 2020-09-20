using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Domain.Entities;
using MediatR;

namespace CrouseMath.Application.Students.Queries.GetStudent
{
    public class GetStudentQuery : IRequest<StudentViewModel>
    {
        public long Id { get; set; }
    }

    public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, StudentViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStudentQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StudentViewModel> Handle(GetStudentQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Students.FindAsync(request.Id);

            if(entity == null)
            {
                throw new NotFoundException(nameof(Student), request.Id);
            }

            return new StudentViewModel
            {
                Student = _mapper.Map<StudentDto>(entity)
            };               
        }
    }
}