using System;
using System.Collections.Generic;

namespace IMS.Models;

public partial class TaxMaster
{
    public int Id { get; set; }

    public string? PercentageDesc { get; set; }

    public virtual ICollection<ItemWork> ItemWorks { get; set; } = new List<ItemWork>();
}
