using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using WarehouseManagement.EF.App.Config;
using WarehouseManagement.EF.App.DAL;
using WarehouseManagement.EF.App.Entities;

namespace WarehouseManagement.EF.App;

internal sealed class Program
{
    static async Task Main(string[] args)
    {
        Init(args);

        SqlServerContext context = new SqlServerContext();

        await AddFakeBase(context);
        await AddFakeCustomerAndOrder(context);

        //var customerToDelete = await context.Customer.FirstOrDefaultAsync(i => i.Name.Contains("Anne"));
        //if(customerToDelete != null)
        //{
        //    context.Customer.Remove(customerToDelete);
        //    await context.SaveChangesAsync();
        //}            

        var customer = await context.Customer.FirstOrDefaultAsync(i => i.Name.Contains("Anne"));
        
        var Order = context.Order
           .Where(order => order.CustomerId == customer.Id)
           .Include(order => order.Customer)
           .Include(order => order.ShippingProvider)
           .Include(order => order.LineItems)
           .ThenInclude(lineItem => lineItem.Item);

        foreach (var order in Order)
        {
            Console.WriteLine($"------------------------------------------------------'");
            Console.WriteLine($"Order Id: {order.Id}");
            Console.WriteLine($"Customer: {order.Customer.Name}");
            Console.WriteLine($"Shipping Provider: {order.ShippingProvider.Name}");
            foreach (var lineItem in order.LineItems)
            {
                Console.WriteLine($"------------------------------------------------------'");
                Console.WriteLine($"\t{lineItem.Item.Name}");
                Console.WriteLine($"\t{lineItem.Item.Price}");
            }
            Console.WriteLine($"------------------------------------------------------'");
        }

        Console.ReadLine();
    }

    private static async Task AddCustomer(SqlServerContext context)
    {
        //Console.Write("Enter customer name: ");

        var newCustomer = new Customer
        {
            Name = "Thamara Hoffmann",
            Address = "Happy St.",
            PostalCode = "FD 123 45",
            Country = "Ireland",
            PhoneNumber = "+89 111 111 11"
        };
        context.Customer.Add(newCustomer);
        await context.SaveChangesAsync();
    }

    private static async Task AddFakeBase(SqlServerContext context)
    {
        ShippingProvider shippingProvider = new()
        {
            Id = Guid.NewGuid(),
            Name = "Cork Postal Service",
            FreightCost = 100m
        };
        context.ShippingProvider.Add(shippingProvider);
        
        Item item1 = new()
        {
            Id = Guid.NewGuid(),
            Name = "IPhone 14",
            Description = "Smart Phone",
            InStock = 5,
            Price = 1400,
            Warehouses = new Warehouse[]
            {
                new () { Id = Guid.NewGuid(), Location = "Ireland" }
            }
        };
        context.Item.Add(item1);

        Item item2 = new()
        {
            Id = Guid.NewGuid(),
            Name = "Air pod 2",
            Description = "Phone & Microphone",
            InStock = 5,
            Price = 299,
            Warehouses = new Warehouse[]
            {
                new () { Id = Guid.NewGuid(), Location = "Ireland" }
            }
        };
        context.Item.Add(item2);

        await context.SaveChangesAsync();
    }


    private static async Task AddFakeCustomerAndOrder(SqlServerContext context)
    {
        //Console.Write("Enter customer name: ");

        var newCustomer = new Customer
        {
            Name = "Anne Frank",
            Address = "Happy St.",
            PostalCode = "FD 123 45",
            Country = "Ireland",
            PhoneNumber = "+89 111 111 11"
        };
        context.Customer.Add(newCustomer);

        Order newOrder1 = new()
        {
            Id = Guid.NewGuid(),
            LineItems = new LineItem[]
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Item = await context.Item.FirstOrDefaultAsync(i=> i.Name.Equals("IPhone 14")),
                    Quantity = 1
                }
            },
            CreatedAt = DateTimeOffset.UtcNow,
            ShippingProvider = context.ShippingProvider.First(),
            Customer = newCustomer
        };        
        Order newOrder2 = new()
        {
            Id = Guid.NewGuid(),
            LineItems = new LineItem[]
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Item = await context.Item?.FirstOrDefaultAsync(i=> i.Name.Equals("Air pod 2")),
                    Quantity = 1
                }
            },
            CreatedAt = DateTimeOffset.UtcNow,
            ShippingProvider = context.ShippingProvider.First(),
            Customer = newCustomer
        };

        context.Order.Add(newOrder1);
        context.Order.Add(newOrder2);

        await context.SaveChangesAsync();
        Console.WriteLine("Order added!");
    }


    /// <summary>
    /// Inject Dependencies & Initialize Services
    /// </summary>
    static void Init(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateScopes = true;
                })
                .ConfigureAppConfiguration(configBuilder =>
                {
                    configBuilder.AddJsonFile("appsettings.json", false, true)
                    .Build();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddProductImporter();
                })
                .RunConsoleAsync();
    }
}

