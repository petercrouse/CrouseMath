using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        private readonly IIdentityService _identityService;

        public GetBookingQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
        { 
            _context = context;
            _mapper = mapper;
            _identityService = identityService;
        }

        public async Task<BookingViewModel> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Bookings.Where(x => x.Id == request.Id)
                .Include(e => e.ExtraClass)
                .SingleOrDefaultAsync();

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
