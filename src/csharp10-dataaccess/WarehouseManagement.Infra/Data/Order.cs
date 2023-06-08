using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace WarehouseManagement.Infra.Data;

[DataContract]
[Table("Order")]
[Index(nameof(Id), Name = "IDX_Order_Id")]
public partial class Order
{
    public Order()
    {
        LineItems = new HashSet<LineItem>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    public DateTimeOffset CreatedAt { get; set; }

    public Guid CustomerId { get; set; }

    [Required]
    [ForeignKey(nameof(CustomerId))]
    public virtual Customer Customer { get; set; } = null!;

    public Guid ShippingProviderId { get; set; }

    [Required]
    [ForeignKey(nameof(ShippingProviderId))]
    public virtual ShippingProvider ShippingProvider { get; set; } = null!;

    public virtual ICollection<LineItem> LineItems { get; set; }
}
