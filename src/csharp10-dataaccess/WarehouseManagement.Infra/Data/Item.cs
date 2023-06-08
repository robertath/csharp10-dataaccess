using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WarehouseManagement.Infra.Data;

[DataContract]
[Table("Item")]
[Index(nameof(Id), Name = "IDX_Item_Id")]
public partial class Item
{
    public Item()
    {
        LineItems = new HashSet<LineItem>();
        Warehouses = new HashSet<Warehouse>();
    }

    [Key]
    public Guid Id { get; set; }
        
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; } = null!;

    [Column(TypeName = "varchar(200)")]
    public string Description { get; set; } = null!;

    [Column(TypeName = "decimal(14,2)")]
    public decimal Price { get; set; }
        
    public int InStock { get; set; }

    public virtual ICollection<LineItem> LineItems { get; set; }

    public virtual ICollection<Warehouse> Warehouses { get; set; }
}
