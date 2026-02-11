using System;
using System.Collections.Generic;

namespace MedTwinMVC.Models.DatabaseModel;

public partial class WellnessJournal
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public DateOnly EntryDate { get; set; }

    public int? MoodScore { get; set; }

    public string? Notes { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
