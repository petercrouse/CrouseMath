using AutoMapper;
using CrouseMath.Application.Common.Mappings;
using CrouseMath.Domain.Entities;

namespace CrouseMath.Application.Bookings.Queries.GetBooking
{
    public class BookingDto : IMapFrom<Booking>
    {
        public long Id { get; set; }
        public long ExtraClassId { get; set; }
        public string ExtraClassName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool Paid { get; set; }
        public double BookingPrice { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Booking, BookingDto>()
                .ForMember(d => d.ExtraClassName, opt => opt.MapFrom(b => b.ExtraClass.Name))
                .ForMember(d=> d.UserName, opt => opt.Ignore());
        }
    }
}
