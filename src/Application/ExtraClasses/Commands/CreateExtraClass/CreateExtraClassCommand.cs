using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Domain.Entities;
using CrouseMath.Application.Common.Exceptions;

namespace CrouseMath.Application.ExtraClasses.Commands.CreateExtraClass
{
    public class CreateExtraClassCommand : IRequest<long>
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Size { get; set; }
        public TimeSpan Duration { get; set; }
        public long SubjectId { get; set; }
        public double Price { get; set; }
    }
    
    public class CreateExtraClassCommandHandler : IRequestHandler<CreateExtraClassCommand, long>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public CreateExtraClassCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<long> Handle(CreateExtraClassCommand request, CancellationToken cancellationToken)
        {
            var subject = await _context.Subjects.FindAsync(request.SubjectId);

            if(subject == null)
            {
                throw new NotFoundException(nameof(Subject), request.SubjectId);
            }

            var entity = new ExtraClass
            {
                Name = request.Name,
                Date = request.Date,
                Duration = request.Duration,
                IsClassFull = false,
                Size = request.Size,
                SubjectId = request.SubjectId,
                TeacherId = _currentUserService.UserId,
                Price = request.Price,
            };

            _context.ExtraClasses.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
