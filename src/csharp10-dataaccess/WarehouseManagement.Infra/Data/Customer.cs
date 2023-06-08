using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WarehouseManagement.Infra.Data;

[DataContract]
[Table("Customer")]
[Index(nameof(Id), Name = "IDX_Customer_Id")]
[Index(nameof(Name), Name = "IDX_Customer_Name")]
public class Customer
{
    public Customer()
    {
        Orders = new HashSet<Order>();
    }

    [Key]
    public Guid Id { get; set; }

    [Column(TypeName = "varchar(200)")]
    public string Name { get; set; } = null!;

    [Column(TypeName = "varchar(200)")]
    public string Address { get; set; } = null!;
            
    [Column(TypeName = "varchar(10)")]
    public string PostalCode { get; set; } = null!;

    [Column(TypeName = "varchar(100)")]
    public string Country { get; set; } = null!;

    [Column(TypeName = "varchar(20)")]
    public string PhoneNumber { get; set; } = null!;

    //[NotMapped]
    //public Lazy<byte[]>? Logo {get;set;}

    private byte[]? logo;
    [NotMapped]
    public byte[]? Logo
    {
        get
        {
            if (logo is null)
            {
                logo = LogoService.GetFor(Name);
            }
            return logo;
        }
        set => logo = value;
    }

    public virtual ICollection<Order> Orders { get; set; }
}


public class CustomerProxy : Customer
{
    //public byte[]? logo;
    //public override byte[]? Logo
    //{
    //    get
    //    {
    //        if (logo is null)
    //        {
    //            logo = LogoService.GetFor(Name);
    //        }
    //        return logo;
    //    }        
    //}
}