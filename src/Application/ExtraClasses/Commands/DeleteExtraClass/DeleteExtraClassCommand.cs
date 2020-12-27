using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Domain.Entities;
using MediatR;

namespace CrouseMath.Application.ExtraClasses.Commands.DeleteExtraClass
{
    public class DeleteExtraClassCommand : IRequest<Unit>
    {
        public long Id { get; set; } 
    }

    public class DeleteExtraClassCommandHandler : IRequestHandler<DeleteExtraClassCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public DeleteExtraClassCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteExtraClassCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.ExtraClasses.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(ExtraClass), request.Id);
                }

                var hasBookings = _context.Bookings.Any(b => b.ExtraClassId == entity.Id);
                if (hasBookings)
                {
                    throw new DeleteFailureException(nameof(ExtraClass), request.Id, "There are exisiting bookings associated with this class");
                }

                _context.ExtraClasses.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
}
