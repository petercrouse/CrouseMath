using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CrouseMath.Application.Bookings.Commands.DeleteBooking
{
    public class DeleteBookingCommand: IRequest<Unit>
    {
        public long Id { get; set; }
    }
    
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteBookingCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Bookings.Where(x => x.Id == request.Id)
                .Include(e => e.ExtraClass)
                .SingleOrDefaultAsync();

            if(entity == null)
            {
                throw new NotFoundException(nameof(Booking), request.Id);
            }

            if (entity.ExtraClass.IsClassFull)
            {
                entity.ExtraClass.IsClassFull = false;
            }            

            _context.Bookings.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
