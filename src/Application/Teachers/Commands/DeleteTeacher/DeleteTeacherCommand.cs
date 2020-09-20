using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Domain.Entities;
using MediatR;

namespace CrouseMath.Application.Teachers.Commands.DeleteTeacher
{
    public class DeleteTeacherCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }

    public class DeleteTeacherCommandHandler : IRequestHandler<DeleteTeacherCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTeacherCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Teachers.FindAsync(request.Id);

            if(entity == null)
            {
                throw new NotFoundException(nameof(Teacher), request.Id);
            }

            var teachesClasses = _context.ExtraClasses.Any(c => c.TeacherId == entity.Id);

            if (teachesClasses)
            {
                throw new DeleteFailureException(nameof(Teacher), request.Id, "There are still classes taught by this teacher");
            }

            _context.Teachers.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
