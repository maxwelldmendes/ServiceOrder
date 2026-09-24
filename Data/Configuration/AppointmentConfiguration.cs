using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceOrderManager.Models;

namespace ServiceOrderManager.Data.Configuration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {

        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.AssignedUserId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(a => a.StartTime)
                .IsRequired();

            builder.Property(a => a.EndTime)
                .IsRequired();

            builder.Property(a => a.Description)
                .HasMaxLength(1000);

            builder.Property(a => a.Status)
                .IsRequired();

            builder.ToTable("Appointments", table =>
            {
                table.HasCheckConstraint(
                    "CK_Appointment_EndTime_After_StartTime",
                    "[EndTime] > [StartTime]");
            });

            builder.HasOne(a => a.AssignedUser)
                .WithMany()
                .HasForeignKey(a => a.AssignedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Client)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
