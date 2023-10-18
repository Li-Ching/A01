using System;
using System.Collections.Generic;

namespace WebAPItest.Models;

public partial class Branch
{
    public int BranchId { get; set; }

    public string BranchName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int BrandId { get; set; }

    public virtual Brand Brand { get; set; } = null!;
}
