using System;
using System.Collections.Generic;

namespace WebAPItest.Models;

public partial class Brand
{
    public int BrandId { get; set; }

    public string Brand1 { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Logo { get; set; } = null!;

    public string Url { get; set; } = null!;

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual ICollection<Furniture> Furnitures { get; set; } = new List<Furniture>();
}
