using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WarehouseManagement.EF.App.Entities;

namespace WarehouseManagement.EF.App.DAL;

public class SQLiteContext
        : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<LineItem> LineItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ShippingProvider> ShippingProviders { get; set; }

    public SQLiteContext() : base() { }

    public SQLiteContext(DbContextOptions<SQLiteContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}