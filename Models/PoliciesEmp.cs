﻿using System;
using System.Collections.Generic;

namespace HealthInsurance.Models;

public partial class PoliciesEmp
{
    public string EmployeeId { get; set; } = null!;

    public int PolicyId { get; set; }

    public string? PolicyName { get; set; }

    public decimal? PolicyAmount { get; set; }

    public decimal? PolicyDuration { get; set; }

    public DateTime? PolicyStartdate { get; set; }

    public DateTime? PolicyEnddate { get; set; }

    public int? CompanyId { get; set; }

    public string? CompanyName { get; set; }

    public int? HospitalId { get; set; }

    public decimal? Emi { get; set; }

    public virtual Company? Company { get; set; }

    public virtual Hospital? Hospital { get; set; }

    public virtual Policy Policy { get; set; } = null!;


}

