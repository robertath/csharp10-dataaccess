using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Infra.Data;

namespace WarehouseManagement.Infra;

public interface IUnitOfWork
{
    IRepositoryBase<Customer> CustomerRepository { get; }
    IRepositoryBase<Order> OrderRepository { get; }
    IRepositoryBase<Item> ItemRepository { get; }
    IRepositoryBase<ShippingProvider> ShippingProviderRepository { get; }

    void SaveChanges();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly SqlServerContext context;

    public UnitOfWork(SqlServerContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));        
    }


    private IRepositoryBase<Customer> customerRepository;
    public IRepositoryBase<Customer> CustomerRepository
    {
        get
        {
            if (customerRepository is null)
                customerRepository = new RepositoryCustomer(context);

            return customerRepository;
        }
    }

    private IRepositoryBase<Order> orderRepository;
    public IRepositoryBase<Order> OrderRepository
    {
        get
        {
            if (orderRepository is null)
                orderRepository = new RepositoryOrder(context);

            return orderRepository;
        }
    }
        
    private IRepositoryBase<Item> itemRepository;
    public IRepositoryBase<Item> ItemRepository
    {
        get
        {
            if (itemRepository is null)
                itemRepository = new RepositoryItem(context);

            return itemRepository;
        }
    }

    private IRepositoryBase<ShippingProvider> shippingProviderRepository;
    public IRepositoryBase<ShippingProvider> ShippingProviderRepository
    {
        get
        {
            if(shippingProviderRepository is null)
                shippingProviderRepository = new RepositoryShippingProvider(context);
            
            return shippingProviderRepository;
        }
    }

    public void SaveChanges() => context.SaveChanges();
}
