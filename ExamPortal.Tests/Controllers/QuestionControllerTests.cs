using ExamPortal.API.Implementations;
using ExamPortal.API.Models;
using ExamPortal.API.Models.Common;
using ExamPortal.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ExamPortal.Tests.Controllers;

public class QuestionControllerTests
{
  private readonly Mock<IQuestionService> _mockService;
  private readonly Mock<ILogger<QuestionController>> _mockLogger;
  private readonly QuestionController _controller;
  public QuestionControllerTests()
  {
    _mockService = new Mock<IQuestionService>();
    _mockLogger = new Mock<ILogger<QuestionController>>();
    _controller = new QuestionController(_mockService.Object, _mockLogger.Object);
    _controller.ControllerContext = new ControllerContext();
  }
  [Fact]
  public async Task AddQuestion_Returns201_OnSuccess()
  {
    var create = new QuestionCreate();
    _mockService.Setup(s => s.CreateQuestionAsync(create)).ReturnsAsync(true);

    var result = await _controller.AddQuestion(create);

    var status = Assert.IsType<StatusCodeResult>(result);
    Assert.Equal(201, status.StatusCode);
  }

  [Fact]
  public async Task AddQuestion_Returns400_OnServiceFailure()
  {
    var create = new QuestionCreate();
    _mockService.Setup(s => s.CreateQuestionAsync(create)).ReturnsAsync(false);

    var result = await _controller.AddQuestion(create);

    var badRequest = Assert.IsType<BadRequestObjectResult>(result);
    var error = Assert.IsType<Error>(badRequest.Value);
    Assert.Equal("BadRequest", error.Code);
  }

  [Fact]
  public async Task AddQuestion_Returns400_WhenModelStateInvalid()
  {
    var create = new QuestionCreate();
    _controller.ModelState.AddModelError("Text", "Required");

    var result = await _controller.AddQuestion(create);

    var badRequest = Assert.IsType<BadRequestObjectResult>(result);
    var serializableError = Assert.IsType<SerializableError>(badRequest.Value);
    Assert.True(serializableError.ContainsKey("Text"));
  }

  [Fact]
  public async Task AddQuestion_Returns500_OnException()
  {
    var create = new QuestionCreate();
    _mockService.Setup(s => s.CreateQuestionAsync(create)).ThrowsAsync(new Exception("fail"));

    var result = await _controller.AddQuestion(create);

    var objectResult = Assert.IsType<ObjectResult>(result);
    Assert.Equal(500, objectResult.StatusCode);
    Assert.Equal("InternalServerError", ((Error)objectResult.Value!).Code);
  }

  [Fact]
  public async Task DeleteQuestion_ReturnsNoContent_OnSuccess()
  {
    _mockService.Setup(s => s.DeleteQuestionAsync(1)).ReturnsAsync(true);

    var result = await _controller.DeleteQuestion(1);

    Assert.IsType<NoContentResult>(result);
  }

  [Fact]
  public async Task DeleteQuestion_ReturnsNotFound_WhenMissing()
  {
    _mockService.Setup(s => s.DeleteQuestionAsync(1)).ReturnsAsync(false);

    var result = await _controller.DeleteQuestion(1);

    var notFound = Assert.IsType<NotFoundObjectResult>(result);
    Assert.Equal("NotFound", ((Error)notFound.Value!).Code);
  }

  [Fact]
  public async Task DeleteQuestion_Returns500_OnException()
  {
    _mockService.Setup(s => s.DeleteQuestionAsync(1)).ThrowsAsync(new Exception("fail"));

    var result = await _controller.DeleteQuestion(1);

    var objectResult = Assert.IsType<ObjectResult>(result);
    Assert.Equal(500, objectResult.StatusCode);
    Assert.Equal("fail", ((Error)objectResult.Value!).Message);
  }

  [Fact]
  public async Task GetQuestionById_ReturnsOk_WhenFound()
  {
    var question = new Question { Id = 1 };
    _mockService.Setup(s => s.GetQuestionByIdAsync(1)).ReturnsAsync(question);

    var result = await _controller.GetQuestionById(1);

    var ok = Assert.IsType<OkObjectResult>(result);
    Assert.Equal(1, ((Question)ok.Value!).Id);
  }

