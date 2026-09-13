using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceOrderManager.Models;

namespace ServiceOrderManager.Data.Configuration
{
    public class PartItemConfiguration : IEntityTypeConfiguration<PartItem>
    {
        public void Configure(EntityTypeBuilder<PartItem> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.PartName).IsRequired();
            builder.Property(e => e.Amount).HasColumnType("Integer");
            builder.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");

            builder.Property(e => e.ServiceOrderId);
            

            builder.HasOne(c => c.ServiceOrder)
                   .WithMany(s => s.PartItems)
                   .HasForeignKey(c => c.ServiceOrderId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        }
    }
}
