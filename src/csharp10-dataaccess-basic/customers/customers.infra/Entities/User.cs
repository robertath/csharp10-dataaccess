using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace customers.infra.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(30)]
    public string UserName { get; set; }

    [PasswordPropertyText(true)]
    public string Password { get; set; }

    public Person Person { get; set; }

    public Guid PersonId { get; set; }
}
