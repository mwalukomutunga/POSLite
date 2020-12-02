using Microsoft.EntityFrameworkCore;
using POSLite.Domain;
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
        //    optionsBuilder.UseSqlite("Data Source=POS.db");
        //}

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
    }
}
