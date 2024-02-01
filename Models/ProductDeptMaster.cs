using System;
using System.Collections.Generic;

namespace IMS.Models;

public partial class ProductDeptMaster
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<SubItemsMaster> SubItemsMasters { get; set; } = new List<SubItemsMaster>();
}
