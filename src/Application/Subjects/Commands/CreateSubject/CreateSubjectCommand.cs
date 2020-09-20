using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Domain.Entities;
using MediatR;

namespace CrouseMath.Application.Subjects.Commands.CreateSubject
{
    public class CreateSubjectCommand : IRequest<long>
    {
        public string Name { get; set; }
    }

    public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, long>
    {
        private readonly IApplicationDbContext _context;

        public CreateSubjectCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            var entity = new Subject
            {
                Name = request.Name
            };

            _context.Subjects.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
