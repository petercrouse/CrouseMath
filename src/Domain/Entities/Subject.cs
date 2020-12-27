using CrouseMath.Domain.Common;
using System.Collections.Generic;

namespace CrouseMath.Domain.Entities
{
    public class Subject : AuditableEntity
    {
        public Subject()
        {
            ExtraClasses = new HashSet<ExtraClass>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<ExtraClass> ExtraClasses { get; set; }
    }
}