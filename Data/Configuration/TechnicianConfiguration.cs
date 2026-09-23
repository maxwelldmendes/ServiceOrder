using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceOrderManager.Models;

namespace ServiceOrderManager.Data.Configuration
{
    public class TechnicianConfiguration
        : IEntityTypeConfiguration<Technician>
    {
        public void Configure(EntityTypeBuilder<Technician> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.UserId)
                .IsRequired();

            builder.Property(t => t.Skills)
                .IsRequired()
                .HasMaxLength(2048);

            builder.Property(t => t.Enabled)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasOne(t => t.User)
                .WithOne(su => su.Technician)
                .HasForeignKey<Technician>(t => t.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(t => t.UserId)
                .IsUnique();
        }
    }
}
