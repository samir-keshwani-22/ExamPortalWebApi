#nullable enable
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ExamPortal.API.Controllers;
using ExamPortal.API.Models;
using ExamPortal.API.Models.Common;
using ExamPortal.Business.Interfaces;
using ExamPortal.Common.Exceptions;
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

    private readonly IRuleValidationService _ruleValidationService;

    #endregion

    #region Constructor

    /// <summary>
    /// ExamController constructor
    /// </summary>
    /// <param name="examService"></param>
    /// <param name="logger"></param>
    /// <param name="salesDataService"></param>
    /// <param name="ruleValidationService"></param>
    public ExamController(IExamService examService, ILogger<ExamController> logger, ISalesDataService salesDataService, IRuleValidationService ruleValidationService)
    {
        _salesDataService = salesDataService;
        _examService = examService;
        _logger = logger;
        _ruleValidationService = ruleValidationService;
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
            _logger.LogWarning("AddExam - ModelState is invalid . Payload {@Payload}", examCreate);
            return BadRequest(ModelState);
        }
        try
        {
            _logger.LogInformation("AddExam - Creating exam with title: {Title}", examCreate.Title);
            bool result = await _examService.CreateExamAsync(examCreate);
            if (result)
            {
                _logger.LogInformation("AddExam - Successfully created exam: {Title}", examCreate.Title);
                return StatusCode(201);
            }
            else
            {
                _logger.LogWarning("AddExam - Failed to create exam: {Title}", examCreate.Title);
                return BadRequest(new Error
                {
                    Code = "BadRequest",
                    Message = "Failed to create exam."
                });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "AddExam - Error creating exam: {Title}", examCreate.Title);
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
            _logger.LogInformation("DeleteExam - Deleting the exam with id: {id}", id);
            bool result = await _examService.DeleteExamAsync(id);
            if (result)
            {
                _logger.LogInformation("DeleteExam - Deleted the exam with id :{id}", id);
                return NoContent();
            }
            else
            {
                _logger.LogWarning("DeleteExam - Exam with the required id not found");
                return NotFound(new Error
                {
                    Code = "NotFound",
                    Message = "Exam Not Found"
                });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DeleteExam - Error deleting exam");
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
            _logger.LogInformation("GetExamById - Started getting the exam with the id:{id}", id);
            API.Models.Exam? exam = await _examService.GetExamByIdAsync(id);
            if (exam == null)
            {
                _logger.LogWarning("GetExamById - Exam with the given id :{id} not found ", id);
                return NotFound(new Error
                {
                    Code = "NotFound",
                    Message = "Exam not found"
                });
            }
            _logger.LogInformation("GetExamById - Exam with the given id found");
            return Ok(exam);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetExamById - Error getting the exam with id : {id}", id);
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
            _logger.LogInformation("ListExams - Getting the list of exams ");
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
            _logger.LogWarning("UpdateExam - ModalState is invalid . Payload {@Payload}", examUpdate);
            return BadRequest(ModelState);
        }
        try
        {
            _logger.LogInformation("UpdateExam - Updating exam with the id:{id}", id);
            bool result = await _examService.UpdateExamAsync(id, examUpdate);
            if (result)
            {
                _logger.LogInformation("UpdateExam - Successfully updated exam with id :{id}", id);
                return Ok();
            }
            else
            {
                _logger.LogWarning("UpdateExam - Failed to update exam with  id {id}", id);
                return NotFound(new Error
                {
                    Code = "NotFound",
                    Message = "Exam not found or update failed"
                });
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating exam with id :{id}", id);
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


    #pragma warning disable CS1998
    /// <summary>
    /// Check for a rules value 
    /// </summary>
    /// <param name="ruleEvaluatorRequest">Rule Checker</param> 
    [HttpPost]
    [Route("/api/exams/rule-check")]
    [Consumes("application/json")]
    public override async Task<IActionResult> CheckRule([FromBody] RuleEvaluatorRequest ruleEvaluatorRequest)
    {
        try
        {
            _ruleValidationService.CheckThreshold(ruleEvaluatorRequest);
            _ruleValidationService.ValidateRules(ruleEvaluatorRequest);
            return Ok(new { success = true, message = "Valid input" });
        }
        catch (RuleValidationException ex)
        {
            return BadRequest(new Error { Code = "InvalidInput", Message = ex.Message });
        }
    }
    #pragma warning restore CS1998

    #endregion
}
