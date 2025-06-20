using ExamPortal.API.Models;
using ExamPortal.API.Models.Common;

namespace ExamPortal.Business.Interfaces
{
    public interface IAnswerService
    {
        Task<bool> CreateAnswerAsync(AnswerCreate answerCreate);
        Task<bool> DeleteAnswerAsync(int id);
        Task<Answer?> GetAnswerByIdAsync(int id);

        Task<PagedResponse<Answer>> ListAnswersAsync(long pageIndex, long pageSize);

        Task<bool> UpdateAnswerAsync(int id, AnswerUpdate answerUpdate);
    }
}