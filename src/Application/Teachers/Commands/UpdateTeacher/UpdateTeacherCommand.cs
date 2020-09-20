using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Domain.Entities;
using MediatR;

namespace CrouseMath.Application.Teachers.Commands.UpdateTeacher
{
    public class UpdateTeacherCommand : IRequest<Unit>
    {
        public long Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
    }

    public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTeacherCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Teachers.FindAsync(request.Id);

            if(entity == null)
            {
                throw new NotFoundException(nameof(Teacher), request.Id);
            }

            entity.LastName = request.LastName;
            entity.FirstName = request.FirstName;
            entity.Email = request.Email;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
