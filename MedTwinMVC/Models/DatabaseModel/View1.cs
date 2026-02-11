using System;
using System.Collections.Generic;

namespace MedTwinMVC.Models.DatabaseModel;

public partial class View1
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public string BloodType { get; set; } = null!;

    public int Height { get; set; }

    public decimal Weight { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Expr1 { get; set; } = null!;

    public string Expr2 { get; set; } = null!;

    public string? Expr3 { get; set; }

    public string CodeMkb { get; set; } = null!;

    public string Expr4 { get; set; } = null!;
}
