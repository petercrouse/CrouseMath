using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CrouseMath.Application.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommand: IRequest<long>
    {
        public long ExtraClassId { get; set; }
    }
    
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, long>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public CreateBookingCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<long> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ExtraClasses.Where(x => x.Id == request.ExtraClassId)
                .Include(b => b.Bookings)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ExtraClass), request.ExtraClassId);
            }

            if (entity.Bookings.Any(b => b.UserId == _currentUserService.UserId))
            {
                throw new DoubleBookingException(nameof(ExtraClass), request.ExtraClassId, _currentUserService.UserId);
            }

            if (entity.IsClassFull)
            {
                throw new ClassIsFullException(nameof(ExtraClass), request.ExtraClassId);
            }

            var booking = new Booking
            {
                ExtraClassId = entity.Id,
                UserId = _currentUserService.UserId,
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
