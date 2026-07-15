using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Perdin.WebApi.Models;

namespace Perdin.WebApi.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("roles");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.HasData(
                new Role { Id = 1, Name = "ADMIN" },
                new Role { Id = 2, Name = "PEGAWAI" },
                new Role { Id = 3, Name = "SDM" }
            );
        }
    }
}
