using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Infra;
using WarehouseManagement.Infra.Data;

namespace WarehouseManagement.Web.Controllers;

public class CustomerController : Controller
{
    private readonly IRepositoryBase<Customer> _customerRepository;

    public CustomerController(IRepositoryBase<Customer> customerRepository)
    {
        _customerRepository = customerRepository 
            ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public IActionResult Index(Guid? id)
    {
        if (id == null)
        {
            var customers = _customerRepository.All();
            
            return View(customers);
        }
        else
        {
            var customer = _customerRepository.Get(id.Value);

            return View(new[] { customer });
        }
    }
}
