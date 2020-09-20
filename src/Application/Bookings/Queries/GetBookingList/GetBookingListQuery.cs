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

        public GetBookingsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BookingListViewModel> Handle(GetBookingListQuery request, CancellationToken cancellationToken)
        {
            return new BookingListViewModel
            {
                Bookings = await _context.Bookings.ProjectTo<BookingLookup>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}
