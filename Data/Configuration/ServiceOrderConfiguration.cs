using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ServiceOrderManager.Data.Configuration
{
    public class ServiceOrderConfiguration : IEntityTypeConfiguration<ServiceOrder>
    {
        public void Configure(EntityTypeBuilder<ServiceOrder> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Protocol).HasMaxLength(13);
            builder.Property(c => c.OpenDate).HasColumnType("datetime2").IsRequired();
            builder.Property(c => c.ClosedDate).HasColumnType("datetime2");
            builder.Property(c => c.Status).HasMaxLength(15);
            builder.Property(c => c.ProblemDescription).HasMaxLength(2048);
            builder.Property(c => c.TechnicalSolution).HasMaxLength(2048);
            builder.Property(c => c.TotalAmount).HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Client)
                    .WithMany(x => x.ServiceOrders)
                    .HasForeignKey(x => x.ClientId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

            builder.HasOne(x => x.Technician)
                 .WithMany(x => x.ServiceOrders)
                 .HasForeignKey(x => x.TechnicianId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
        }
    }
}
