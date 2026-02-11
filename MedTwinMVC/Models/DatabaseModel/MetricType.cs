using System;
using System.Collections.Generic;

namespace MedTwinMVC.Models.DatabaseModel;

public partial class MetricType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int UnitOfMetricTypeId { get; set; }

    public decimal? MinValue { get; set; }

    public decimal? MaxValue { get; set; }

    public virtual ICollection<HealthMetric> HealthMetrics { get; set; } = new List<HealthMetric>();

    public virtual UnitOfMetricType UnitOfMetricType { get; set; } = null!;
}
