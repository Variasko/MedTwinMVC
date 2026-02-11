using System;
using System.Collections.Generic;

namespace MedTwinMVC.Models.DatabaseModel;

public partial class Gender
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
