using AutoMapper;
using CrouseMath.Application.Common.Interfaces;
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
        public string UserFullName { get; set; }
        public bool Paid { get; set; }
        public double Price { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Booking, BookingDto>()
                .ForMember(d => d.ExtraClassName, opt => opt.MapFrom(b => b.ExtraClass.Name))
                .ForMember(u => u.UserFullName, opt => opt.MapFrom<BookingDtoUserNameResolver>());
        }
    }

    public class BookingDtoUserNameResolver : IValueResolver<Booking, BookingDto, string>
    {
        private readonly IIdentityService _identityService;

        public BookingDtoUserNameResolver(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public string Resolve(Booking source, BookingDto destination, string destMember, ResolutionContext context)
        {
            return _identityService.GetUserName(source.UserId);
        }
    }
}
