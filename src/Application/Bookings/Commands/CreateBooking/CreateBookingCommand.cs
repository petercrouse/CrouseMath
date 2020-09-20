using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Domain.Entities;
using MediatR;

namespace CrouseMath.Application.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommand: IRequest<long>
    {
        public long ExtraClassId { get; set; }
        public long StudentId { get; set; }
    }
    
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, long>
    {
        private readonly IApplicationDbContext _context;

        public CreateBookingCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ExtraClasses.FindAsync(request.ExtraClassId, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ExtraClass), request.ExtraClassId);
            }

            if (entity.Bookings.Any(b => b.StudentId == request.StudentId))
            {
                throw new DoubleBookingException(nameof(ExtraClass), request.ExtraClassId, request.StudentId);
            }

            if (entity.IsClassFull)
            {
                throw new ClassIsFullException(nameof(ExtraClass), request.ExtraClassId);
            }

            var booking = new Booking
            {
                ExtraClassId = entity.Id,
                StudentId = request.StudentId,
                BookingPrice = entity.Price,
                Paid = false
            };

            _context.Bookings.Add(booking);

            if(entity.Bookings.Count == entity.Size)
            {
                entity.IsClassFull = true;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return booking.Id;
        }
    }
}
