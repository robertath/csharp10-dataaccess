using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace WarehouseManagement.Infra.Data;

[DataContract]
[Table("LineItem")]
[Index(nameof(ItemId), Name = "IDX_LineItem_ItemId")]
[Index(nameof(OrderId), Name = "IDX_LineItem_OrderId")]
public partial class LineItem
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ItemId { get; set; }

    public int Quantity { get; set; }

    [Required]
    public Guid OrderId { get; set; }

    [ForeignKey(nameof(ItemId))]
    public virtual Item Item { get; set; } = null!;

    [ForeignKey(nameof(OrderId))]
    public virtual Order Order { get; set; } = null!;
}
