using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using WarehouseManagement.EF.App.Entities;

namespace WarehouseManagement.EF.App.DAL;

public class SqlServerContext
        : DbContext
{
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Warehouse> Warehouse { get; set; }
    public DbSet<Item> Item { get; set; }
    public DbSet<LineItem> LineItem { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<ShippingProvider> ShippingProvider { get; set; }

    public SqlServerContext()
    {
        this.Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //modelBuilder.Entity<Customer>(entity => entity.Property(e => e.Id).ValueGeneratedOnAdd());

        //modelBuilder.Entity<Item>(entity =>
        //{
        //    //entity.Property(e => e.Id).ValueGeneratedNever();
        //    //entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        //    entity.HasMany(d => d.Warehouses)
        //        .WithMany(p => p.Items)
        //        .UsingEntity<Dictionary<string, object>>(
        //            "ItemWarehouse",
        //            l => l.HasOne<Warehouse>().WithMany().HasForeignKey("WarehouseId"),
        //            r => r.HasOne<Item>().WithMany().HasForeignKey("ItemId"),
        //            j =>
        //            {
        //                j.HasKey("ItemId", "WarehouseId");
        //                j.ToTable("ItemWarehouse");
        //                j.HasIndex(new[] { "WarehousesId" }, "IX_Item");
        //            });
        //});


    }

    protected override void
            OnConfiguring(DbContextOptionsBuilder
            optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            var appSettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = appSettings.GetValue<string>("ConnectionStrings:SQLServerDB");

            optionsBuilder.UseSqlServer(connectionString);
        }

        //To show all database logs
        optionsBuilder.UseLoggerFactory(
            new LoggerFactory(new[]
            {
                new DebugLoggerProvider()
            }));
    }


}