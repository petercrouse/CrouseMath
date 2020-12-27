using AutoMapper;
using CrouseMath.Application.Common.Mappings;
using CrouseMath.Domain.Entities;

namespace CrouseMath.Application.Bookings.Queries.GetBookingList
{
    public class BookingLookupDto : IMapFrom<Booking>
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ExtraClassName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Booking, BookingLookupDto>()
                .ForMember(d => d.ExtraClassName, opt => opt.MapFrom(s => s.ExtraClass.Name))
                .ForMember(d => d.UserName, opt => opt.Ignore());
        }
    }
}