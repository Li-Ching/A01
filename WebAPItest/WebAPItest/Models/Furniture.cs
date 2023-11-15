using System;
using System.Collections.Generic;

namespace WebAPItest.Models;

public partial class Furniture
{
    public int FurnitureId { get; set; }

    public string FurnitureName { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string Style { get; set; } = null!;

    public int BrandId { get; set; }

    public string Location { get; set; } = null!;

    public string Picture { get; set; } = null!;
    public string SceneName { get; set; } = null!;


    public virtual Brand Brand { get; set; } = null!;

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
