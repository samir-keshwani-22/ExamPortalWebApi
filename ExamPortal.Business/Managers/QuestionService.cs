using AutoMapper;
using ExamPortal.API.Models;
using ExamPortal.API.Models.Common;
using ExamPortal.Business.Interfaces;

namespace ExamPortal.Business.Managers;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IMapper _mapper;

    public QuestionService(IQuestionRepository questionRepository, IMapper mapper)
    {
        _mapper = mapper;
        _questionRepository = questionRepository;
    }

    public async Task<bool> CreateQuestionAsync(QuestionCreate questionCreate)
    {
        var question = _mapper.Map<API.Models.Entities.Question>(questionCreate);
        return await _questionRepository.CreateAsync(question);
    }

    public async Task<bool> DeleteQuestionAsync(int id)
    {
        return await _questionRepository.DeleteQuestionAsync(id);
    }

    public async Task<Question?> GetQuestionByIdAsync(int id)
    {
        var questionModel = await _questionRepository.GetQuestionByIdAsync(id);
        return questionModel == null ? null : _mapper.Map<API.Models.Question>(questionModel);
    }

    public async Task<PagedResponse<Question>> ListQuestionsAsync(long pageIndex, long pageSize)
    {
        var result = await _questionRepository.GetPagedQuestionsAsync(pageIndex, pageSize);
        return new PagedResponse<Question>
        {
            Paging = new API.Models.Common.Paging
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRecords = result.TotalCount,
                TotalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize)
            },
            Items = _mapper.Map<List<API.Models.Question>>(result.Items)
        };
    }

    public async Task<bool> UpdateQuestionAsync(int id, QuestionUpdate questionUpdate)
    {
        var existingQuestion = await _questionRepository.GetQuestionByIdAsync(id);
        if (existingQuestion == null)
            return false;
        // from , to 
        _mapper.Map(questionUpdate, existingQuestion);
        return await _questionRepository.UpdateQuestionAsync(existingQuestion);
    }
}
