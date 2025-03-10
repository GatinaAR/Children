using System;
using System.Collections.Generic;

namespace MyDiplom.Models;

public partial class ObservationJournal
{
    public int JournalId { get; set; }

    public int? ChildId { get; set; }

    public DateOnly? Date { get; set; }

    public string? Observation { get; set; }

    public int? TeacherId { get; set; }

    public virtual Child? Child { get; set; }

    public virtual User? Teacher { get; set; }
}
