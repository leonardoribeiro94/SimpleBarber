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

            builder.Property(x => x.Status)
                .IsRequired();

            // 1:N
            builder.HasOne(a => a.Client)
                .WithMany();

            //N:1
            builder.HasMany(s => s.Services)
                .WithOne();
        }
    }
}
