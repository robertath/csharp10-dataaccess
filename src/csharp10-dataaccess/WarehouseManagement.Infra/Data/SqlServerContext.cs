using Microsoft.EntityFrameworkCore;

namespace WarehouseManagement.Infra.Data;

public class SqlServerContext
    : DbContext
{
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Warehouse> Warehouse { get; set; }
    public DbSet<Item> Item { get; set; }
    public DbSet<LineItem> LineItem { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<ShippingProvider> ShippingProvider { get; set; }

    protected override void 
        OnConfiguring(DbContextOptionsBuilder 
        optionsBuilder)
    {
        // MOVE TO A SECURE PLACE!!!!
        var connectionString = "Server=localhost;Database=WarehouseManagementDB;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=false;MultipleActiveResultSets=true;User ID=sa;Password=qaz?123";
        optionsBuilder.UseSqlServer(connectionString);
    }
}
