using System;
using System.Collections.Generic;

namespace CybEth.SQLInjectionDemo.Web.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Address { get; set; }

    public string? TelephoneNumber { get; set; }
}
