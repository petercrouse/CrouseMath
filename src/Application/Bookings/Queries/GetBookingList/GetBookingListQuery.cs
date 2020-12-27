using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CrouseMath.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CrouseMath.Application.Bookings.Queries.GetBookingList
{
    public class GetBookingListQuery : IRequest<BookingListViewModel>
    {
    }
    
    public class GetBookingsQueryHandler : IRequestHandler<GetBookingListQuery, BookingListViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

        public GetBookingsQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
        {
            _context = context;
            _mapper = mapper;
            _identityService = identityService;
        }

        public async Task<BookingListViewModel> Handle(GetBookingListQuery request, CancellationToken cancellationToken)
        {
            var bookings = await _context.Bookings.ProjectTo<BookingLookupDto>(_mapper.ConfigurationProvider)
                                                  .ToListAsync(cancellationToken);

            foreach (var booking in bookings)
            {
                booking.UserName = await _identityService.GetUserNameAsync(booking.UserId);
            }

            return new BookingListViewModel
            {
                Bookings = bookings
            };
        }
    }
}
