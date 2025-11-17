using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleBarber.Api.Domain;

namespace SimpleBarber.Api.Infrastructure.EntityConfig
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DateAppointment)
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasColumnType("varchar(500)")
                .IsRequired(false);
            
            builder.Property(x => x.Status)
                .IsRequired();
            
            builder.Property(x => x.CustomerId)
                .IsRequired();
            
            // 1:N
            builder.HasOne(a => a.Customer)
                .WithMany(c => c.Appointments)
                .HasForeignKey(c => c.CustomerId);

            //N:1
            builder.HasMany(a => a.Services)
                .WithMany(s => s.Appointments)
                .UsingEntity(j => j.ToTable("AppointmentServices"));
        }
    }
}
