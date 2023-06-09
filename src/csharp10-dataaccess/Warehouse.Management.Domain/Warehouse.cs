﻿namespace WarehouseManagement.Domain;

public class Warehouse
{
    public Guid Id { get; set; }
    public string Location { get; set; }
    public ICollection<Item> Items { get; set; }
}