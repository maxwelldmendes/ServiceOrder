using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceOrderManager.Models;

namespace ServiceOrderManager.Data.Configuration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Street1).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Street2).HasMaxLength(100);
            builder.Property(c => c.City).IsRequired().HasMaxLength(100);
            builder.Property(c => c.State).IsRequired().HasMaxLength(100);
            builder.Property(c => c.ZipCode).IsRequired().HasMaxLength(10);
            builder.Property(c => c.Country).IsRequired().HasMaxLength(3).HasDefaultValue("USA");
        }
    } 
}
