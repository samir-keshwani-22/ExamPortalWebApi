using ExamPortal.API.Models.Common;
using ExamPortal.API.Models.Entities;

namespace ExamPortal.Business.Interfaces;

public interface IQuestionOptionRepository
{
    Task<bool> CreateAsync(QuestionOption questionOption);

    Task<bool> DeleteAsync(int id);
    Task<QuestionOption?> GetByIdAsync(int id);
    Task<PagedResult<QuestionOption>> GetPagedAsync(long pageIndex, long pageSize);
    Task<bool> UpdateAsync(QuestionOption questionOption);
}
