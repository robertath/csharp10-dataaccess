using customers.infra.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace customers.infra.Repositories;

public class RepositoryPerson : RepositoryBase<Person>
{
    public RepositoryPerson(SqlServerContext context) : base(context)
    {
    }

    public async override Task<Person> Update(Person entity)
    {
        Person toUpdate = await Get(entity.Id);
        toUpdate.FirstName = entity.FirstName;
        toUpdate.LastName = entity.LastName;
        toUpdate.Gender = entity.Gender;
        toUpdate.MaritalStatus = entity.MaritalStatus;
        toUpdate.Address = entity.Address;
        toUpdate.Email = entity.Email;
        toUpdate.Phone = entity.Phone;
        toUpdate.Fax = entity.Fax;
        toUpdate.HomePage = entity.HomePage;        
        toUpdate.Salary = entity.Salary;

        return await base.Update(toUpdate);
    }

    public async override Task<IEnumerable<Person>> All()
    {
        var list = await context.Set<Person>().ToListAsync();

        return list
            .OrderBy(i => i.FirstName)
            .ThenBy(i => i.LastName);
    }
}
