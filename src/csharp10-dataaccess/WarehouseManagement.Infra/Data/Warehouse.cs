using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Linq;
namespace WarehouseManagement.Infra.Data;

[DataContract]
[Table("Warehouse")]
[Index(nameof(Id), Name = "IDX_Warehouse_Id")]
public partial class Warehouse
{
    public Warehouse()
    {
        Items = new HashSet<Item>();
    }

    [Key]
    public Guid Id { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string Location { get; set; } = null!;

    public virtual ICollection<Item> Items { get; set; }
}
