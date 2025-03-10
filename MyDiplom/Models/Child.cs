using System;
using System.Collections.Generic;

namespace MyDiplom.Models;

public partial class Child
{
    public int ChildId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateOnly? Birthdate { get; set; }

    public int? ParentId { get; set; }

    public int? GroupId { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Group? Group { get; set; }

    public virtual ICollection<ObservationJournal> ObservationJournals { get; set; } = new List<ObservationJournal>();

    public virtual User? Parent { get; set; }
}
