using EFCore.BulkExtensions;
using ExamPortal.API.Models.Entities;
using ExamPortal.Business.Interfaces;
using ExamPortal.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
namespace ExamPortal.DataAccess.Repositories;

public class SalesDataRepository : ISalesDataRepository
{
    private readonly ExamPortalContext _context;

    public SalesDataRepository(ExamPortalContext context)
    {
        _context = context;
    }

    public async Task<int> AddSalesDataBulkAsync(List<SalesData> data, CancellationToken cancellationToken)
    {
        _context.ChangeTracker.AutoDetectChangesEnabled = false;
        int insertedCount = 0;
        try
        {
            await _context.BulkInsertAsync(data, new BulkConfig
            {
                BatchSize = 20000,
                SetOutputIdentity = false,
                // in returned obj not seting the id back for performance
            });
            insertedCount = data.Count;
        }
        finally
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = true;
        }
        return insertedCount;
    }

    public async Task<HashSet<string>> GetExistingRowHashesAsync(IEnumerable<string> hashes, CancellationToken cancellationToken)
    {
        var list = await _context.SalesData
        .Where(sd => hashes.Contains(sd.RowHash))
        .Select(sd => sd.RowHash)
        .ToListAsync(cancellationToken);
        return list.ToHashSet();
    }


    // public async Task AddSalesDataBulkAsync(List<SalesData> data)
    // {
    //     await _context.SalesData.AddRangeAsync(data);
    //     await _context.SaveChangesAsync();
    // } 

    // public async Task<HashSet<long>> GetExistingOrderIdsAsync(IEnumerable<long> orderIds)
    // {
    //     var list = await _context.SalesData
    //         .Where(sd => orderIds.Contains(sd.OrderId))
    //         .Select(sd => sd.OrderId)
    //         .ToListAsync();

    //     return list.ToHashSet();
    // }



}