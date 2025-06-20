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
/// API Controller for Question operations.
/// </summary>
[ApiController]
public class QuestionController : QuestionApiController
{
    #region Fields 
    private readonly IQuestionService _questionService;
    private readonly ILogger<QuestionController> _logger;

    #endregion

    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="QuestionController"/> class.
    /// </summary>
    /// <param name="questionService">Service for question operations.</param>
    /// <param name="logger">Logger instance.</param>
    public QuestionController(IQuestionService questionService, ILogger<QuestionController> logger)
    {
        _questionService = questionService;
        _logger = logger;
    }

    #endregion

    #region Methods 

    #region Add Question

    /// <summary>
    /// Adds a new question.
    /// </summary>
    /// <param name="questionCreate">Question creation model.</param>
    /// <returns>HTTP 201 if created, 400 if bad request, 500 if server error.</returns>

    public override async Task<IActionResult> AddQuestion([FromBody] QuestionCreate questionCreate)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _questionService.CreateQuestionAsync(questionCreate);
            return result ? StatusCode(201) : BadRequest(
                new Error
                {
                    Code = "BadRequest",
                    Message = "Failed to create question."
                }
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating question");
            return StatusCode(500, new Error
            {
                Code = "InternalServerError",
                Message = "Internal server error"
            });
        }
    }

    #endregion

    #region Delete Question 

    /// <summary>
    /// Deletes a question by ID.
    /// </summary>
    /// <param name="id">Question ID.</param>
    /// <returns>204 if deleted, 404 if not found.</returns>
    public override async Task<IActionResult> DeleteQuestion([FromRoute(Name = "id"), Required] int id)
    {
        try
        {
            var result = await _questionService.DeleteQuestionAsync(id);
            if (result)
                return NoContent();
            else
            {
                return NotFound(new Error
                {
                    Code = "NotFound",
                    Message = "Question not found"
                });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting question");
            return StatusCode(500, new Error
            {
                Code = "InternalServerError",
                Message = ex.Message
            });
        }
    }

    #endregion


    #region Get Question By ID

    /// <summary>
    /// Retrieves a question by ID.
    /// </summary>
    /// <param name="id">Question ID.</param>
    /// <returns>200 with question data or 404 if not found.</returns>

    public override async Task<IActionResult> GetQuestionById([FromRoute(Name = "id"), Required] int id)
    {
        try
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            if (question == null)
            {
                return NotFound(new Error
                {
                    Code = "NotFound",
                    Message = "Question not found"
                });
            }
            return Ok(question);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving question");
            return StatusCode(500, new Error
            {
                Code = "InternalServerError",
                Message = ex.Message
            });
        }
    }

    #endregion

    #region List Questions

    /// <summary>
    /// Returns paginated list of questions.
    /// </summary>
    /// <param name="pageIndex">Current page index.</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <returns>200 with paginated questions.</returns>
    public override async Task<IActionResult> ListQuestions([FromQuery(Name = "pageIndex")] long? pageIndex, [FromQuery(Name = "pageSize")] long? pageSize)
    {
        try
        {
            var result = await _questionService.ListQuestionsAsync(
                pageIndex ?? 1,
                pageSize ?? 10);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing questions");
            return StatusCode(500, new Error
            {
                Code = "InternalServerError",
                Message = "Internal server error"
            });
        }
    }

    #endregion


    #region Update Question

    /// <summary>
    /// Updates an existing question.
    /// </summary>
    /// <param name="id">Question ID.</param>
    /// <param name="questionUpdate">Update model.</param>
    /// <returns>200 if updated, 404 if not found.</returns>

    public override async Task<IActionResult> UpdateQuestion([FromRoute(Name = "id"), Required] int id, [FromBody] QuestionUpdate questionUpdate)
    {
        if (questionUpdate.ExamId == 0)
        {
            ModelState.AddModelError("ExamId", "ExamId is required for update.");
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _questionService.UpdateQuestionAsync(id, questionUpdate);
            return result ? Ok() : NotFound(new Error
            {
                Code = "NotFound",
                Message = "Question not found or update failed"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating question");
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
