namespace CrouseMath.Domain.Entities
{
    public class TeacherSubject
    {
        public long TeacherId { get; set; }
        public long SubjectId { get; set; }

        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
    }
}