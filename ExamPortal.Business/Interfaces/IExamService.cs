using ExamPortal.API.Models;

namespace ExamPortal.Business.Interfaces;

public interface IExamService
{
    Task<bool> CreateExamAsync(ExamCreate exam);

    Task<bool> DeleteExamAsync(int id);

    Task<bool> UpdateExamAsync(int id, ExamUpdate examUpdate);
    Task<Exam?> GetExamByIdAsync(int id);
}
