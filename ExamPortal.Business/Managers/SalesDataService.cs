using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using ExamPortal.API.Models.Entities;
using ExamPortal.Business.Interfaces;
using ExamPortal.Business.Mappings;
using Microsoft.AspNetCore.Http;

namespace ExamPortal.Business.Managers;

public class SalesDataService : ISalesDataService
{
    private readonly ISalesDataRepository _salesDataRepository;

    public SalesDataService(ISalesDataRepository salesDataRepository)
    {
        _salesDataRepository = salesDataRepository;
    }
    public async Task<(int successCount, int errorCount)> ProcessCsvUploadAsync(IFormFile file)
    {
        int success = 0;
        int error = 0;
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null,
        }; 
        using var stream = file.OpenReadStream();
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, config);
        var fileOrderIdSet = new HashSet<long>();
        csv.Context.RegisterClassMap<SalesDataMap>();
        var buffer = new List<SalesData>();
        int batchSize = 20000;

        await foreach (var record in csv.GetRecordsAsync<SalesData>())
        {
            try
            {
                if (fileOrderIdSet.Contains(record.OrderId))
                {
                    error++;
                    continue;
                }
                fileOrderIdSet.Add(record.OrderId);
                record.CreatedDate = DateTime.UtcNow;
                buffer.Add(record);

                if (buffer.Count >= batchSize)
                {
                    var batchSuccess = await InsertNonDuplicateBatch(buffer);
                    success += batchSuccess;
                    error += buffer.Count - batchSuccess;
                    buffer.Clear();
                }
            }
            catch
            {
                error++;
            }
        }

        if (buffer.Count > 0)
        {
            try
            {
                var batchSuccess = await InsertNonDuplicateBatch(buffer);
                success += batchSuccess;
                error += buffer.Count - batchSuccess;
            }
            catch
            {
                error += buffer.Count;
            }
        }

        return (success, error);
    }
 
    private async Task<int> InsertNonDuplicateBatch(List<SalesData> batch)
    {
        var orderIds = batch.Select(x => x.OrderId).Distinct();
        var existingOrderIds = await _salesDataRepository.GetExistingOrderIdsAsync(orderIds);

        var uniqueRecords = batch
            .Where(x => !existingOrderIds.Contains(x.OrderId))
            .ToList();

        if (!uniqueRecords.Any())
            return 0;

        return await _salesDataRepository.AddSalesDataBulkAsync(uniqueRecords);
    }

}
