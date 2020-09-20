using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Domain.Entities;
using MediatR;

namespace CrouseMath.Application.Bookings.Commands.UpdateBooking
{
    public class UpdateBookingCommand: IRequest<Unit>
    {
        public long Id { get; set; }
        public long ExtraClassId { get; set; }
        public bool Paid { get; set; }
        public double Price { get; set; }
    }
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateBookingCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Bookings.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Booking), request.Id);
            }

            ExtraClass extraClass;

            if(entity.ExtraClassId != request.ExtraClassId)
            {
                extraClass = await _context.ExtraClasses.FindAsync(request.ExtraClassId, cancellationToken);

                if (extraClass == null)
                {
                    throw new NotFoundException(nameof(ExtraClass), request.ExtraClassId);
                }

                if (extraClass.Bookings.Any(b => b.StudentId == entity.StudentId))
                {
                    throw new DoubleBookingException(nameof(ExtraClass), request.ExtraClassId, entity.StudentId);
                }
            }           

            entity.BookingPrice = request.Price;
            entity.Paid = request.Paid;
            entity.ExtraClassId = request.ExtraClassId;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
    
}
