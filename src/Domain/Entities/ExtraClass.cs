using System;
using System.Collections.Generic;
using CrouseMath.Domain.Common;

namespace CrouseMath.Domain.Entities
{
    public class ExtraClass : AuditableEntity
    {
        public ExtraClass()
        {
            Bookings = new HashSet<Booking>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Size { get; set; }
        public bool IsClassFull { get; set; }
        public TimeSpan Duration { get; set; }
        public long SubjectId { get; set; }
        public double Price { get; set; }
        public string TeacherId { get; set; }

        public Subject Subject { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}