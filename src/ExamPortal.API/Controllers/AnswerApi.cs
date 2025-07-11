/*
 * Exam Portal API
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0.0
 * 
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using ExamPortal.API.Attributes;
using ExamPortal.API.Models;

namespace ExamPortal.API.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public abstract class AnswerApiController : ControllerBase
    { 
        /// <summary>
        /// Add a new answer
        /// </summary>
        /// <param name="answerCreate">Answer data to create</param>
        /// <response code="201">Answer created successfully</response>
        /// <response code="400">Invalid input</response>
        /// <response code="422">Validation exception</response>
        /// <response code="0">Unexpected error</response>
        [HttpPost]
        [Route("/api/answers")]
        [Consumes("application/json")]
        [ValidateModelState]
        [SwaggerOperation("AddAnswer")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "Invalid input")]
        [SwaggerResponse(statusCode: 422, type: typeof(Error), description: "Validation exception")]
        [SwaggerResponse(statusCode: 0, type: typeof(Error), description: "Unexpected error")]
        public abstract Task<IActionResult> AddAnswer([FromBody]AnswerCreate answerCreate);

        /// <summary>
        /// Delete an answer
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Answer deleted</response>
        /// <response code="404">Answer not found</response>
        /// <response code="0">Unexpected error</response>
        [HttpDelete]
        [Route("/api/answers/{id}")]
        [ValidateModelState]
        [SwaggerOperation("DeleteAnswer")]
        [SwaggerResponse(statusCode: 404, type: typeof(Error), description: "Answer not found")]
        [SwaggerResponse(statusCode: 0, type: typeof(Error), description: "Unexpected error")]
        public abstract Task<IActionResult> DeleteAnswer([FromRoute (Name = "id")][Required]int id);

        /// <summary>
        /// Get an answer by ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Answer details</response>
        /// <response code="404">Answer not found</response>
        /// <response code="0">Unexpected error</response>
        [HttpGet]
        [Route("/api/answers/{id}")]
        [ValidateModelState]
        [SwaggerOperation("GetAnswerById")]
        [SwaggerResponse(statusCode: 200, type: typeof(Answer), description: "Answer details")]
        [SwaggerResponse(statusCode: 404, type: typeof(Error), description: "Answer not found")]
        [SwaggerResponse(statusCode: 0, type: typeof(Error), description: "Unexpected error")]
        public abstract Task<IActionResult> GetAnswerById([FromRoute (Name = "id")][Required]int id);

        /// <summary>
        /// List all answers
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <response code="200">OK</response>
        /// <response code="0">Unexpected error</response>
        [HttpGet]
        [Route("/api/answers")]
        [ValidateModelState]
        [SwaggerOperation("ListAnswers")]
        [SwaggerResponse(statusCode: 200, type: typeof(ListAnswers200Response), description: "OK")]
        [SwaggerResponse(statusCode: 0, type: typeof(Error), description: "Unexpected error")]
        public abstract Task<IActionResult> ListAnswers([FromQuery (Name = "pageIndex")]long? pageIndex, [FromQuery (Name = "pageSize")]long? pageSize);

        /// <summary>
        /// Update an existing answer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="answerUpdate">Answer data to update</param>
        /// <response code="200">Answer updated successfully</response>
        /// <response code="400">Invalid ID supplied</response>
        /// <response code="404">Answer not found</response>
        /// <response code="422">Validation exception</response>
        /// <response code="0">Unexpected error</response>
        [HttpPut]
        [Route("/api/answers/{id}")]
        [Consumes("application/json")]
        [ValidateModelState]
        [SwaggerOperation("UpdateAnswer")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "Invalid ID supplied")]
        [SwaggerResponse(statusCode: 404, type: typeof(Error), description: "Answer not found")]
        [SwaggerResponse(statusCode: 422, type: typeof(Error), description: "Validation exception")]
        [SwaggerResponse(statusCode: 0, type: typeof(Error), description: "Unexpected error")]
        public abstract Task<IActionResult> UpdateAnswer([FromRoute (Name = "id")][Required]int id, [FromBody]AnswerUpdate answerUpdate);
    }
}
