using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Domain.Entities;
using MediatR;

namespace CrouseMath.Application.Bookings.Queries.GetBooking
{
    public class GetBookingQuery : IRequest<BookingViewModel>
    {
        public long Id { get; set; }
    }
    
    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, BookingViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBookingQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BookingViewModel> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Bookings.FindAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Booking), request.Id);
            }

            return new BookingViewModel
            {
                Booking = _mapper.Map<BookingDto>(entity)
            };
        }
    }
}
