using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleBarber.Api.Domain;

namespace SimpleBarber.Api.Infrastructure.EntityConfig
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(c => c.Phone)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(c => c.Email)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(c => c.BirthDate)
                .HasColumnType("date");  

            builder.Property(c => c.UserId)
                .IsRequired();

            // 1:1 map
            builder.HasOne(c => c.User)
                .WithOne(u => u.Client)
                .HasForeignKey<Customer>(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
