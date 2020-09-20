using AutoMapper;
using CrouseMath.Application.Common.Mappings;
using CrouseMath.Domain.Entities;

namespace CrouseMath.Application.Bookings.Queries.GetBookingList
{
    public class BookingLookup : IMapFrom<Booking>
    {
        public long Id { get; set; }
        public string StudentName { get; set; }
        public string ExtraClassName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Booking, BookingLookup>()
                .ForMember(d => d.StudentName, opt => opt.MapFrom(b => $"{b.Student.FirstName} {b.Student.LastName}"))
                .ForMember(d => d.ExtraClassName, opt => opt.MapFrom(c => c.ExtraClass.Name));
        }
    }
}