using System;
using System.Collections.Generic;

namespace MyDiplom.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public int? GroupId { get; set; }

    public string? Date { get; set; }

    public string? Activity { get; set; }

    public string? Description { get; set; }

    public virtual Group? Group { get; set; }
}
