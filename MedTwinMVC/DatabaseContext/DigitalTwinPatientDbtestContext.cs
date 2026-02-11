using System;
using System.Collections.Generic;
using MedTwinMVC.Models.DatabaseModel;
using Microsoft.EntityFrameworkCore;

namespace MedTwinMVC.DatabaseContext;

public partial class DigitalTwinPatientDbtestContext : DbContext
{
    public DigitalTwinPatientDbtestContext()
    {
    }

    public DigitalTwinPatientDbtestContext(DbContextOptions<DigitalTwinPatientDbtestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Consultation> Consultations { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Diagnosis> Diagnoses { get; set; }

    public virtual DbSet<DiagnosisCategory> DiagnosisCategories { get; set; }

    public virtual DbSet<DiagnosisStatus> DiagnosisStatuses { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoseUnit> DoseUnits { get; set; }

    public virtual DbSet<Frequency> Frequencies { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<HealthMetric> HealthMetrics { get; set; }

    public virtual DbSet<Instruction> Instructions { get; set; }

    public virtual DbSet<MedicalCard> MedicalCards { get; set; }

    public virtual DbSet<Medication> Medications { get; set; }

    public virtual DbSet<MetricType> MetricTypes { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientComplaint> PatientComplaints { get; set; }

    public virtual DbSet<PatientHistory> PatientHistories { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<PrescriptionStatus> PrescriptionStatuses { get; set; }

    public virtual DbSet<Severity> Severities { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<Symptom> Symptoms { get; set; }

    public virtual DbSet<SymptomCategory> SymptomCategories { get; set; }

    public virtual DbSet<UnitOfMetricType> UnitOfMetricTypes { get; set; }

    public virtual DbSet<View1> View1s { get; set; }

    public virtual DbSet<VwPatientHealthMetricsAggregated> VwPatientHealthMetricsAggregateds { get; set; }

    public virtual DbSet<WellnessJournal> WellnessJournals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MainAdmin\\SQLEXPRESS01;Initial Catalog=DigitalTwinPatientDBTest;Integrated Security=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Address");

            entity.Property(e => e.Building).HasMaxLength(7);
            entity.Property(e => e.City).HasMaxLength(30);
            entity.Property(e => e.Street).HasMaxLength(30);
        });

        modelBuilder.Entity<Consultation>(entity =>
        {
            entity.ToTable("Consultation");

            entity.Property(e => e.DateConsultation).HasColumnType("datetime");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Consultations)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Consultation_Doctor");

            entity.HasOne(d => d.Patient).WithMany(p => p.Consultations)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Consultation_Patient");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Diagnosis>(entity =>
        {
            entity.ToTable("Diagnosis");

            entity.Property(e => e.CodeMkb)
                .HasMaxLength(5)
                .HasColumnName("CodeMKB");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.DiagnosisCategory).WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.DiagnosisCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Diagnosis_DiagnosisCategory");
        });

        modelBuilder.Entity<DiagnosisCategory>(entity =>
        {
            entity.ToTable("DiagnosisCategory");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<DiagnosisStatus>(entity =>
        {
            entity.ToTable("DiagnosisStatus");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.ToTable("Doctor");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Patronymic).HasMaxLength(30);
            entity.Property(e => e.Phone).HasMaxLength(12);
            entity.Property(e => e.Surname).HasMaxLength(30);

            entity.HasOne(d => d.Department).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Doctor_Department");

            entity.HasOne(d => d.Specialization).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.SpecializationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Doctor_Specialization");
        });

        modelBuilder.Entity<DoseUnit>(entity =>
        {
            entity.ToTable("DoseUnit");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Frequency>(entity =>
        {
            entity.ToTable("Frequency");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.ToTable("Gender");

            entity.Property(e => e.Name).HasMaxLength(7);
        });

        modelBuilder.Entity<HealthMetric>(entity =>
        {
            entity.ToTable("HealthMetric");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.MeasuredAt).HasColumnType("datetime");
            entity.Property(e => e.Value).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.MetricType).WithMany(p => p.HealthMetrics)
                .HasForeignKey(d => d.MetricTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HealthMetric_MetricType");

            entity.HasOne(d => d.Patient).WithMany(p => p.HealthMetrics)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HealthMetric_Patient");
        });

        modelBuilder.Entity<Instruction>(entity =>
        {
            entity.ToTable("Instruction");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<MedicalCard>(entity =>
        {
            entity.ToTable("MedicalCard");

            entity.Property(e => e.BloodType).HasMaxLength(5);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Patient).WithMany(p => p.MedicalCards)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicalCard_Patient");
        });

        modelBuilder.Entity<Medication>(entity =>
        {
            entity.ToTable("Medication");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<MetricType>(entity =>
        {
            entity.ToTable("MetricType");

            entity.Property(e => e.MaxValue).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MinValue).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.UnitOfMetricType).WithMany(p => p.MetricTypes)
                .HasForeignKey(d => d.UnitOfMetricTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MetricType_UnitOfMetricType");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("Patient");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Patronymic).HasMaxLength(30);
            entity.Property(e => e.Phone).HasMaxLength(12);
            entity.Property(e => e.Surname).HasMaxLength(30);

            entity.HasOne(d => d.Address).WithMany(p => p.Patients)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_Patient_Address");

            entity.HasOne(d => d.Gender).WithMany(p => p.Patients)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patient_Gender");
        });

        modelBuilder.Entity<PatientComplaint>(entity =>
        {
            entity.ToTable("PatientComplaint");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientComplaints)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientComplaint_Patient");

            entity.HasOne(d => d.Severity).WithMany(p => p.PatientComplaints)
                .HasForeignKey(d => d.SeverityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientComplaint_Severity");

            entity.HasOne(d => d.Symptom).WithMany(p => p.PatientComplaints)
                .HasForeignKey(d => d.SymptomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientComplaint_Symptom");
        });

        modelBuilder.Entity<PatientHistory>(entity =>
        {
            entity.ToTable("PatientHistory");

            entity.HasOne(d => d.Diagnosis).WithMany(p => p.PatientHistories)
                .HasForeignKey(d => d.DiagnosisId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientHistory_Diagnosis");

            entity.HasOne(d => d.DiagnosisStatus).WithMany(p => p.PatientHistories)
                .HasForeignKey(d => d.DiagnosisStatusId)
                .HasConstraintName("FK_PatientHistory_DiagnosisStatus");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientHistories)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientHistory_Patient");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.ToTable("Prescription");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasColumnType("decimal(6, 2)");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescription_Doctor");

            entity.HasOne(d => d.DoseUnit).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.DoseUnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescription_DoseUnit");

            entity.HasOne(d => d.Frequency).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.FrequencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescription_Frequency");

            entity.HasOne(d => d.Instruction).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.InstructionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescription_Instruction");

            entity.HasOne(d => d.Medication).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.MedicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescription_Medication");

            entity.HasOne(d => d.Patient).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescription_Patient");

            entity.HasOne(d => d.PrescriptionStatus).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.PrescriptionStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescription_PrescriptionStatus");
        });

        modelBuilder.Entity<PrescriptionStatus>(entity =>
        {
            entity.ToTable("PrescriptionStatus");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Severity>(entity =>
        {
            entity.ToTable("Severity");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.ToTable("Specialization");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Symptom>(entity =>
        {
            entity.ToTable("Symptom");

            entity.Property(e => e.Name).HasMaxLength(30);

            entity.HasOne(d => d.SymptomCategory).WithMany(p => p.Symptoms)
                .HasForeignKey(d => d.SymptomCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Symptom_SymptomCategory");
        });

        modelBuilder.Entity<SymptomCategory>(entity =>
        {
            entity.ToTable("SymptomCategory");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<UnitOfMetricType>(entity =>
        {
            entity.ToTable("UnitOfMetricType");

            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<View1>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_1");

            entity.Property(e => e.BloodType).HasMaxLength(5);
            entity.Property(e => e.CodeMkb)
                .HasMaxLength(5)
                .HasColumnName("CodeMKB");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Expr1).HasMaxLength(30);
            entity.Property(e => e.Expr2).HasMaxLength(30);
            entity.Property(e => e.Expr3).HasMaxLength(30);
            entity.Property(e => e.Expr4).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Patronymic).HasMaxLength(30);
            entity.Property(e => e.Surname).HasMaxLength(30);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<VwPatientHealthMetricsAggregated>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_PatientHealthMetricsAggregated");

            entity.Property(e => e.AverageValue).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.FirstMeasurementDate).HasColumnType("datetime");
            entity.Property(e => e.FullName).HasMaxLength(92);
            entity.Property(e => e.LastMeasurementDate).HasColumnType("datetime");
            entity.Property(e => e.MaxValue).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MetricTypeName).HasMaxLength(100);
            entity.Property(e => e.MinValue).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UnitName).HasMaxLength(20);
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<WellnessJournal>(entity =>
        {
            entity.ToTable("WellnessJournal");

            entity.HasOne(d => d.Patient).WithMany(p => p.WellnessJournals)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WellnessJournal_Patient");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
