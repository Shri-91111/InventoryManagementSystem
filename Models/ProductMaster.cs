using System;
using System.Collections.Generic;

namespace IMS.Models;

public partial class ProductMaster
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public virtual ICollection<SubProductMaster> SubProductMasters { get; set; } = new List<SubProductMaster>();
}
