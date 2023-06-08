using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagement.Domain;

public class ShippingProvider
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal FreightCost { get; set; }
}
