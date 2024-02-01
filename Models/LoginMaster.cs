using System;
using System.Collections.Generic;

namespace IMS.Models;

public partial class LoginMaster
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }
}
