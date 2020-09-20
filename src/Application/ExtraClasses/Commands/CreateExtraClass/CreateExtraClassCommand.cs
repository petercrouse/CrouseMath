using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CrouseMath.Application.Common.Exceptions;
using CrouseMath.Application.Common.Extensions;
using CrouseMath.Application.Common.Interfaces;
using CrouseMath.Domain.Entities;

namespace CrouseMath.Application.ExtraClasses.Commands.CreateExtraClass
{
    public class CreateExtraClassCommand : IRequest<long>
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int? TeacherId { get; set; }
        public int Size { get; set; }
        public TimeSpan Duration { get; set; }
        public int SubjectId { get; set; }
        public double Price { get; set; }
    }
    
    public class CreateExtraClassCommandHandler : IRequestHandler<CreateExtraClassCommand, long>
    {
        private readonly IApplicationDbContext _context;

        public CreateExtraClassCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(CreateExtraClassCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _context.Teachers.FindAsync(request.TeacherId, cancellationToken);

            if (teacher == null)
            {
                throw new NotFoundException(nameof(Teacher), request.TeacherId);
            }          

            if (!teacher.TeachSubject(request.SubjectId))
            {
                throw new TeacherDoesNotTeachSubjectException(teacher.Id, request.SubjectId);
            }

            var entity = new ExtraClass
            {
                Name = request.Name,
                Date = request.Date,
                Duration = request.Duration,
                IsClassFull = false,
                Size = request.Size,
                SubjectId = request.SubjectId,
                TeacherId = request.TeacherId,
                Price = request.Price,
            };

            _context.ExtraClasses.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
