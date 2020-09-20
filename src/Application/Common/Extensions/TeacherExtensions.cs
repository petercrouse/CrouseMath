using System.Linq;
using CrouseMath.Domain.Entities;

namespace CrouseMath.Application.Common.Extensions
{
    public static class TeacherExtensions
    {
        public static bool TeachSubject(this Teacher instance, long id)
        {
            return instance.TeachingSubjects.Any(s => s.SubjectId == id);
        }
    }
}