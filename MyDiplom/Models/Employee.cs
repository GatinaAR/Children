using System;
using System.Collections.Generic;

namespace MyDiplom.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Position { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
