using System.Collections.Generic;
using CrouseMath.Domain.Common;

namespace CrouseMath.Domain.Entities
{
    public class Student : AuditableEntity
    {
        public Student()
        {
            Bookings = new HashSet<Booking>();
        }

        public long Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}