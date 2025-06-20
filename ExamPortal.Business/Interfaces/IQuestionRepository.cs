using ExamPortal.API.Models.Common;
using ExamPortal.API.Models.Entities;

namespace ExamPortal.Business.Interfaces;

public interface IQuestionRepository
{
    Task<bool> CreateAsync(Question question);
    Task<bool> DeleteQuestionAsync(int id);
    Task<Question?> GetQuestionByIdAsync(int id);

    Task<PagedResult<Question>> GetPagedQuestionsAsync(long pageIndex, long pageSize);
    Task<bool> UpdateQuestionAsync(Question question);

}
