using Microsoft.AspNetCore.Http;

namespace ExamPortal.Business.Interfaces;

public interface ISalesDataService
{
    Task<(int successCount, int errorCount)> ProcessCsvUploadAsync(IFormFile file, CancellationToken cancellationToken);
}
