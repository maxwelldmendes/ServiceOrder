using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceOrderManager.Models;

namespace ServiceOrderManager.Data.Configurations
{
    public class ServiceAppointmentConfiguration : IEntityTypeConfiguration<ServiceAppointment>
    {
        public void Configure(EntityTypeBuilder<ServiceAppointment> builder)
        {
            builder.ToTable("ServiceAppointments");
            builder.HasKey(appointment => appointment.Id);
            builder.Property(appointment => appointment.Title)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(appointment => appointment.Description)
                .IsRequired()
                .HasMaxLength(1000);
            builder.Property(appointment => appointment.StartTime)
                .HasColumnType("datetime2")
                .IsRequired();
            builder.Property(appointment => appointment.EndTime)
                .HasColumnType("datetime2")
                .IsRequired();
            builder.Property(appointment => appointment.ClientId)
                .HasColumnType("int")
                .IsRequired();
            builder.Property(appointment => appointment.Status)
                .IsRequired()
                .HasConversion<int>();
            builder.Property(appointment => appointment.AssignedTechnicianId)
                .HasColumnType("int")
                .IsRequired();
            /*--------------------------------------------------------------------
             * Configura relacionamentos com Clients e Technician
             -------------------------------------------------------------------*/
            builder.HasOne(appointment => appointment.ClientAssigned)
                .WithMany()
                .HasForeignKey(appointment => appointment.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(appointment => appointment.AssignedTechnician)
                .WithMany()
                .HasForeignKey(appointment =>
                    appointment.AssignedTechnicianId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
