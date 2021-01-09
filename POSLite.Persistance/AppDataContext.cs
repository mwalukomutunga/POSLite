using Microsoft.EntityFrameworkCore;
using POSLite.Domain;
using POSLite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace POSLite.Persistance
{
  public  class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        { }
        //public AppDataContext() : base()
        //{

        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source=POS.db;Version=3;Cache Size=3000;");
        //}
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLineItem> InvoiceLineItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemInventory> ItemInventories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<SalesOutlet> SalesOutlets { get; set; }
        public DbSet<InventoryAdjustment> InventoryAdjustment { get; set; }
        public DbSet<ItemPriceLog> ItemPriceLog { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<UnitOfMeasurement> UnitOfMeasurements { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ItemCategory> ItemCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                  new Customer
                 {
                     CustomerId = Guid.NewGuid(),
                     FullName = "Walkin Customer",
                     Gender = Domain.Enums.Gender.Unknown,
                     AmountOfLastDeposit = 0,
                     CurrentBalance = 0,
                     DateOfBirth = IDateTime.Now().AddYears(-50),
                     CreatedAt = IDateTime.Now(),
                     DateOfLastDeposit = IDateTime.Now(),
                     OtherDetails = "Anonymous customer",
                     Terminus = Environment.MachineName,
                     UpdatedAt = IDateTime.Now(),
                 }
                );
            modelBuilder.Entity<Brand>().HasData(new Brand { Name="Unknown", BrandId=Guid.NewGuid() });
            modelBuilder.Entity<ItemCategory>().HasData(new ItemCategory { CategoryId= Guid.NewGuid(), Name="Other",Description= "Other" });
            modelBuilder.Entity<UnitOfMeasurement>().HasData(new UnitOfMeasurement { ID = Guid.NewGuid(), UOMCode="Each", UOMDescription = "Each" });
            base.OnModelCreating(modelBuilder);
        }
    }
}
