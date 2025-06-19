using ExamPortal.API.Models.Entities;

namespace ExamPortal.Business.Interfaces;

public interface IExamRepository
{
    Task<bool> CreateAsync(Exam exam);
    Task<bool> DeleteExamAsync(int id);
    Task<bool> UpdateExamAsync(Exam exam);

    Task<Exam?> GetExamByIdAsync(int id);
}
