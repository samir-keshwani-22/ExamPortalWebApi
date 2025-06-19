using ExamPortal.API.Models.Entities;

namespace ExamPortal.Business.Interfaces;

public interface IExamRepository
{
    Task<bool> CreateAsync(Exam exam);
    Task<bool> DeleteExamAsync(int id);

    Task<Exam?> GetExamByIdAsync(int id);
}
