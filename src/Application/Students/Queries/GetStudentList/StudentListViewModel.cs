using System.Collections.Generic;

namespace CrouseMath.Application.Students.Queries.GetStudentList
{
    public class StudentListViewModel
    {
        public IEnumerable<StudentLookup> Students { get; set; }
    }
}