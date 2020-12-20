using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Domain.Entities;
using MediatR;

namespace CrouseMath.Application.Subjects.Commands.DeleteSubject
{
    public class DeleteSubjectCommand : IRequest<Unit>
    {
        public long Id { get; set; }    
    }

    public class DeleteSubjectCommandHandler : IRequestHandler<DeleteSubjectCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteSubjectCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Subjects.FindAsync(request.Id);

            if(entity == null)
            {
                throw new NotFoundException(nameof(Subject), request.Id);
            }

            var taughtInClasses = _context.ExtraClasses.Any(s => s.SubjectId == request.Id);

            if (taughtInClasses)
            {
                throw new DeleteFailureException(nameof(Subject), request.Id, "There are still classes hosting this subject");
            }

            _context.Subjects.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
