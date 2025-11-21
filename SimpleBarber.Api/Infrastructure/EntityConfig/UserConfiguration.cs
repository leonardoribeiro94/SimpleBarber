using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleBarber.Api.Domain;

namespace SimpleBarber.Api.Infrastructure.EntityConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Login)
                .HasColumnType("varchar(150)")
                .IsRequired();
            
            builder.HasIndex(x  => x.Login)
                .IsUnique();

            builder.Property(x => x.PasswordHash)
                .HasColumnType("varchar(150)")
                .IsRequired();
            
            builder.Property(x => x.Role)
                .HasColumnType("varchar(150)")
                .IsRequired();
        }
    }
}
