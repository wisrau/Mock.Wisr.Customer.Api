using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Wisr.Customer.Api.Database.Models;

namespace Wisr.Customer.Api.Database
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureCustomer(modelBuilder);

            ConfigureFee(modelBuilder);
        }

        private static void ConfigureFee(ModelBuilder modelBuilder)
        {
            var existingFees = new Fee[]
            {
                new()
                {
                    Id = 1,
                    Threshold = 11000,
                    Amount = 30
                },
                new()
                {
                    Id = 2,
                    Threshold = 30000,
                    Amount = 5
                },
                new()
                {
                    Id = 3,
                    Threshold = 90000,
                    Amount = 20
                },
                new()
                {
                    Id = 4,
                    Threshold = 45000,
                    Amount = 10
                },
                new()
                {
                    Id = 5,
                    Threshold = 70000,
                    Amount = 15
                },
            };

            modelBuilder.Entity<Models.Fee>().HasKey(_ => _.Id);

            modelBuilder.Entity<Models.Fee>().Property(_ => _.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Models.Fee>().HasData(existingFees);
        }

        private static void ConfigureCustomer(ModelBuilder modelBuilder)
        {
            int sequence = 1;
            var existingCustomers = new Fixture()
                .Build<Models.Customer>()
                .With(_ => _.Income)
                .With(_ => _.Id, () => sequence++)
                .CreateMany<Models.Customer>(100);

            modelBuilder.Entity<Models.Customer>().Property(_ => _.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Models.Customer>().HasKey(_ => _.Id);
            modelBuilder.Entity<Models.Customer>().HasData(existingCustomers);
        }

        public DbSet<Models.Customer> Customers { get; set; }
        public DbSet<Models.Fee> Fees { get; set; }
    }
}
