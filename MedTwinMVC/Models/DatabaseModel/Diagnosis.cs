using System;
using System.Collections.Generic;

namespace MedTwinMVC.Models.DatabaseModel;

public partial class Diagnosis
{
    public int Id { get; set; }

    public string CodeMkb { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int DiagnosisCategoryId { get; set; }

    public virtual DiagnosisCategory DiagnosisCategory { get; set; } = null!;

    public virtual ICollection<PatientHistory> PatientHistories { get; set; } = new List<PatientHistory>();
}
