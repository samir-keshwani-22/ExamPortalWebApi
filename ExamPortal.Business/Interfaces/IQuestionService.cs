using ExamPortal.API.Models;
using ExamPortal.API.Models.Common;

namespace ExamPortal.Business.Interfaces;

public interface IQuestionService
{
    Task<bool> CreateQuestionAsync(QuestionCreate questionCreate);
    Task<bool> DeleteQuestionAsync(int id);

    Task<Question?> GetQuestionByIdAsync(int id);
    Task<PagedResponse<Question>> ListQuestionsAsync(long pageIndex, long pageSize);
    Task<bool> UpdateQuestionAsync(int id, QuestionUpdate questionUpdate);

}
