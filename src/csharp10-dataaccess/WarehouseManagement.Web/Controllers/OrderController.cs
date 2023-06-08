using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Infra;
using WarehouseManagement.Infra.Data;
using WarehouseManagement.Web.Models;

namespace WarehouseManagement.Web.Controllers;

public class OrderController : Controller
{
    private readonly IUnitOfWork unitOfWork;
    

    public OrderController(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public IActionResult Index()
    {
        var orders = unitOfWork.OrderRepository.Find(order => 
            order.CreatedAt > DateTimeOffset.UtcNow.AddDays(-1));

        return View(orders);
    }

    public IActionResult Create()
    {
        var items = unitOfWork.ItemRepository.All();

        return View(items);
    }

    [HttpPost]
    public IActionResult Create(CreateOrderModel model)
    {
        #region Validate input
        if (!model.LineItems.Any()) return BadRequest("Please submit line items");

        if (string.IsNullOrWhiteSpace(model.Customer.Name)) return BadRequest("Customer needs a name");
        #endregion

        var customer = unitOfWork.CustomerRepository
            .Find(i=> i.Name == model.Customer.Name)
            .FirstOrDefault();
        
        if(customer is null)
        {
            customer = new Customer
            {
                Name = model.Customer.Name,
                PhoneNumber = model.Customer.PhoneNumber,
                Address = model.Customer.Address,
                PostalCode = model.Customer.PostalCode,
                Country = model.Customer.Country
            };
        }
        else
        {
            customer.Address = model.Customer.Address;
            customer.PostalCode = model.Customer.PostalCode;
            customer.Country = model.Customer.Country;
            customer.PhoneNumber = model.Customer.PhoneNumber;

            unitOfWork.CustomerRepository.Update(customer);
        }
               

        var order = new Order
        {
            Customer = customer,
            LineItems = model.LineItems
                        .Select(line => new LineItem
                        {
                            Id = Guid.NewGuid(),
                            ItemId = line.ItemId,
                            Quantity = line.Quantity
                        })
                        .ToList(),

            ShippingProviderId = unitOfWork.ShippingProviderRepository.All().First().Id,
            CreatedAt = DateTimeOffset.UtcNow
        };

        unitOfWork.OrderRepository.Add(order);

        unitOfWork.SaveChanges();

        return Ok("Order Created");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
