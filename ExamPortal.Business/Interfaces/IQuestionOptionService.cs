using ExamPortal.API.Models;
using ExamPortal.API.Models.Common;

namespace ExamPortal.Business.Interfaces;

public interface IQuestionOptionService
{
    Task<bool> CreateQuestionOptionAsync(QuestionOptionCreate questionOptionCreate);

    Task<bool> DeleteQuestionOptionAsync(int id);
    Task<QuestionOption?> GetQuestionOptionByIdAsync(int id);
    Task<PagedResponse<QuestionOption>> ListQuestionOptionsAsync(long pageIndex, long pageSize);
    Task<bool> UpdateQuestionOptionAsync(int id, QuestionOptionUpdate questionOptionUpdate);

}
