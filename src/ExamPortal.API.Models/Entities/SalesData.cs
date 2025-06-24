using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamPortal.API.Models.Entities;

/// <summary>
/// Represents a sales data record for batch upload.
/// </summary>
public class SalesData
{
    /// <summary>
    /// Primary key of the sales record.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Sales region (e.g., Asia, Europe).
    /// </summary>
    [MaxLength(100)]
    public string Region { get; set; }

    /// <summary>
    /// Country where the sale occurred.
    /// </summary>
    [MaxLength(100)]
    public string Country { get; set; }

    /// <summary>
    /// Type of item sold (e.g., Electronics, Clothes).
    /// </summary>
    [MaxLength(100)]
    public string ItemType { get; set; }

    /// <summary>
    /// Sales channel (e.g., Online, Offline).
    /// </summary>
    [MaxLength(50)]
    public string SalesChannel { get; set; }

    /// <summary>
    /// Order priority (e.g., H for High, L for Low).
    /// </summary>
    [MaxLength(10)]
    public string OrderPriority { get; set; }

    /// <summary>
    /// Date when the order was placed.
    /// </summary>
    public DateTime OrderDate { get; set; }

    /// <summary>
    /// External order ID (can be non-unique).
    /// </summary>
    public long OrderId { get; set; }

    /// <summary>
    /// Date when the order was shipped.
    /// </summary>
    public DateTime ShipDate { get; set; }

    /// <summary>
    /// Number of units sold.
    /// </summary>
    public int UnitsSold { get; set; }

    /// <summary>
    /// Price per unit.
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Cost per unit.
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitCost { get; set; }

    /// <summary>
    /// Total revenue generated (UnitsSold × UnitPrice).
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalRevenue { get; set; }

    /// <summary>
    /// Total cost incurred (UnitsSold × UnitCost).
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalCost { get; set; }

    /// <summary>
    /// Total profit (TotalRevenue − TotalCost).
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalProfit { get; set; }

    /// <summary>
    /// Timestamp when the record was created.
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Hash of the row data for deduplication.
    /// </summary>
    [MaxLength(100)]
    public string RowHash { get; set; }

}
