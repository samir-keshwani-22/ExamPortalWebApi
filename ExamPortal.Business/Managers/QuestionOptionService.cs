using AutoMapper;
using ExamPortal.API.Models;
using ExamPortal.API.Models.Common;
using ExamPortal.Business.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ExamPortal.Business.Managers;

public class QuestionOptionService : IQuestionOptionService
{
    private readonly IQuestionOptionRepository _questionOptionRepository;
    private readonly IMapper _mapper;

    public QuestionOptionService(IQuestionOptionRepository questionOptionRepository, IMapper mapper)
    {
        _questionOptionRepository = questionOptionRepository;
        _mapper = mapper;
    }
    public async Task<bool> CreateQuestionOptionAsync(QuestionOptionCreate questionOptionCreate)
    {
        API.Models.Entities.QuestionOption questionOption = _mapper.Map<API.Models.Entities.QuestionOption>(questionOptionCreate);
        return await _questionOptionRepository.CreateAsync(questionOption);
    }

    public async Task<bool> DeleteQuestionOptionAsync(int id)
    {
        return await _questionOptionRepository.DeleteAsync(id);
    }

    public async Task<QuestionOption?> GetQuestionOptionByIdAsync(int id)
    {
        API.Models.Entities.QuestionOption? questionOptionModel = await _questionOptionRepository.GetByIdAsync(id);
        return questionOptionModel == null ? null : _mapper.Map<API.Models.QuestionOption>(questionOptionModel);
    }

    public async Task<PagedResponse<QuestionOption>> ListQuestionOptionsAsync(long pageIndex, long pageSize)
    {
        PagedResult<API.Models.Entities.QuestionOption> result = await _questionOptionRepository.GetPagedAsync(pageIndex, pageSize);
        return new PagedResponse<QuestionOption>
        {
            Paging = new API.Models.Common.Paging
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRecords = result.TotalCount,
                TotalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize)
            },
            Items = _mapper.Map<List<API.Models.QuestionOption>>(result.Items)
        };
    }

    public async Task<bool> UpdateQuestionOptionAsync(int id, QuestionOptionUpdate questionOptionUpdate)
    {
        API.Models.Entities.QuestionOption? existingQuestionOption = await _questionOptionRepository.GetByIdAsync(id);
        if (existingQuestionOption == null)
        {
            return false;
        }
        _mapper.Map(questionOptionUpdate, existingQuestionOption);
        return await _questionOptionRepository.UpdateAsync(existingQuestionOption);
    }
}
