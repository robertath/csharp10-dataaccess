using customers.infra.Entities;

namespace customers.infra.Repositories;

public interface IRepositoryProxy
{
    IRepositoryBase<Customer> RepositoryCustomer { get; }
    IRepositoryBase<Person> RepositoryPerson { get; }
    IRepositoryBase<Address> RepositoryAddress { get; }
    IRepositoryBase<User> RepositoryUser { get; }

    Task SaveChanges();
}

public class RepositoryProxy : IRepositoryProxy
{
    private readonly SqlServerContext context;

    public RepositoryProxy()
    {
        this.context = new SqlServerContext();
    }

    private IRepositoryBase<Customer> repositoryCustomer;
    public IRepositoryBase<Customer> RepositoryCustomer
    {
        get
        {
            if(repositoryCustomer is null)
                repositoryCustomer = new RepositoryCustomer(context);

            return repositoryCustomer;
        }
    }

    private IRepositoryBase<Person> repositoryPerson;
    public IRepositoryBase<Person> RepositoryPerson
    {
        get
        {
            if (repositoryPerson is null)
                repositoryPerson = new RepositoryPerson(context);

            return repositoryPerson;
        }
    }

    private IRepositoryBase<Address> repositoryAddress;
    public IRepositoryBase<Address> RepositoryAddress
    {
        get
        {
            if (repositoryAddress is null)
                repositoryAddress = new RepositoryAddress(context);

            return repositoryAddress;
        }
    }

    private IRepositoryBase<User> repositoryUser;
    public IRepositoryBase<User> RepositoryUser
    {
        get
        {
            if (repositoryUser is null)
                repositoryUser = new RepositoryUser(context);

            return repositoryUser;
        }
    }

    public async Task SaveChanges()
    {
        await context.SaveChangesAsync();
    }
}