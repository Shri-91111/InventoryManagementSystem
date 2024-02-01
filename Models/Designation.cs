using System;
using System.Collections.Generic;

namespace IMS.Models;

public partial class Designation
{
    public int Id { get; set; }

    public string? Fdesigcode { get; set; }

    public string? Fdesigname { get; set; }

    public string? Fgroup { get; set; }

    public int? Fgat { get; set; }

    public int? Fgradebkp { get; set; }
}
