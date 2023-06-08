using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace customers.infra.Entities;

public class Person
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(150)]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }

    [Required]
    public Gender Gender { get; set; }

    [MaxLength(20)]
    public string? MaritalStatus { get; set; }

    public ICollection<Address>? Address { get; set; }

    public string? Phone { get; set; }
    public string? Fax { get; set; }
    public string? HomePage { get; set; }

    public decimal? Salary { get; set; }
}

public enum Gender
{
    Female = 1,
    Male = 2,
    Other = 3,
    Unknown = 4
}

