using System;
using System.Collections.Generic;

namespace CybEth.SQLInjectionDemo.Web.Models;

public partial class Device
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Cost { get; set; }

    public string? Origin { get; set; }
}
