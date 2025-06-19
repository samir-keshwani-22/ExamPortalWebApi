using ExamPortal.API.Models;
using ExamPortal.API.Models.Common;

namespace ExamPortal.Business.Interfaces;

public interface IExamService
{
    Task<bool> CreateExamAsync(ExamCreate exam);

    Task<bool> DeleteExamAsync(int id);

    Task<bool> UpdateExamAsync(int id, ExamUpdate examUpdate);
    Task<Exam?> GetExamByIdAsync(int id);

    Task<PagedResponse<Exam>> ListExamsAsync(
    long pageIndex,
    long pageSize,
    string? title,
    DateOnly? startDateFrom,
    DateOnly? startDateTo,
    DateOnly? endDateFrom,
    DateOnly? endDateTo,
    long? createdBy);
}
