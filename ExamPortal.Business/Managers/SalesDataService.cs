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
    public async Task<(int successCount, int errorCount)> ProcessCsvUploadAsync(IFormFile file, CancellationToken cancellationToken)
    {
        int success = 0, error = 0;

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null,
        };

        using var stream = new BufferedStream(file.OpenReadStream(), bufferSize: 8192);
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, config);
        csv.Context.RegisterClassMap<SalesDataMap>();
        var seenHashes = new HashSet<string>();
        var buffer = new List<SalesData>();
        int batchSize = 5000;

        await foreach (var record in csv.GetRecordsAsync<SalesData>().WithCancellation(cancellationToken))
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                var rowHash = ComputeRowHash(record);
                if (seenHashes.Contains(rowHash))
                {
                    error++;
                    continue;
                }
                record.RowHash = rowHash;
                seenHashes.Add(rowHash);
                record.CreatedDate = DateTime.UtcNow;
                buffer.Add(record);

                if (buffer.Count >= batchSize)
                {
                    var inserted = await InsertUniqueHashBatch(buffer,cancellationToken);
                    success += inserted;
                    error += buffer.Count - inserted;
                    buffer.Clear();
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch
            {
                error++;
            }
        }

        if (buffer.Any())
        {
            try
            {
                var inserted = await InsertUniqueHashBatch(buffer,cancellationToken);
                success += inserted;
                error += buffer.Count - inserted;
            }
            catch (OperationCanceledException)
            {
                throw; 
            }
            catch
            {
                error += buffer.Count;
            }
        }
        return (success, error);
    }

    private async Task<int> InsertUniqueHashBatch(List<SalesData> batch,CancellationToken cancellationToken )
    {
        var hashes = batch.Select(x => x.RowHash).Distinct();
        var existingHashes = await _salesDataRepository.GetExistingRowHashesAsync(hashes,cancellationToken);

        var uniqueRecords = batch
            .Where(x => !existingHashes.Contains(x.RowHash))
            .ToList();

        if (!uniqueRecords.Any())
            return 0;

        return await _salesDataRepository.AddSalesDataBulkAsync(uniqueRecords,cancellationToken);
    }


    // 44 char ane slow hatu 

    // private string ComputeRowHash(SalesData s)
    // {
    //     var rowString = $"{s.Region}|{s.Country}|{s.ItemType}|{s.SalesChannel}|{s.OrderPriority}|" +
    //                     $"{s.OrderDate:O}|{s.ShipDate:O}|{s.UnitsSold}|{s.UnitPrice}|{s.UnitCost}|" +
    //                     $"{s.TotalRevenue}|{s.TotalCost}|{s.TotalProfit}|{s.OrderId}";

    //     using var sha256 = System.Security.Cryptography.SHA256.Create();
    //     var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(rowString));
    //     return Convert.ToBase64String(hashBytes);
    // }

    // 16 char and fast 
    private static string ComputeRowHash(SalesData s)
    {
        var rowString = $"{s.Region}|{s.Country}|{s.ItemType}|{s.SalesChannel}|{s.OrderPriority}|"
                      + $"{s.OrderDate:O}|{s.ShipDate:O}|{s.UnitsSold}|{s.UnitPrice}|{s.UnitCost}|"
                      + $"{s.TotalRevenue}|{s.TotalCost}|{s.TotalProfit}|{s.OrderId}";

        var bytes = System.Text.Encoding.UTF8.GetBytes(rowString);
        ulong hash = K4os.Hash.xxHash.XXH64.DigestOf(bytes);
        return hash.ToString("X16");
    }

}
