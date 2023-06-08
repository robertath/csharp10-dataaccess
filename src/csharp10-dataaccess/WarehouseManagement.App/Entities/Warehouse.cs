﻿using System;
using System.Collections.Generic;

namespace WarehouseManagement.EF.App.Entities;

public partial class Warehouse
{
    public Warehouse()
    {
        Items = new HashSet<Item>();
    }

    public Guid Id { get; set; }
    public string Location { get; set; } = null!;

    public virtual ICollection<Item> Items { get; set; }
}
