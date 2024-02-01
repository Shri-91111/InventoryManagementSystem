using System;
using System.Collections.Generic;

namespace IMS.Models;

public partial class Employeemst
{
    public int Id { get; set; }

    public string? Fempcode { get; set; }

    public string? Fempname { get; set; }

    public string? Fempdesig { get; set; }

    public string? Fapptype { get; set; }

    public string? Faccno { get; set; }

    public string? Fdeptcode { get; set; }

    public DateTime? Fdoapp { get; set; }

    public string? DaCategory { get; set; }

    public string? CcaGroup { get; set; }

    public string? HraLoc { get; set; }

    public bool? Nps { get; set; }

    public int? LeaveCl { get; set; }

    public int? LeaveRh { get; set; }

    public int? LeaveEl { get; set; }

    public int? Porder { get; set; }

    public string? El { get; set; }

    public string? Print { get; set; }

    public bool? EmpIsActive { get; set; }

    public string? CategoryType { get; set; }

    public string? EmployeeStatus { get; set; }

    public string? EmployeeCreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? EmployeeUpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? BiometricCode { get; set; }

    public string? MacAddress { get; set; }

    public string? IpAddress { get; set; }

    public string? Aicte { get; set; }

    public int? PayScaleSchemeCode { get; set; }

    public DateTime? PreIncrementDate { get; set; }

    public DateTime? CurrentIncrementDate { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public DateTime? DateOfRetirement { get; set; }

    public int? CurrentBasicPay { get; set; }

    public int? PayBondId { get; set; }
}
