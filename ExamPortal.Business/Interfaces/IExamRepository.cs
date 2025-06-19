using ExamPortal.API.Models.Common;
using ExamPortal.API.Models.Entities;

namespace ExamPortal.Business.Interfaces;

public interface IExamRepository
{
    Task<bool> CreateAsync(Exam exam);
    Task<bool> DeleteExamAsync(int id);
    Task<bool> UpdateExamAsync(Exam exam);

    Task<Exam?> GetExamByIdAsync(int id);
    Task<PagedResult<Exam>> GetPagedExamsAsync(
    long pageIndex, long pageSize,
    string? title, DateOnly? startDateFrom, DateOnly? startDateTo,
    DateOnly? endDateFrom, DateOnly? endDateTo, long? createdBy);
}
