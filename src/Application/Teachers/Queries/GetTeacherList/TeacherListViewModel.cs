using System.Collections.Generic;

namespace CrouseMath.Application.Teachers.Queries.GetTeacherList
{
    public class TeacherListViewModel
    {
        public IEnumerable<TeacherLookup> Teachers { get; set; }
    }
}
