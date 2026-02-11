using System;
using System.Collections.Generic;

namespace MedTwinMVC.Models.DatabaseModel;

public partial class MedicalCard
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public string BloodType { get; set; } = null!;

    public int Height { get; set; }

    public decimal Weight { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}
