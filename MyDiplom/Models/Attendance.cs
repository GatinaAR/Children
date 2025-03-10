using System;
using System.Collections.Generic;

namespace MyDiplom.Models;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int? ChildId { get; set; }

    public DateOnly? Date { get; set; }

    public string Status { get; set; } = null!;

    public virtual Child? Child { get; set; }
}
