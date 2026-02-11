using System;
using System.Collections.Generic;

namespace MedTwinMVC.Models.DatabaseModel;

public partial class PatientComplaint
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int SymptomId { get; set; }

    public DateOnly ComplaintDate { get; set; }

    public int SeverityId { get; set; }

    public string? Description { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual Severity Severity { get; set; } = null!;

    public virtual Symptom Symptom { get; set; } = null!;
}