  [Fact]
  public async Task GetQuestionById_ReturnsNotFound_WhenMissing()
  {
    _mockService.Setup(s => s.GetQuestionByIdAsync(1)).ReturnsAsync((Question?)null);

    var result = await _controller.GetQuestionById(1);

    var notFound = Assert.IsType<NotFoundObjectResult>(result);
    Assert.Equal("NotFound", ((Error)notFound.Value!).Code);
  }

  [Fact]
  public async Task GetQuestionById_Returns500_OnException()
  {
    _mockService.Setup(s => s.GetQuestionByIdAsync(1)).ThrowsAsync(new Exception("fail"));

    var result = await _controller.GetQuestionById(1);

    var objectResult = Assert.IsType<ObjectResult>(result);
    Assert.Equal(500, objectResult.StatusCode);
    Assert.Equal("InternalServerError", ((Error)objectResult.Value!).Code);
  }

  [Fact]
  public async Task ListQuestions_ReturnsOk_WithPagedResponse()
  {
    var paged = new PagedResponse<Question>
    {
      Paging = new API.Models.Common.Paging { PageIndex = 1, PageSize = 10, TotalRecords = 1, TotalPages = 1 },
      Items = new List<Question> { new Question { Id = 1 } }
    };

    _mockService.Setup(s => s.ListQuestionsAsync(1, 10)).ReturnsAsync(paged);

    var result = await _controller.ListQuestions(1, 10);

    var ok = Assert.IsType<OkObjectResult>(result);
    Assert.Single(((PagedResponse<Question>)ok.Value!).Items);
  }

  [Fact]
  public async Task ListQuestions_Returns500_OnException()
  {
    _mockService.Setup(s => s.ListQuestionsAsync(1, 10)).ThrowsAsync(new Exception("fail"));

    var result = await _controller.ListQuestions(1, 10);

    var objectResult = Assert.IsType<ObjectResult>(result);
    Assert.Equal(500, objectResult.StatusCode);
    Assert.Equal("InternalServerError", ((Error)objectResult.Value!).Code);
  }

  [Fact]
  public async Task UpdateQuestion_ReturnsOk_OnSuccess()
  {
    var update = new QuestionUpdate { ExamId = 2 };
    _mockService.Setup(s => s.UpdateQuestionAsync(1, update)).ReturnsAsync(true);

    var result = await _controller.UpdateQuestion(1, update);

    Assert.IsType<OkResult>(result);
  }

  [Fact]
  public async Task UpdateQuestion_ReturnsNotFound_OnServiceFailure()
  {
    var update = new QuestionUpdate { ExamId = 2 };
    _mockService.Setup(s => s.UpdateQuestionAsync(1, update)).ReturnsAsync(false);

    var result = await _controller.UpdateQuestion(1, update);

    var notFound = Assert.IsType<NotFoundObjectResult>(result);
    Assert.Equal("NotFound", ((Error)notFound.Value!).Code);
  }

  [Fact]
  public async Task UpdateQuestion_ReturnsBadRequest_WhenExamIdMissing()
  {
    var update = new QuestionUpdate { ExamId = 0 };

    var result = await _controller.UpdateQuestion(1, update);

    var badRequest = Assert.IsType<BadRequestObjectResult>(result);
    Assert.True(((SerializableError)badRequest.Value!).ContainsKey("ExamId"));
  }

  [Fact]
  public async Task UpdateQuestion_Returns500_OnException()
  {
    var update = new QuestionUpdate { ExamId = 2 };
    _mockService.Setup(s => s.UpdateQuestionAsync(1, update)).ThrowsAsync(new Exception("fail"));

    var result = await _controller.UpdateQuestion(1, update);

    var objectResult = Assert.IsType<ObjectResult>(result);
    Assert.Equal(500, objectResult.StatusCode);
    Assert.Equal("InternalServerError", ((Error)objectResult.Value!).Code);
  }

}
