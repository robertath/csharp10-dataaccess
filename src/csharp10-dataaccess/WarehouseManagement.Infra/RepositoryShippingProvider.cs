using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagement.Infra.Data;

namespace WarehouseManagement.Infra;

public class RepositoryShippingProvider
    : RepositoryBase<ShippingProvider>
{
    public RepositoryShippingProvider(SqlServerContext context) : base(context)
    {
    }
}
