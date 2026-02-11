using System;
using System.Collections.Generic;

namespace MedTwinMVC.Models.DatabaseModel;

public partial class VwPatientHealthMetricsAggregated
{
    public int PatientId { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public int? Age { get; set; }

    public int Height { get; set; }

    public decimal Weight { get; set; }

    public string MetricTypeName { get; set; } = null!;

    public string UnitName { get; set; } = null!;

    public int? MeasurementsCount { get; set; }

    public decimal? AverageValue { get; set; }

    public decimal? MinValue { get; set; }

    public decimal? MaxValue { get; set; }

    public double? StdDevValue { get; set; }

    public DateTime? FirstMeasurementDate { get; set; }

    public DateTime? LastMeasurementDate { get; set; }
}
