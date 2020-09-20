using FluentValidation;

namespace CrouseMath.Application.Teachers.Commands.CreateTeacher
{
    public class CreateTeacherCommandValidator: AbstractValidator<CreateTeacherCommand>
    {
        public CreateTeacherCommandValidator()
        {
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
