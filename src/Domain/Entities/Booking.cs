using CrouseMath.Domain.Common;

namespace CrouseMath.Domain.Entities
{
    public class Booking : AuditableEntity
    {
        public long Id { get; set; }
        public long ExtraClassId { get; set; }
        public string UserId { get; set; }
        public double BookingPrice { get; set; }
        public bool Paid { get; set; }

        public ExtraClass ExtraClass { get; set; }
    }
}