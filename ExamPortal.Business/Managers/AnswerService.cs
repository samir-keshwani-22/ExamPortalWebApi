using AutoMapper;
using ExamPortal.API.Models;
using ExamPortal.API.Models.Common;
using ExamPortal.Business.Interfaces;

namespace ExamPortal.Business.Managers;

public class AnswerService : IAnswerService
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IMapper _mapper;

    public AnswerService(IAnswerRepository answerRepository, IMapper mapper)
    {
        _answerRepository = answerRepository;
        _mapper = mapper;
    }
    public async Task<bool> CreateAnswerAsync(AnswerCreate answerCreate)
    {
        API.Models.Entities.Answer answer = _mapper.Map<API.Models.Entities.Answer>(answerCreate);
        return await _answerRepository.CreateAsync(answer);
    }

    public async Task<bool> DeleteAnswerAsync(int id)
    {
        return await _answerRepository.DeleteAnswerAsync(id);
    }

    public async Task<Answer?> GetAnswerByIdAsync(int id)
    {
        API.Models.Entities.Answer? answerModel = await _answerRepository.GetAnswerByIdAsnc(id);
        return answerModel == null ? null : _mapper.Map<API.Models.Answer>(answerModel);
    }

    public async Task<PagedResponse<Answer>> ListAnswersAsync(long pageIndex, long pageSize)
    {
        PagedResult<API.Models.Entities.Answer> result = await _answerRepository.GetPagedAnswersAsync(pageIndex, pageSize);
        return new PagedResponse<Answer>
        {
            Paging = new API.Models.Common.Paging
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRecords = result.TotalCount,
                TotalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize)
            },
            Items = _mapper.Map<List<API.Models.Answer>>(result.Items)
        };
    }

    public async Task<bool> UpdateAnswerAsync(int id, AnswerUpdate answerUpdate)
    {
        API.Models.Entities.Answer? existingAnswer = await _answerRepository.GetAnswerByIdAsnc(id);
        if (existingAnswer == null)
        {
            return false;
        }
        _mapper.Map(answerUpdate, existingAnswer);
        return await _answerRepository.UpdateAnswerAsync(existingAnswer);
    }

}
