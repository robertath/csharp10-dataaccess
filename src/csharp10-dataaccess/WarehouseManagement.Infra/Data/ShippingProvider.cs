using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace WarehouseManagement.Infra.Data;

[DataContract]
[Table("ShippingProvider")]
[Index(nameof(Id), Name = "IDX_ShippingProvider_Id")]
public partial class ShippingProvider
{
    public ShippingProvider()
    {
        Orders = new HashSet<Order>();
    }

    [Key]
    public Guid Id { get; set; }

    [Column(TypeName = "varchar(200)")]
    public string Name { get; set; } = null!;

    [Column(TypeName = "decimal(10,2)")]
    public decimal FreightCost { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
}
