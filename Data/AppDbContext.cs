using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


using ServiceOrderManager.Models;
using System.Reflection;

namespace ServiceOrderManager.Data
{
    public class AppDbContext : IdentityDbContext<SystemUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Technician> Technician { get; set; }
        public DbSet<PartItem> PartItem { get; set; }
        public DbSet<ServiceItem> ServiceItem { get; set; }
        public DbSet<ServiceOrder> ServiceOrder { get; set; }
        public DbSet<SystemUser> SystemUser { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<ServiceAppointment> ServiceAppointments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Automatically finds and applies all implementations of IEntityTypeConfiguration in this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}