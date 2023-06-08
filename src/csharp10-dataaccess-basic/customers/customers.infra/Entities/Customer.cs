using System.ComponentModel.DataAnnotations;

namespace customers.infra.Entities;


public class Customer
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid PersonId { get; set; }

    [Required]
    public Person Person { get; set; }

    [Required]
    public Substripions Subscription { get; set; }
}


public enum Substripions
{
    GOLDEN = 1,
    PLATINUM = 2,
    FREE = 3
}