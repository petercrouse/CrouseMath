using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Domain.Entities;

namespace CrouseMath.Application.ExtraClasses.Commands.UpdateExtraClass
{
    public class UpdateExtraClassCommand : IRequest<Unit>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string TeacherId { get; set; }
        public int Size { get; set; }
        public TimeSpan Duration { get; set; }
        public long SubjectId { get; set; }
        public double Price { get; set; } 
    }

    public class UpdateExtraClassCommandHandler : IRequestHandler<UpdateExtraClassCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;

        public UpdateExtraClassCommandHandler(IApplicationDbContext context, IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        public async Task<Unit> Handle(UpdateExtraClassCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ExtraClasses.FindAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ExtraClass), request.Id);
            }

            if (entity.TeacherId != request.TeacherId)
            {
                var user = await _identityService.GetUserNameAsync(request.TeacherId);

                if (user == null)
                {
                    throw new NotFoundException("Teacher", request.TeacherId);
                }
            }

            if (entity.Bookings.Count > request.Size)
            {
                throw new ClassSizeIsTooSmallForCurrentBookingsException(nameof(ExtraClass), entity.Id, entity.Size,
                    request.Size);
            }

            entity.Name = request.Name;
            entity.Date = request.Date;
            entity.Duration = request.Duration;
            entity.IsClassFull = false;
            entity.Size = request.Size;
            entity.SubjectId = request.SubjectId;
            entity.TeacherId = request.TeacherId;
            entity.Price = request.Price;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}