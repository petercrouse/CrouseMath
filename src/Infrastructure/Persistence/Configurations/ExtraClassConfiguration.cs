using CrouseMath.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrouseMath.Infrastructure.Persistence.Configurations
{
    public class ExtraClassConfiguration : IEntityTypeConfiguration<ExtraClass>
    {
        public void Configure(EntityTypeBuilder<ExtraClass> builder)
        {
            builder.ToTable("ExtraClass");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.Date)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(e => e.Duration)
                .IsRequired()
                .HasColumnType("time(7)");

            builder.Property(e => e.Price)
                .IsRequired()
                .HasColumnType("money");
        }
    }
}