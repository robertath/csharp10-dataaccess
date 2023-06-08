using customers.infra.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace customers.infra.Repositories;

public class RepositoryAddress : RepositoryBase<Address>
{
    public RepositoryAddress(SqlServerContext context) : base(context)
    {
    }

    public async override Task<IEnumerable<Address>> All()
    {
        var list = await context.Set<Address>().ToListAsync();
        return list.OrderBy(i=> i.Country).ThenBy(i=> i.County).ThenBy(i=> i.Street);
    }
}
