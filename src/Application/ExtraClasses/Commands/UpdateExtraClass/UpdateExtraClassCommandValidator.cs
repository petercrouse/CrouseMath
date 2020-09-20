using FluentValidation;
using System;
using CrouseMath.Application.Common.Interfaces;

namespace CrouseMath.Application.ExtraClasses.Commands.UpdateExtraClass
{
    public class UpdateExtraClassCommandValidator : AbstractValidator<UpdateExtraClassCommand>
    {
        public UpdateExtraClassCommandValidator(IDateTime dateTime)
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Date).GreaterThanOrEqualTo(dateTime.Now);
            RuleFor(x => x.Size).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Duration).GreaterThanOrEqualTo(new TimeSpan(0, 30, 0));
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.SubjectId).NotNull();
        }
    }
}
