using System.ComponentModel.DataAnnotations;

namespace customers.infra.Entities;

public class Address
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(150)]
    public string? Street { get; set; }

    [MaxLength(150)]
    public string? Street2 { get; set; }

    [MaxLength(150)]
    public string? City { get; set; }

    [MaxLength(150)]
    public string? County { get; set; }

    [MaxLength(10)]
    public string? PostalCode { get; set; }

    [MaxLength(150)]
    public string? Country { get; set; }

    [MaxLength(15)]
    public string? Latitude { get; set; }
    
    [MaxLength(15)]
    public string? Longitude { get; set; }

    [MaxLength(100)]
    public string? Timezone { get; set; }
}
