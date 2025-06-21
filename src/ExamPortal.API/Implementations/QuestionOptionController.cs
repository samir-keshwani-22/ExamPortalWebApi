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
/// API Controller for Question Option operations
/// </summary>
[ApiController]
public class QuestionOptionController : QuestionOptionApiController
{
    #region Fields

    private readonly IQuestionOptionService _questionOptionService;
    private readonly ILogger<QuestionOptionController> _logger;

    #endregion

    #region Constructor

    /// <summary>
    /// QuestionOptionController constructor
    /// </summary>

    public QuestionOptionController(IQuestionOptionService questionOptionService, ILogger<QuestionOptionController> logger)
    {
        _questionOptionService = questionOptionService;
        _logger = logger;
    }

    #endregion

    #region Methods 

    #region Add Question Option

    /// <summary>
    /// Adds a new question option.
    /// </summary>
    public override async Task<IActionResult> AddQuestionOption([FromBody] QuestionOptionCreate questionOptionCreate)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var created = await _questionOptionService.CreateQuestionOptionAsync(questionOptionCreate);
            return created ? StatusCode(201) : BadRequest(new Error
            {
                Code = "BadRequest",
                Message = "Failed to create question option."
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating question option");
            return StatusCode(500, new Error
            {
                Code = "InternalServerError",
                Message = "Internal server error"
            });
        }
    }

    #endregion

    #region Delete Question Option

    /// <summary>
    /// Deletes a question option by ID.
    /// </summary>
    public override async Task<IActionResult> DeleteQuestionOption([FromRoute(Name = "id"), Required] int id)
    {
        try
        {
            var deleted = await _questionOptionService.DeleteQuestionOptionAsync(id);
            return deleted ? NoContent() : NotFound(new Error
            {
                Code = "NotFound",
                Message = "Question option not found"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting question option");
            return StatusCode(500, new Error
            {
                Code = "InternalServerError",
                Message = ex.Message
            });
        }
    }

    #endregion

    #region Get Question Option By ID

    /// <summary>
    /// Retrieves a question option by ID.
    /// </summary>

    public override async Task<IActionResult> GetQuestionOptionById([FromRoute(Name = "id"), Required] int id)
    {
        try
        {
            var option = await _questionOptionService.GetQuestionOptionByIdAsync(id);
            return option != null ? Ok(option) : NotFound(new Error
            {
                Code = "NotFound",
                Message = "Question option not found"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching question option");
            return StatusCode(500, new Error
            {
                Code = "InternalServerError",
                Message = ex.Message
            });
        }
    }

    #endregion

    #region List Question Options

    /// <summary>
    /// Returns paginated list of question options.
    /// </summary>

    public override async Task<IActionResult> ListQuestionOptions([FromQuery(Name = "pageIndex")] long? pageIndex, [FromQuery(Name = "pageSize")] long? pageSize)
    {
        try
        {
            var result = await _questionOptionService.ListQuestionOptionsAsync(pageIndex ?? 1, pageSize ?? 10);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing question options");
            return StatusCode(500, new Error
            {
                Code = "InternalServerError",
                Message = "Internal server error"
            });
        }
    }
    #endregion

    #region Update Question Option

    /// <summary>
    /// Updates an existing question option.
    /// </summary>

    public override async Task<IActionResult> UpdateQuestionOption([FromRoute(Name = "id"), Required] int id, [FromBody] QuestionOptionUpdate questionOptionUpdate)
    {
        if (questionOptionUpdate.QuestionId == 0)
        {
            ModelState.AddModelError("QuestionId", "QuestionId is required for update.");
            return BadRequest(ModelState);
        }
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var updated = await _questionOptionService.UpdateQuestionOptionAsync(id, questionOptionUpdate);
            return updated ? Ok() : NotFound(new Error
            {
                Code = "NotFound",
                Message = "Question option not found or update failed"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating question option");
            return StatusCode(500, new Error
            {
                Code = "InternalServerError",
                Message = "Internal server error"
            });
        }
    }

    #endregion
    #endregion

}
