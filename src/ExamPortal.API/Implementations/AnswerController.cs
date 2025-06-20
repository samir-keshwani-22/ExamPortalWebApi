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
/// API Controller for Answer operations
/// </summary>
[ApiController]
public class AnswerController : AnswerApiController
{
    #region  Fields

    private readonly IAnswerService _answerService;
    private readonly ILogger<AnswerController> _logger;

    #endregion

    #region Constructor

    /// <summary>
    /// AnswerController constructor
    /// </summary>
    /// <param name="answerService"></param>
    /// <param name="logger"></param>
    public AnswerController(IAnswerService answerService, ILogger<AnswerController> logger)
    {
        _answerService = answerService;
        _logger = logger;
    }

    #endregion

    #region Methods

    #region Add Answer

    /// <summary>
    /// Adds a new answer.
    /// </summary>
    /// <param name="answerCreate">Answer creation model</param>
    /// <returns>HTTP 201 if created, 400 if bad request, 500 if server error</returns>
    public override async Task<IActionResult> AddAnswer([FromBody] AnswerCreate answerCreate)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _answerService.CreateAnswerAsync(answerCreate);
            return result ? StatusCode(201) : BadRequest(new Error
            {
                Code = "BadRequest",
                Message = "Failed to create answer."
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating answer");
            return StatusCode(500, new Error
            {
                Code = "InternalServerError",
                Message = "Internal server error"
            });
        }
    }

    #endregion

    #region Delete Answer
    /// <summary>
    /// Deletes an answer by ID.
    /// </summary>
    /// <param name="id">Answer ID</param>
    /// <returns>204 if deleted, 404 if not found</returns>
    public override async Task<IActionResult> DeleteAnswer([FromRoute(Name = "id"), Required] int id)
    {
        try
        {
            var result = await _answerService.DeleteAnswerAsync(id);
            if (result)
                return NoContent();
            else
            {
                return NotFound(new Error
                {
                    Code = "NotFound",
                    Message = "Answer not found"
                });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting answer");
            return StatusCode(500, new Error
            {
                Code = "InternalServerError",
                Message = ex.Message
            });
        }
    }

    #endregion

    #region Get Answer By ID

    /// <summary>
    /// Retrieves an answer by ID.
    /// </summary>
    /// <param name="id">Answer ID</param>
    /// <returns>200 with answer data or 404 if not found</returns>

    public override async Task<IActionResult> GetAnswerById([FromRoute(Name = "id"), Required] int id)
    {
        try
        {
            var answer = await _answerService.GetAnswerByIdAsync(id);
            if (answer == null)
            {
                return NotFound(new Error
                {
                    Code = "NotFound",
                    Message = "Answer not found"
                });
            }
            return Ok(answer);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting answer");
            return StatusCode(500, new Error
            {
                Code = "InternalServerError",
                Message = ex.Message
            });
        }
    }

    #endregion


    #region List Answers

    /// <summary>
    /// Returns paginated list of answers.
    /// </summary>
    public override async Task<IActionResult> ListAnswers([FromQuery(Name = "pageIndex")] long? pageIndex, [FromQuery(Name = "pageSize")] long? pageSize)
    {
        try
        {
            var result = await _answerService.ListAnswersAsync(pageIndex ?? 1, pageSize ?? 10);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing answers");
            return StatusCode(500, new Error
            {
                Code = "InternalServerError",
                Message = "Internal server error"
            });
        }
    }

    #endregion

    #region Update Answer

    /// <summary>
    /// Updates an existing answer.
    /// </summary>
    /// <param name="id">Answer ID</param>
    /// <param name="answerUpdate">Update model</param>
    /// <returns>200 if updated, 404 if not found</returns>

    public override async Task<IActionResult> UpdateAnswer([FromRoute(Name = "id"), Required] int id, [FromBody] AnswerUpdate answerUpdate)
    {
        if (answerUpdate.QuestionId == 0)
        {
            ModelState.AddModelError("QuestionId", "QuestionId is required for update.");
            return BadRequest(ModelState);
        }
        try
        {
            var result = await _answerService.UpdateAnswerAsync(id, answerUpdate);
            return result ? Ok() : NotFound(new Error
            {
                Code = "NotFound",
                Message = "Answer not found or update failed"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating answer");
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
