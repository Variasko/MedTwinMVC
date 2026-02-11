using System;
using System.Collections.Generic;

namespace MedTwinMVC.Models.DatabaseModel;

public partial class Prescription
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public int MedicationId { get; set; }

    public decimal Quantity { get; set; }

    public int DoseUnitId { get; set; }

    public int FrequencyId { get; set; }

    public int DurationInDays { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int InstructionId { get; set; }

    public int PrescriptionStatusId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual DoseUnit DoseUnit { get; set; } = null!;

    public virtual Frequency Frequency { get; set; } = null!;

    public virtual Instruction Instruction { get; set; } = null!;

    public virtual Medication Medication { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;

    public virtual PrescriptionStatus PrescriptionStatus { get; set; } = null!;
}
