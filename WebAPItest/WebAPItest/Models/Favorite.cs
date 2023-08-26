using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAPItest.Models;

public partial class Favorite
{
    public int FavoriteId { get; set; }
    public string? UserId { get; set; }

    public int FurnitureId { get; set; }

    public virtual Furniture? Furniture { get; set; }
    public virtual User? User { get; set; }
}
