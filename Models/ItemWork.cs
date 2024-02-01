using System;
using System.Collections.Generic;

namespace IMS.Models;

public partial class ItemWork
{
    public int Id { get; set; }

    public int? Pid { get; set; }

    public int? ItemDesc { get; set; }

    public int? TypeId { get; set; }

    public int? Subtypeid { get; set; }

    public int? VendoId { get; set; }

    public int? NumOfQuantity { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public int? ThroghId { get; set; }

    public string? Amount { get; set; }

    public int Tax { get; set; }

    public virtual SubItemsMaster? Subtype { get; set; }

    public virtual TaxMaster? TaxNavigation { get; set; } 
}
