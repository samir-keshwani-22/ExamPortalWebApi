using ExamPortal.API.Models.Common;
using ExamPortal.API.Models.Entities;

namespace ExamPortal.Business.Interfaces;

public interface IAnswerRepository
{
    Task<bool> CreateAsync(Answer answer);
    Task<bool> DeleteAnswerAsync(int id);

    Task<Answer?> GetAnswerByIdAsnc(int id);

    Task<PagedResult<Answer>> GetPagedAnswersAsync(long pageIndex, long pageSize);

    Task<bool> UpdateAnswerAsync(Answer answer);
}
