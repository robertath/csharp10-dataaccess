
using customers.infra.Repositories;
using System.Text;

var context = new RepositoryProxy();

var listPerson = await context.RepositoryPerson.All();

StringBuilder sb = new StringBuilder();

sb.AppendLine("--------------------------------------------");
sb.AppendLine("PEOPLES ON DATABASE");
sb.AppendLine("--------------------------------------------");
foreach (var person in listPerson)
{
    sb.AppendLine($"Firs Name: {person.FirstName}");
    sb.AppendLine($"Last Name: {person.LastName}");
    sb.AppendLine($"Birth Date: {person.BirthDate}");
    sb.AppendLine($"Gender: {person.Gender}");
    sb.AppendLine($"Marital Status: {person.MaritalStatus}");
    sb.AppendLine($"Salary: {person.Salary}");
    sb.AppendLine("--------------------------------------------");
}
Console.WriteLine( sb.ToString() );
Console.ReadLine();