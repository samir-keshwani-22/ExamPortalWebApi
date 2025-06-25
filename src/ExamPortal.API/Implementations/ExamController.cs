#nullable enable
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ExamPortal.API.Controllers;
using ExamPortal.API.Models;
using ExamPortal.API.Models.Common;
using ExamPortal.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExamPortal.API.Implementations;

/// <summary>
/// API Controller for Exam operations
/// </summary>
[ApiController]
public class ExamController : ExamApiController
{
    #region Fields 

    private readonly IExamService _examService;
    private readonly ILogger<ExamController> _logger;
    private readonly ISalesDataService _salesDataService;

    #endregion

    #region Constructor

    /// <summary>
    /// ExamController constructor
    /// </summary>
    /// <param name="examService"></param>
    /// <param name="logger"></param>
    /// <param name="salesDataService"></param>
    public ExamController(IExamService examService, ILogger<ExamController> logger, ISalesDataService salesDataService)
    {
        _salesDataService = salesDataService;
        _examService = examService;
        _logger = logger;
    }

    #endregion

    #region  Methods 

    #region  Add Exam 

    /// <summary>
    /// Adds a new exam.
    /// </summary>
    /// <param name="examCreate">Exam creation model</param>
    /// <returns>HTTP 201 if created, 400 if bad request, 500 if server error</returns>
    public override async Task<IActionResult> AddExam([FromBody] ExamCreate examCreate)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            bool result = await _examService.CreateExamAsync(examCreate);
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

    /// <summary>
    /// Deletes an exam by ID.
    /// </summary>
    /// <param name="id">Exam ID</param>
    /// <returns>204 if deleted, 404 if not found</returns>

    public override async Task<IActionResult> DeleteExam([FromRoute(Name = "id"), Required] int id)
    {
        try
        {
            bool result = await _examService.DeleteExamAsync(id);
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

    #region Get Exam By ID

    /// <summary>
    /// Retrieves an exam by ID.
    /// </summary>
    /// <param name="id">Exam ID</param>
    /// <returns>200 with exam data or 404 if not found</returns>

    public override async Task<IActionResult> GetExamById([FromRoute(Name = "id"), Required] int id)
    {

        try
        {
            API.Models.Exam? exam = await _examService.GetExamByIdAsync(id);
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

    #endregion

    #region List Exams
    /// <summary>
    /// Returns paginated and filtered list of exams.
    /// </summary>
    public override async Task<IActionResult> ListExams([FromQuery(Name = "pageIndex")] long? pageIndex, [FromQuery(Name = "pageSize")] long? pageSize, [FromQuery(Name = "title")] string? title, [FromQuery(Name = "startDateFrom")] DateOnly? startDateFrom, [FromQuery(Name = "startDateTo")] DateOnly? startDateTo, [FromQuery(Name = "endDateFrom")] DateOnly? endDateFrom, [FromQuery(Name = "endDateTo")] DateOnly? endDateTo, [FromQuery(Name = "createdBy")] long? createdBy)
    {
        try
        {
            PagedResponse<API.Models.Exam> result = await _examService.ListExamsAsync(
                pageIndex ?? 1,
                pageSize ?? 10,
                title,
                startDateFrom,
                startDateTo,
                endDateFrom,
                endDateTo,
                createdBy);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing exams");
            return StatusCode(500, new Error
            {
                Code = "InternalServerError",
                Message = "Internal server error"
            });
        }
    }
    #endregion


    #region Update Exam

    /// <summary>
    /// Updates an existing exam.
    /// </summary>
    /// <param name="id">Exam ID</param>
    /// <param name="examUpdate">Update model</param>
    /// <returns>200 if updated, 404 if not found</returns>
    public override async Task<IActionResult> UpdateExam([FromRoute(Name = "id"), Required] int id, [FromBody] ExamUpdate examUpdate)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            bool result = await _examService.UpdateExamAsync(id, examUpdate);
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

    #endregion

    #region Batch Upload

    /// <summary>
    /// Batch upload feature 
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    [RequestSizeLimit(524288000)]
    [HttpPost]
    [Route("/api/batch-upload")]
    [Consumes("multipart/form-data")]
    public override async Task<IActionResult> BatchUpload([Required] IFormFile file)
    {
        var cancellationToken = HttpContext.RequestAborted;

        if (file == null || file.Length == 0)
            return BadRequest(new Error { Code = "InvalidFile", Message = "File is empty or missing" });
        try
        {
            var (successCount, errorCount) = await _salesDataService.ProcessCsvUploadAsync(file, cancellationToken);
            return Ok(new
            {
                successCount,
                errorCount
            });
        }
        catch (OperationCanceledException)
        {
            return StatusCode(499, new Error
            {
                Code = "ClientClosedRequest",
                Message = "The request was canceled by the client."
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing batch upload");
            return StatusCode(500, new Error
            {
                Code = "InternalServerError",
                Message = "Error processing CSV upload"
            });
        }
    }
    #endregion

    #endregion
}
