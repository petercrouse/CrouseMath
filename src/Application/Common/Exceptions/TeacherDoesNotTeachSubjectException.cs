using System;

namespace CrouseMath.Application.Common.Exceptions
{
    public class TeacherDoesNotTeachSubjectException : Exception
    {
        public TeacherDoesNotTeachSubjectException(object teacherKey, object subjectKey)
            : base($"Teacher key: ({teacherKey}) does not teach subject key: ({subjectKey})")
        {
        }
    }
}