using System.Collections.Generic;
using CrouseMath.Domain.Common;

namespace CrouseMath.Domain.Entities
{
    public class Teacher : AuditableEntity
    {
        public Teacher()
        {
            TeachingSubjects = new HashSet<TeacherSubject>();
            TeachingClasses = new HashSet<ExtraClass>();
        }

        public long Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }

        public ICollection<TeacherSubject> TeachingSubjects { get; set; }
        public ICollection<ExtraClass> TeachingClasses { get; set; }
    }
}