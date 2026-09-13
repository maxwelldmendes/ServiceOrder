using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceOrderManager.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ServiceOrderManager.Data.Configuration
{
    public class TechnicianConfiguration : IEntityTypeConfiguration<Technician>
    {
        public void Configure(EntityTypeBuilder<Technician> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Skils).IsRequired().HasMaxLength(2048);
            builder.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(10);
            builder.Property(c => c.Enabled).HasColumnType("bit").IsRequired().HasDefaultValue(true);
        }
    }
}
