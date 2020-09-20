using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Common.Extensions;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Domain.Entities;

namespace CrouseMath.Application.ExtraClasses.Commands.UpdateExtraClass
{
    public class UpdateExtraClassCommand : IRequest<Unit>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public long? TeacherId { get; set; }
        public int Size { get; set; }
        public TimeSpan Duration { get; set; }
        public long SubjectId { get; set; }
        public double Price { get; set; } 
    }

    public class UpdateExtraClassCommandHandler : IRequestHandler<UpdateExtraClassCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateExtraClassCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateExtraClassCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ExtraClasses.FindAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ExtraClass), request.Id);
            }

            Teacher teacher;

            if (entity?.TeacherId != request?.TeacherId)
            {
                teacher = await _context.Teachers.FindAsync(request.TeacherId, cancellationToken);

                if (teacher == null)
                {
                    throw new NotFoundException(nameof(Teacher), request.TeacherId);
                }

                if (!teacher.TeachSubject(request.SubjectId))
                {
                    throw new TeacherDoesNotTeachSubjectException(teacher.Id, request.SubjectId);
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