using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleBarber.Api.Domain;

namespace SimpleBarber.Api.Infrastructure.EntityConfig
{
    public class ServiceConfiguration  : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("service")
                .HasKey(s => s.Id);
            
            builder.Property(x => x.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(200)
                .HasColumnType("varchar(500)");

            builder.Property(x => x.EstimateTime)
                .IsRequired();
            
            builder.Property(x => x.Price)
                .HasPrecision(18,2)
                .IsRequired();
        }
    }
}
