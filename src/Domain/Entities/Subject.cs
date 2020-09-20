using CrouseMath.Domain.Common;

namespace CrouseMath.Domain.Entities
{
    public class Subject : AuditableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}