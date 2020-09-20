using AutoMapper;
using CrouseMath.Application.Common.Mappings;
using CrouseMath.Domain.Entities;

namespace CrouseMath.Application.Bookings.Queries.GetBooking
{
    public class BookingDto : IMapFrom<Booking>
    {
        public int Id { get; set; }
        public int ExtraClassId { get; set; }
        public string ExtraClassName { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public bool Paid { get; set; }
        public double Price { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Booking, BookingDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(d => d.ExtraClassName, opt => opt.MapFrom(b => b.ExtraClass.Name))
                .ForMember(d => d.StudentName,
                    opt => opt.MapFrom(b => b != null ? $"{b.Student.FirstName} {b.Student.LastName}" : string.Empty))
                .ForMember(d => d.Price, opt => opt.MapFrom(b => b.BookingPrice));
        }

        // public class FullNameResolver : IMemberValueResolver<Booking, string = "">
        // {
        //     
        // }
    }
}
