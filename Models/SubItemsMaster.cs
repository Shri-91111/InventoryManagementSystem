using System;
using System.Collections.Generic;

namespace IMS.Models;

public partial class SubItemsMaster
{
    public int Id { get; set; }

    public string? SubItemName { get; set; }

    public int? MainitemId { get; set; }

    public string? SubItemConfig { get; set; }

    public virtual ICollection<ItemWork> ItemWorks { get; set; } = new List<ItemWork>();

    public virtual ProductDeptMaster? Mainitem { get; set; }
}
