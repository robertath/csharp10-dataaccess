using customers.infra.Entities;
using Microsoft.EntityFrameworkCore;

namespace customers.infra.Repositories;

public class SqlServerContext : DbContext
{
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Address> Address { get; set; }
    public DbSet<Person> Person { get; set; }
    public DbSet<User> User { get; set; }

    public SqlServerContext()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var connectionString = "Server=localhost;Database=CustomerDB;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=false;MultipleActiveResultSets=true;User ID=sa;Password=qaz?123";
        optionsBuilder.UseSqlServer(connectionString);
    }
}
