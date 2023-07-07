using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAPItest.Models;

public partial class Furniture
{
    public int FurnitureId { get; set; }

    public string? Type { get; set; }

    public string? Color { get; set; }

    public string? Style { get; set; }

    public int BrandId { get; set; }

    public string? Location { get; set; }

    public string? Picture { get; set; }

    public virtual Brand? Brand { get; set; }

}
