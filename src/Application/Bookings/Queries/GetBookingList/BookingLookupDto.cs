using AutoMapper;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Application.Common.Mappings;
using CrouseMath.Domain.Entities;

namespace CrouseMath.Application.Bookings.Queries.GetBookingList
{
    public class BookingLookupDto : IMapFrom<Booking>
    {
        public long Id { get; set; }
        public string UserFullName { get; set; }
        public string ExtraClassName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Booking, BookingLookupDto>()
                .ForMember(d => d.ExtraClassName, opt => opt.MapFrom(c => c.ExtraClass.Name))
                .ForMember(u => u.UserFullName, opt => opt.MapFrom<BookingLookupDtoUserNameResolver>());
        }
    }

    public class BookingLookupDtoUserNameResolver : IValueResolver<Booking, BookingLookupDto, string>
    {
        private readonly IIdentityService _identityService;

        public BookingLookupDtoUserNameResolver(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public string Resolve(Booking source, BookingLookupDto destination, string destMember, ResolutionContext context)
        {
            return _identityService.GetUserName(source.UserId);
        }
    }
}