using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAPItest.Models;

public partial class Brand
{
    public int BrandId { get; set; }

    public string? Brand1 { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? Logo { get; set; }

    public virtual ICollection<Furniture>? Furnitures { get; set; }
}
