using System;
using System.Collections.Generic;

namespace MedTwinMVC.Models.DatabaseModel;

public partial class DiagnosisStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<PatientHistory> PatientHistories { get; set; } = new List<PatientHistory>();
}
