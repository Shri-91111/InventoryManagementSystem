using System;
using System.Collections.Generic;

namespace IMS.Models;

public partial class SubProductMaster
{
    public int Id { get; set; }

    public string? SubProdcutDesc { get; set; }

    public int? Pid { get; set; }

    public virtual ProductMaster? PidNavigation { get; set; }
}
