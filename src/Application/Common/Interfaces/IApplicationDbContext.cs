using CrouseMath.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CrouseMath.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Booking> Bookings { get; set; }
        DbSet<ExtraClass> ExtraClasses { get; set; }
        DbSet<Subject> Subjects { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
