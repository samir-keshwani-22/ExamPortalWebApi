using ExamPortal.API.Models.Entities;

namespace ExamPortal.Business.Interfaces;

public interface ISalesDataRepository
{
    Task<int> AddSalesDataBulkAsync(List<SalesData> data, CancellationToken cancellationToken);
    // Task<HashSet<long>> GetExistingOrderIdsAsync(IEnumerable<long> orderIds);

    Task<HashSet<string>> GetExistingRowHashesAsync(IEnumerable<string> hashes, CancellationToken cancellationToken);


}
