using System;
using System.Collections.Generic;

namespace project.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int? BrandId { get; set; }

    public int? CategoryId { get; set; }

    public string? ProductName { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public string? ProductImagePath { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
