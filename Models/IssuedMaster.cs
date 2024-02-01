using System;
using System.Collections.Generic;

namespace IMS.Models;

public partial class IssuedMaster
{
    public int Id { get; set; }

    public int? ProductName { get; set; }

    public int? DeptType { get; set; }

    public DateTime? IssuedDate { get; set; }

    public string? Rol { get; set; }

    public string? IssuedPerson { get; set; }

    public string? IssuedPersonDesigntion { get; set; }

    public string? Remarks { get; set; }

    public string? BookNumber { get; set; }

    public string? PageNumber { get; set; }

    public string? SerialNumber { get; set; }
}
