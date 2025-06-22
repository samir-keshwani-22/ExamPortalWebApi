using AutoMapper;
using ExamPortal.API.Models;
using ExamPortal.API.Models.Common;
using ExamPortal.Business.Interfaces;

namespace ExamPortal.Business.Managers;

public class ExamService : IExamService
{
    private readonly IExamRepository _examRepository;
    private readonly IMapper _mapper;
    public ExamService(IExamRepository examRepository, IMapper mapper)
    {
        _examRepository = examRepository;
        _mapper = mapper;
    }

    public async Task<bool> CreateExamAsync(ExamCreate examCreate)
    {
        API.Models.Entities.Exam exam = _mapper.Map<API.Models.Entities.Exam>(examCreate);
        exam.DurationMinutes = TimeSpan.FromMinutes(examCreate.DurationMinutes);
        return await _examRepository.CreateAsync(exam);
    }

    public async Task<bool> DeleteExamAsync(int id)
    {
        return await _examRepository.DeleteExamAsync(id);
    }

    public async Task<API.Models.Exam?> GetExamByIdAsync(int id)
    {
        API.Models.Entities.Exam? examModel = await _examRepository.GetExamByIdAsync(id);
        return examModel == null ? null : _mapper.Map<API.Models.Exam>(examModel);
    }

    public async Task<PagedResponse<API.Models.Exam>> ListExamsAsync(long pageIndex, long pageSize, string? title, DateOnly? startDateFrom, DateOnly? startDateTo, DateOnly? endDateFrom, DateOnly? endDateTo, long? createdBy)
    {
        PagedResult<API.Models.Entities.Exam> result = await _examRepository.GetPagedExamsAsync(
          pageIndex, pageSize, title,
          startDateFrom, startDateTo,
          endDateFrom, endDateTo, createdBy);

        return new PagedResponse<API.Models.Exam>
        {
            Paging = new API.Models.Common.Paging
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRecords = result.TotalCount,
                TotalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize)
            },
            Items = _mapper.Map<List<API.Models.Exam>>(result.Items)
        };
    }

    public async Task<bool> UpdateExamAsync(int id, ExamUpdate examUpdate)
    {
        API.Models.Entities.Exam? existingExam = await _examRepository.GetExamByIdAsync(id);
        if (existingExam == null)
            return false;
        // from , to 
        _mapper.Map(examUpdate, existingExam);
        return await _examRepository.UpdateExamAsync(existingExam);
    }
}
