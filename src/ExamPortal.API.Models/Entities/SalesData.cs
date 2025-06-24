using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamPortal.API.Models.Entities;

public class SalesData
{
    [Key]
    public int Id { get; set; }
 
    [MaxLength(100)]
    public  string Region { get; set; }

    
    [MaxLength(100)]
    public string Country { get; set; }

    
    [MaxLength(100)]
    public string ItemType { get; set; }

    
    [MaxLength(50)]
    public string SalesChannel { get; set; }

    [MaxLength(10)]
    public string OrderPriority { get; set; }

    public DateTime OrderDate { get; set; }

  
    public long OrderId { get; set; }

    public DateTime ShipDate { get; set; }

    public int UnitsSold { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitCost { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalRevenue { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalCost { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalProfit { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    
}
