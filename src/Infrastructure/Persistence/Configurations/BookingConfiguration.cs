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

            builder.Property(e => e.BookingPrice)
                .HasColumnType("money");
        }
    }
}