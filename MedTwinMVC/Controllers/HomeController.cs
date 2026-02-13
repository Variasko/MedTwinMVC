using System;
using System.Collections.Generic;
using System.Linq;
using MedTwinMVC.DatabaseContext;
using MedTwinMVC.Models.DatabaseModel;
using MedTwinMVC.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedTwinMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly DigitalTwinPatientDbtestContext _db = new DigitalTwinPatientDbtestContext();

        public IActionResult Index()
        {
            // Получаем текущего пациента из авторизации
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
            var patient = _db.Patients
                .Include(p => p.Gender)
                .FirstOrDefault(p => p.Id == userId);

            if (patient == null)
            {
                return RedirectToAction("Login", "Authorization");
            }

            // Рассчитываем возраст
            var today = DateTime.Today;
            var age = today.Year - patient.Birthday.Year;
            if (patient.Birthday > DateOnly.FromDateTime(today.AddYears(-age)))
                age--;

            var viewModel = new DashboardViewModel
            {
                PatientFullName = $"{patient.Surname} {patient.Name} {patient.Patronymic ?? ""}".Trim(),
                PatientEmail = patient.Email,
                PatientBirthday = patient.Birthday,
                PatientAge = age,
                Today = DateTime.Now
            };

            // Последние показатели здоровья (за последние 7 дней, последние 10 записей)
            var latestMetrics = _db.HealthMetrics
                .Where(hm => hm.PatientId == userId && hm.MeasuredAt >= DateTime.Now.AddDays(-7))
                .OrderByDescending(hm => hm.MeasuredAt)
                .Take(10)
                .Select(hm => new HealthMetricWithTypeName
                {
                    MetricTypeName = hm.MetricType.Name,
                    Value = hm.Value,
                    UnitName = hm.MetricType.UnitOfMetricType.Name,
                    MeasuredAt = hm.MeasuredAt,
                    MinValue = hm.MetricType.MinValue,
                    MaxValue = hm.MetricType.MaxValue
                })
                .ToList();

            viewModel.LatestHealthMetrics = latestMetrics;

            // Активные назначения (статус "активный" - предположим, что это статус с Id = 1)
            var todayDateOnly = DateOnly.FromDateTime(DateTime.Today);

            var activePrescriptions = _db.Prescriptions
                .Include(p => p.Medication)
                .Include(p => p.DoseUnit)
                .Include(p => p.Frequency)
                .Include(p => p.PrescriptionStatus)
                .Where(p => p.PatientId == userId &&
                           p.PrescriptionStatusId == 1 &&
                           p.StartDate <= todayDateOnly &&
                           (p.EndDate == null || p.EndDate >= todayDateOnly))
                .OrderBy(p => p.StartDate)
                .Take(5)
                .ToList();

            viewModel.ActivePrescriptions = activePrescriptions;
            viewModel.ActivePrescriptionsCount = activePrescriptions.Count;

            // Предстоящие визиты (на этой неделе)
            var weekStart = DateTime.Now.Date;
            var weekEnd = DateTime.Now.Date.AddDays(7);

            var upcomingConsultations = _db.Consultations
                .Include(c => c.Doctor)
                .Where(c => c.PatientId == userId && c.DateConsultation >= weekStart && c.DateConsultation <= weekEnd)
                .OrderBy(c => c.DateConsultation)
                .Take(5)
                .ToList();

            viewModel.UpcomingConsultations = upcomingConsultations;
            viewModel.UpcomingConsultationsCount = upcomingConsultations.Count;

            // Статистика дневника самочувствия (за последнюю неделю)
            var weekAgo = DateOnly.FromDateTime(DateTime.Today.AddDays(-7));

            var wellnessEntries = _db.WellnessJournals
                .Where(wj => wj.PatientId == userId && wj.EntryDate >= weekAgo)
                .ToList();

            viewModel.WellnessEntriesCount = wellnessEntries.Count;
            viewModel.AverageMoodScore = (decimal) wellnessEntries
                .Where(wj => wj.MoodScore.HasValue)
                .Select(wj => wj.MoodScore.Value)
                .DefaultIfEmpty()
                .Average();

            // Передаем типы метрик для модального окна
            ViewBag.MetricTypes = _db.MetricTypes
                .Include(mt => mt.UnitOfMetricType)
                .ToList();

            return View(viewModel);
        }

        // Метод для быстрого добавления показателя
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddHealthMetric([FromBody] AddHealthMetricRequest request)
        {
            if (request == null || request.MetricTypeId <= 0)
            {
                return Json(new { success = false, message = "Некорректные данные" });
            }

            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

            var healthMetric = new HealthMetric
            {
                PatientId = userId,
                MetricTypeId = request.MetricTypeId,
                Value = request.Value,
                MeasuredAt = DateTime.Now,
                CreatedAt = DateTime.Now
            };

            _db.HealthMetrics.Add(healthMetric);
            _db.SaveChanges();

            return Json(new { success = true, message = "Показатель успешно добавлен" });
        }

        // Метод для отметки приема лекарства
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkMedicationTaken([FromBody] MarkMedicationRequest request)
        {
            if (request == null || request.PrescriptionId <= 0)
            {
                return Json(new { success = false, message = "Некорректные данные" });
            }

            // Здесь можно добавить логику отметки о приеме
            return Json(new { success = true, message = "Прием лекарства отмечен" });
        }

        // Метод для быстрой записи в дневник
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult QuickWellnessEntry([FromBody] WellnessEntryRequest request)
        {
            if (request == null || request.MoodScore <= 0)
            {
                return Json(new { success = false, message = "Некорректные данные" });
            }

            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

            var journalEntry = new WellnessJournal
            {
                PatientId = userId,
                EntryDate = DateOnly.FromDateTime(DateTime.Now),
                MoodScore = request.MoodScore,
                Notes = request.Notes
            };

            _db.WellnessJournals.Add(journalEntry);
            _db.SaveChanges();

            return Json(new { success = true, message = "Запись в дневник добавлена" });
        }
    }

    // Классы для запросов
    public class AddHealthMetricRequest
    {
        public int MetricTypeId { get; set; }
        public decimal Value { get; set; }
    }

    public class MarkMedicationRequest
    {
        public int PrescriptionId { get; set; }
    }

    public class WellnessEntryRequest
    {
        public int MoodScore { get; set; }
        public string Notes { get; set; }
    }
}