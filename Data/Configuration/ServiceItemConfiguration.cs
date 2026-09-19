using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceOrderManager.Models;

namespace ServiceOrderManager.Data.Configuration
{
    public class ServiceItemConfiguration : IEntityTypeConfiguration<ServiceItem>
    {
        public void Configure(EntityTypeBuilder<ServiceItem> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Description).IsRequired().HasMaxLength(500);
            builder.Property(s => s.HourPrice).HasColumnType("decimal(18,2)");
            builder.Property(s => s.WorkingHours).HasColumnType("float");

            builder.Property(s => s.ServiceOrderId).IsRequired();

            builder.HasOne(c => c.ServiceOrder)
                   .WithMany(e => e.ServiceItem) // use .WithMany("ServiceItems") only if that navigation exists on ServiceOrder
                   .HasForeignKey(s => s.ServiceOrderId)
                   .IsRequired();
        }
    }
}


