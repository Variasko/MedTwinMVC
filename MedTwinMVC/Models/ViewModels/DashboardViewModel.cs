using System;
using System.Collections.Generic;
using MedTwinMVC.Models.DatabaseModel;

namespace MedTwinMVC.Models.ViewModels
{
    public class DashboardViewModel
    {
        // Информация о пациенте
        public string PatientFullName { get; set; }
        public string PatientEmail { get; set; }
        public DateOnly PatientBirthday { get; set; }
        public int PatientAge { get; set; }

        // Последние показатели здоровья
        public List<HealthMetricWithTypeName> LatestHealthMetrics { get; set; }

        // Активные назначения
        public int ActivePrescriptionsCount { get; set; }
        public List<Prescription> ActivePrescriptions { get; set; }

        // Предстоящие визиты
        public int UpcomingConsultationsCount { get; set; }
        public List<Consultation> UpcomingConsultations { get; set; }

        // Статистика за сегодня
        public DateTime Today { get; set; }

        // Статистика дневника самочувствия
        public int WellnessEntriesCount { get; set; }
        public decimal? AverageMoodScore { get; set; }
    }

    public class HealthMetricWithTypeName
    {
        public string MetricTypeName { get; set; }
        public decimal Value { get; set; }
        public string UnitName { get; set; }
        public DateTime MeasuredAt { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }

        // Для определения, в норме ли показатель
        public bool IsNormal => MinValue.HasValue && MaxValue.HasValue
            ? Value >= MinValue.Value && Value <= MaxValue.Value
            : true;
    }
}