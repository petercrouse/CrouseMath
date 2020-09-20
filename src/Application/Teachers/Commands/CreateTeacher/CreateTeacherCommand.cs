using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Domain.Entities;
using MediatR;

namespace CrouseMath.Application.Teachers.Commands.CreateTeacher
{
    public class CreateTeacherCommand : IRequest<Unit>
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
    }

    public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public CreateTeacherCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            var entity = new Teacher
            {
                LastName = request.LastName,
                FirstName = request.FirstName,
                Email = request.Email
            };

            await _context.Teachers.AddAsync(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
