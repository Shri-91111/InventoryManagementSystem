using System;
using System.Collections.Generic;

namespace IMS.Models;

public partial class Vendor
{
    public int VendorId { get; set; }

    public string? VendorName { get; set; }

    public string? VendorAddress { get; set; }

    public string? PhoneNumber { get; set; }

    public string? EmailAddress { get; set; }
}
