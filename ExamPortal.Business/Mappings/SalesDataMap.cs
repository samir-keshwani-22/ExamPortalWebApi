using CsvHelper.Configuration;
using ExamPortal.API.Models.Entities;

namespace ExamPortal.Business.Mappings;

public class SalesDataMap : ClassMap<SalesData>
{
    public SalesDataMap()
    {
        Map(m => m.Region).Name("Region");
        Map(m => m.Country).Name("Country");
        Map(m => m.ItemType).Name("Item Type");
        Map(m => m.SalesChannel).Name("Sales Channel");
        Map(m => m.OrderPriority).Name("Order Priority"); 
        Map(m => m.OrderId).Name("Order ID"); 
        Map(m => m.UnitsSold).Name("Units Sold");
        Map(m => m.UnitPrice).Name("Unit Price");
        Map(m => m.UnitCost).Name("Unit Cost");
        Map(m => m.TotalRevenue).Name("Total Revenue");
        Map(m => m.TotalCost).Name("Total Cost");
        Map(m => m.TotalProfit).Name("Total Profit");
        Map(m => m.OrderDate).Name("Order Date").Convert(args => DateTime.SpecifyKind(args.Row.GetField<DateTime>("Order Date"), DateTimeKind.Utc));
        Map(m => m.ShipDate).Name("Ship Date").Convert(args => DateTime.SpecifyKind(args.Row.GetField<DateTime>("Ship Date"), DateTimeKind.Utc));

    }
}
