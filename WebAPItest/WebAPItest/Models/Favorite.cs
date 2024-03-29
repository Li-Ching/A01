﻿using System;
using System.Collections.Generic;

namespace WebAPItest.Models;

public partial class Favorite
{
    public int FavoriteId { get; set; }

    public string UserId { get; set; } = null!;

    public int FurnitureId { get; set; }

    public virtual Furniture Furniture { get; set; } = null!;
}
