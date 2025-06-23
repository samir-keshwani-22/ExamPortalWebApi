
using ExamPortal.API.Implementations;
using ExamPortal.API.Models;
using ExamPortal.API.Models.Common;
using ExamPortal.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ExamPortal.Tests.Controllers;

public class AnswerControllerTests
{
    private readonly Mock<IAnswerService> _mockService;
    private readonly Mock<ILogger<AnswerController>> _mockLogger;

    private readonly AnswerController _controller;

    public AnswerControllerTests()
    {
        _mockService = new Mock<IAnswerService>();
        _mockLogger = new Mock<ILogger<AnswerController>>();
        _controller = new AnswerController(_mockService.Object, _mockLogger.Object);
        _controller.ControllerContext = new ControllerContext();
    }

    [Fact]
    public async Task AddAnswer_Returns201_OnSuccess()
    {
        var create = new AnswerCreate();
        _mockService.Setup(s => s.CreateAnswerAsync(create)).ReturnsAsync(true);

        var result = await _controller.AddAnswer(create);

        var status = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(201, status.StatusCode);
    }

    [Fact]
    public async Task AddAnswer_Returns400_OnServiceFailure()
    {
        var create = new AnswerCreate();
        _mockService.Setup(s => s.CreateAnswerAsync(create)).ReturnsAsync(false);

        var result = await _controller.AddAnswer(create);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var error = Assert.IsType<Error>(badRequest.Value);
        Assert.Equal("BadRequest", error.Code);
    }

    [Fact]
    public async Task AddAnswer_Returns400_WhenModelStateInvalid()
    {
        var create = new AnswerCreate();
        _controller.ModelState.AddModelError("Text", "Required");

        var result = await _controller.AddAnswer(create);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var serializableError = Assert.IsType<SerializableError>(badRequest.Value);
        Assert.True(serializableError.ContainsKey("Text"));
    }

    [Fact]
    public async Task AddAnswer_Returns500_OnException()
    {
        var create = new AnswerCreate();
        _mockService.Setup(s => s.CreateAnswerAsync(create)).ThrowsAsync(new Exception("fail"));

        var result = await _controller.AddAnswer(create);

        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, objectResult.StatusCode);
        var error = Assert.IsType<Error>(objectResult.Value);
        Assert.Equal("InternalServerError", error.Code);

    }

    [Fact]
    public async Task DeleteAnswer_ReturnsNoContent_OnSuccess()
    {
        _mockService.Setup(s => s.DeleteAnswerAsync(1)).ReturnsAsync(true);

        var result = await _controller.DeleteAnswer(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteAnswer_ReturnsNotFound_OnMissing()
    {
        _mockService.Setup(s => s.DeleteAnswerAsync(1)).ReturnsAsync(false);

        var result = await _controller.DeleteAnswer(1);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        var error = Assert.IsType<Error>(notFound.Value);
        Assert.Equal("NotFound", error.Code);
    }

    [Fact]
    public async Task DeleteAnswer_Returns500_OnException()
    {
        _mockService.Setup(s => s.DeleteAnswerAsync(1)).ThrowsAsync(new Exception("fail"));

        var result = await _controller.DeleteAnswer(1);

        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, objectResult.StatusCode);
        var error = Assert.IsType<Error>(objectResult.Value);
        Assert.Equal("InternalServerError", error.Code);
        Assert.Equal("fail", error.Message);
    }

    [Fact]
    public async Task GetAnswerById_ReturnsOk_WhenFound()
    {
        var answer = new Answer { Id = 1 };
        _mockService.Setup(s => s.GetAnswerByIdAsync(1)).ReturnsAsync(answer);

        var result = await _controller.GetAnswerById(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        var returned = Assert.IsType<Answer>(ok.Value);
        Assert.Equal(1, returned.Id);
    }

    [Fact]
    public async Task GetAnswerById_ReturnsNotFound_WhenMissing()
    {
        _mockService.Setup(s => s.GetAnswerByIdAsync(1)).ReturnsAsync((Answer?)null);

        var result = await _controller.GetAnswerById(1);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        var error = Assert.IsType<Error>(notFound.Value);
        Assert.Equal("NotFound", error.Code);
    }

    [Fact]
    public async Task GetAnswerById_Returns500_OnException()
    {
        _mockService.Setup(s => s.GetAnswerByIdAsync(1)).ThrowsAsync(new Exception("fail"));

        var result = await _controller.GetAnswerById(1);

        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, objectResult.StatusCode);
        var error = Assert.IsType<Error>(objectResult.Value);
        Assert.Equal("InternalServerError", error.Code);
        Assert.Equal("fail", error.Message);
    }

    [Fact]
    public async Task ListAnswers_ReturnsOk_WithPagedResponse()
    {
        var paged = new PagedResponse<Answer>
        {
            Paging = new API.Models.Common.Paging
            {
                PageIndex = 1,
                PageSize = 10,
                TotalRecords = 1,
                TotalPages = 1
            },
            Items = new List<Answer>{
                new Answer{
                    Id=1
                }
            }
        };
        _mockService.Setup(s => s.ListAnswersAsync(1, 10)).ReturnsAsync(paged);

        var result = await _controller.ListAnswers(1, 10);

        var ok = Assert.IsType<OkObjectResult>(result);
        var returned = Assert.IsType<PagedResponse<Answer>>(ok.Value);
        Assert.Single(returned.Items);
    }

    [Fact]
    public async Task ListAnswers_Returns500_OnException()
    {
        _mockService.Setup(s => s.ListAnswersAsync(1, 10)).ThrowsAsync(new Exception("fail"));

        var result = await _controller.ListAnswers(1, 10);

        var objectResult = Assert.IsType<ObjectResult>(result);

    }
}
