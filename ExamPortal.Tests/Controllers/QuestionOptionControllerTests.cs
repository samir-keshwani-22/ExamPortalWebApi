using ExamPortal.API.Implementations;
using ExamPortal.API.Models;
using ExamPortal.API.Models.Common;
using ExamPortal.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ExamPortal.Tests.Controllers;

public class QuestionOptionControllerTests
{
    private readonly Mock<IQuestionOptionService> _mockService;
    private readonly Mock<ILogger<QuestionOptionController>> _mockLogger;
    private readonly QuestionOptionController _controller;

    public QuestionOptionControllerTests()
    {
        _mockService = new Mock<IQuestionOptionService>();
        _mockLogger = new Mock<ILogger<QuestionOptionController>>();
        _controller = new QuestionOptionController(_mockService.Object, _mockLogger.Object);
        _controller.ControllerContext = new ControllerContext();
    }

    [Fact]
    public async Task AddQuestionOption_Returns201_OnSuccess()
    {
        var create = new QuestionOptionCreate();
        _mockService.Setup(s => s.CreateQuestionOptionAsync(create)).ReturnsAsync(true);

        var result = await _controller.AddQuestionOption(create);

        var status = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(201, status.StatusCode);
    }

    [Fact]
    public async Task AddQuestionOption_Returns400_OnFailure()
    {
        var create = new QuestionOptionCreate();
        _mockService.Setup(s => s.CreateQuestionOptionAsync(create)).ReturnsAsync(false);

        var result = await _controller.AddQuestionOption(create);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var error = Assert.IsType<Error>(badRequest.Value);
        Assert.Equal("BadRequest", error.Code);
    }

    [Fact]
    public async Task AddQuestionOption_Returns400_WhenModelInvalid()
    {
        var create = new QuestionOptionCreate();
        _controller.ModelState.AddModelError("Text", "Required");

        var result = await _controller.AddQuestionOption(create);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.True(((SerializableError)badRequest.Value!).ContainsKey("Text"));
    }

    [Fact]
    public async Task AddQuestionOption_Returns500_OnException()
    {
        var create = new QuestionOptionCreate();
        _mockService.Setup(s => s.CreateQuestionOptionAsync(create)).ThrowsAsync(new Exception("fail"));

        var result = await _controller.AddQuestionOption(create);

        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, objectResult.StatusCode);
        Assert.Equal("InternalServerError", ((Error)objectResult.Value!).Code);
    }

    [Fact]
    public async Task DeleteQuestionOption_ReturnsNoContent_OnSuccess()
    {
        _mockService.Setup(s => s.DeleteQuestionOptionAsync(1)).ReturnsAsync(true);

        var result = await _controller.DeleteQuestionOption(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteQuestionOption_ReturnsNotFound_WhenMissing()
    {
        _mockService.Setup(s => s.DeleteQuestionOptionAsync(1)).ReturnsAsync(false);

        var result = await _controller.DeleteQuestionOption(1);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("NotFound", ((Error)notFound.Value!).Code);
    }

    [Fact]
    public async Task DeleteQuestionOption_Returns500_OnException()
    {
        _mockService.Setup(s => s.DeleteQuestionOptionAsync(1)).ThrowsAsync(new Exception("fail"));

        var result = await _controller.DeleteQuestionOption(1);

        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, objectResult.StatusCode);
        Assert.Equal("fail", ((Error)objectResult.Value!).Message);
    }

    [Fact]
    public async Task GetQuestionOptionById_ReturnsOk_WhenFound()
    {
        var option = new QuestionOption { Id = 1 };
        _mockService.Setup(s => s.GetQuestionOptionByIdAsync(1)).ReturnsAsync(option);

        var result = await _controller.GetQuestionOptionById(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(1, ((QuestionOption)ok.Value!).Id);
    }

    [Fact]
    public async Task GetQuestionOptionById_ReturnsNotFound_WhenMissing()
    {
        _mockService.Setup(s => s.GetQuestionOptionByIdAsync(1)).ReturnsAsync((QuestionOption?)null);

        var result = await _controller.GetQuestionOptionById(1);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("NotFound", ((Error)notFound.Value!).Code);
    }

    [Fact]
    public async Task GetQuestionOptionById_Returns500_OnException()
    {
        _mockService.Setup(s => s.GetQuestionOptionByIdAsync(1)).ThrowsAsync(new Exception("fail"));

        var result = await _controller.GetQuestionOptionById(1);

        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, objectResult.StatusCode);
        Assert.Equal("fail", ((Error)objectResult.Value!).Message);
    }

    [Fact]
    public async Task ListQuestionOptions_ReturnsOk_WithPagedResult()
    {
        var paged = new PagedResponse<QuestionOption>
        {
            Paging = new API.Models.Common.Paging { PageIndex = 1, PageSize = 10, TotalPages = 1, TotalRecords = 1 },
            Items = new List<QuestionOption> { new QuestionOption { Id = 1 } }
        };

        _mockService.Setup(s => s.ListQuestionOptionsAsync(1, 10)).ReturnsAsync(paged);

        var result = await _controller.ListQuestionOptions(1, 10);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Single(((PagedResponse<QuestionOption>)ok.Value!).Items);
    }

    [Fact]
    public async Task ListQuestionOptions_Returns500_OnException()
    {
        _mockService.Setup(s => s.ListQuestionOptionsAsync(1, 10)).ThrowsAsync(new Exception("fail"));

        var result = await _controller.ListQuestionOptions(1, 10);

        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, objectResult.StatusCode);
        Assert.Equal("InternalServerError", ((Error)objectResult.Value!).Code);
    }

    [Fact]
    public async Task UpdateQuestionOption_ReturnsOk_OnSuccess()
    {
        var update = new QuestionOptionUpdate { QuestionId = 1 };
        _mockService.Setup(s => s.UpdateQuestionOptionAsync(1, update)).ReturnsAsync(true);

        var result = await _controller.UpdateQuestionOption(1, update);

        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task UpdateQuestionOption_ReturnsBadRequest_WhenQuestionIdMissing()
    {
        var update = new QuestionOptionUpdate { QuestionId = 0 };

        var result = await _controller.UpdateQuestionOption(1, update);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.True(((SerializableError)badRequest.Value!).ContainsKey("QuestionId"));
    }

    [Fact]
    public async Task UpdateQuestionOption_ReturnsNotFound_OnFailure()
    {
        var update = new QuestionOptionUpdate { QuestionId = 1 };
        _mockService.Setup(s => s.UpdateQuestionOptionAsync(1, update)).ReturnsAsync(false);

        var result = await _controller.UpdateQuestionOption(1, update);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("NotFound", ((Error)notFound.Value!).Code);
    }

    [Fact]
    public async Task UpdateQuestionOption_Returns500_OnException()
    {
        var update = new QuestionOptionUpdate { QuestionId = 1 };
        _mockService.Setup(s => s.UpdateQuestionOptionAsync(1, update)).ThrowsAsync(new Exception("fail"));

        var result = await _controller.UpdateQuestionOption(1, update);

        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, objectResult.StatusCode);
        Assert.Equal("InternalServerError", ((Error)objectResult.Value!).Code);
    }

}
