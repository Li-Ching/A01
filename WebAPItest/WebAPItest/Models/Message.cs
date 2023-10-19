using System;
using System.Collections.Generic;

namespace WebAPItest.Models;

public partial class Message
{
    public Guid MessageId { get; set; }

    public string UserId { get; set; } = null!;

    public int FurnitureId { get; set; }

    public string Message1 { get; set; } = null!;

    public DateTime MessageTime { get; set; }

    public bool IsDelete { get; set; }

    public virtual Furniture Furniture { get; set; } = null!;
}
