using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceOrderManager.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ServiceOrderManager.Data.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.EINumber).HasMaxLength(9);
            builder.Property(c => c.PrimariPhone).IsRequired().HasMaxLength(20);
            builder.Property(c => c.PrimeryEmail).IsRequired().HasMaxLength(100);

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

            builder.Property(c => c.CompanyAddress).IsRequired();
            builder.Property(c => c.MailAddress).IsRequired();

        }
    }
}
