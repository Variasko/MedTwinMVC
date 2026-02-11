using System;
using System.Collections.Generic;

namespace MedTwinMVC.Models.DatabaseModel;

public partial class Severity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<PatientComplaint> PatientComplaints { get; set; } = new List<PatientComplaint>();
}
