using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ExamPortal.API.Controllers;
using ExamPortal.API.Models;
using ExamPortal.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExamPortal.API.Implementations;

/// <summary>
/// Project API Im
/// </summary>
[ApiController]
public class ExamController : ExamApiController
{
    #region Fields 

    private readonly IExamService _examService;
    private readonly ILogger<ExamController> _logger;

    #endregion

    #region Constructors 

    public ExamController(IExamService examService, ILogger<ExamController> logger)
    {
        _examService = examService;
        _logger = logger;
    }

    #endregion

    #region  Add Exam 

    /// <summary>
    /// Adds a new exam.
    /// </summary>
    /// <param name="examCreate">The exam data to create.</param>
    /// <returns>
    /// 201 if the exam is created successfully, 400 if the request is invalid or creation fails, 500 for server errors.
    /// </returns>
    public override async Task<IActionResult> AddExam([FromBody] ExamCreate examCreate)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _examService.CreateExamAsync(examCreate);
            return result ? StatusCode(201) : BadRequest(new Error
            {
                Code = "BadRequest",
                Message = "Failed to create exam."
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating exam");
            return StatusCode(500, new Error
            {
                Code = "InternalServerError",
                Message = "Internal server error"
            });
        }
    }

    #endregion

    #region Delete Exam

    public override async Task<IActionResult> DeleteExam([FromRoute(Name = "id"), Required] int id)
    {
        try
        {
            var result = await _examService.DeleteExamAsync(id);
            if (result)
                return NoContent();
            else
            {
                return NotFound(new Error
                {
                    Code = "NotFound",
                    Message = "Exam Not Found"
                });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Error
            {
                Code = "InternalServerError",
                Message = ex.Message
            });
        }
    }

    #endregion

    public override async Task<IActionResult> GetExamById([FromRoute(Name = "id"), Required] int id)
    {

        try
        {
            var exam = await _examService.GetExamByIdAsync(id);
            if (exam == null)
            {
                return NotFound(new Error
                {
                    Code = "NotFound",
                    Message = "Exam not found"
                });
            }
            return Ok(exam);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Error
            {
                Code = "InternalServerError",
                Message = ex.Message
            });
        }
    }


    public override Task<IActionResult> ListExams([FromQuery(Name = "pageIndex")] long? pageIndex, [FromQuery(Name = "pageSize")] long? pageSize, [FromQuery(Name = "title")] string title, [FromQuery(Name = "startDateFrom")] DateOnly? startDateFrom, [FromQuery(Name = "startDateTo")] DateOnly? startDateTo, [FromQuery(Name = "endDateFrom")] DateOnly? endDateFrom, [FromQuery(Name = "endDateTo")] DateOnly? endDateTo, [FromQuery(Name = "createdBy")] long? createdBy)
    {
        throw new NotImplementedException();
    }

    public override async Task<IActionResult> UpdateExam([FromRoute(Name = "id"), Required] int id, [FromBody] ExamUpdate examUpdate)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _examService.UpdateExamAsync(id, examUpdate);
            return result ? Ok() : NotFound(new Error
            {
                Code = "NotFound",
                Message = "Exam not found or update failed"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating exam");
            return StatusCode(500, new Error
            {
                Code = "InternalServerError",
                Message = "Internal server error"
            });
        }
    }

}
