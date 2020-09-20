using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Domain.Entities;
using MediatR;

namespace CrouseMath.Application.Students.Commands.CreateStudent
{
    public class CreateStudentCommand : IRequest<Unit>
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
    }

    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Unit>
    {
        public CreateStudentCommandHandler(IApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;

        public async Task<Unit> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var entity = new Student
            {
                LastName = request.LastName,
                FirstName = request.FirstName,
                Email = request.Email
            };

            _context.Students.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new StudentCreated
            {
                LastName = entity.LastName,
                FirstName = entity.FirstName,
                Email = entity.Email
            }, cancellationToken);

            return Unit.Value;
        }
    }
}