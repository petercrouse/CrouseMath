using CrouseMath.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrouseMath.Infrastructure.Persistence.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Booking");

            builder.Property(e => e.ExtraClassId)
                .IsRequired();

            builder.Property(e => e.StudentId)
                .IsRequired();

            builder.Property(e => e.BookingPrice)
                .HasColumnType("money");

            builder.HasOne(e => e.ExtraClass)
                .WithMany(e => e.Bookings)
                .HasForeignKey(e => e.Id);

            builder.HasOne(e => e.Student)
                .WithMany(e => e.Bookings)
                .HasForeignKey(e => e.Id);
        }
    }
}