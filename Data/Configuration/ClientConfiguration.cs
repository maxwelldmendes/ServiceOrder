using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceOrderManager.Models;

namespace ServiceOrderManager.Data.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.EINumber).HasMaxLength(10);
            builder.Property(c => c.PrimaryPhone).IsRequired().HasMaxLength(20);
            builder.Property(c => c.PrimaryEmail).IsRequired().HasMaxLength(100);

            builder.HasOne(c => c.CompanyAddress)
                   .WithOne() // Left empty because Address doesn't map backwards
                   .HasForeignKey<Client>(c => c.CompanyAddressId) // Enforces uniqueness on this foreign key
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            builder.HasOne(c => c.MailAddress)
                   .WithOne() // Left empty because Address doesn't map backwards
                   .HasForeignKey<Client>(c => c.MailAddressId) // Enforces uniqueness on this foreign key
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        }
    }
}
