using AutoMapper;
using ExamPortal.API.Models;
using ExamPortal.API.Models.Entities;
using ExamPortal.Business.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        var exam = _mapper.Map<API.Models.Entities.Exam>(examCreate);
        exam.DurationMinutes = TimeSpan.FromMinutes(examCreate.DurationMinutes);
        return await _examRepository.CreateAsync(exam);
    }

    public async Task<bool> DeleteExamAsync(int id)
    {
        return await _examRepository.DeleteExamAsync(id);
    }

    public async Task<API.Models.Exam?> GetExamByIdAsync(int id)
    {
        var examModel = await _examRepository.GetExamByIdAsync(id);
        return examModel == null ? null : _mapper.Map<API.Models.Exam>(examModel);
    }

    public async Task<bool> UpdateExamAsync(int id, ExamUpdate examUpdate)
    {
        var existingExam = await _examRepository.GetExamByIdAsync(id);
        if (existingExam == null)
            return false;
        // from , to 
        _mapper.Map(examUpdate, existingExam);
        return await _examRepository.UpdateExamAsync(existingExam);
    }
}
