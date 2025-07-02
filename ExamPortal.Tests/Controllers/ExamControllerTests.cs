using System.Security.Cryptography;
using ExamPortal.API.Implementations;
using ExamPortal.API.Models;
using ExamPortal.API.Models.Common;
using ExamPortal.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ExamPortal.Tests.Controllers;

public class ExamControllerTests
{
    private readonly Mock<IExamService> _mockService;
    private readonly Mock<ILogger<ExamController>> _mockLogger;
    private readonly ExamController _controller;
    private readonly Mock<ISalesDataService> _mockSalesService;
    private readonly Mock<IRuleValidationService> _mockRuleValidationService;

    public ExamControllerTests()
    {
        _mockService = new Mock<IExamService>();
        _mockLogger = new Mock<ILogger<ExamController>>();
        _mockSalesService = new Mock<ISalesDataService>();
        _mockRuleValidationService = new Mock<IRuleValidationService>();
        _controller = new ExamController(_mockService.Object, _mockLogger.Object, _mockSalesService.Object, _mockRuleValidationService.Object);
        _controller.ControllerContext = new ControllerContext();
    }

    [Fact]

    public async Task AddExam_Returns201_OnSuccess()
    {
        // Arrange
        var examCreate = new ExamCreate
        {
            Title = "Sample Exam",
            DurationMinutes = 60,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddHours(2)
        };
        _mockService.Setup(s => s.CreateExamAsync(examCreate)).ReturnsAsync(true);

        // Act 
        var result = await _controller.AddExam(examCreate);

        // Assert
        var statusResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(201, statusResult.StatusCode);
    }

    [Fact]
    public async Task AddExam_Returns400_OnServiceFailure()
    {
        // Arrange
        var examCreate = new ExamCreate
        {
            Title = "Sample Exam",
            Description = "Desc",
            DurationMinutes = 60,
            TotalMarks = 100,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddHours(2),
            CreatedBy = 1
        };
        _mockService.Setup(s => s.CreateExamAsync(examCreate)).ReturnsAsync(false);

        // Act 
        var result = await _controller.AddExam(examCreate);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var error = Assert.IsType<Error>(badRequest.Value);
        Assert.Equal("BadRequest", error.Code);
    }

    [Fact]

    public async Task AddExam_Returns400_WhenModelStateInvalid()
    {
        // Arrange 
        var examCreate = new ExamCreate();
        _controller.ModelState.AddModelError("Title", "Required");

        // Act 
        var result = await _controller.AddExam(examCreate);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var serializableError = Assert.IsType<SerializableError>(badRequest.Value);
        Assert.True(serializableError.ContainsKey("Title"));
        var errors = serializableError["Title"] as string[];
        Assert.NotNull(errors);
        Assert.Contains("Required", errors!);
    }

    [Fact]
    public async Task AddExam_Returns500_OnException()
    {
        // Arrange
        var examCreate = new ExamCreate
        {
            Title = "Sample Exam",
            DurationMinutes = 60,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddHours(2)
        };
        _mockService.Setup(s => s.CreateExamAsync(examCreate)).ThrowsAsync(new Exception("Something failed"));

        // Act
        var result = await _controller.AddExam(examCreate);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, objectResult.StatusCode);
        var error = Assert.IsType<Error>(objectResult.Value);
        Assert.Equal("InternalServerError", error.Code);
        Assert.Equal("Internal server error", error.Message);
    }

    [Fact]
    public async Task DeleteExam_ReturnsNoContent_OnSuccess()
    {
        // Arrange 
        int examId = 1;
        _mockService.Setup(s => s.DeleteExamAsync(examId)).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteExam(examId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteExam_ReturnsNotFound_OnMissing()
    {
        // Arrange
        int examId = 1;
        _mockService.Setup(s => s.DeleteExamAsync(examId)).ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteExam(examId);

        // Assert
        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        var error = Assert.IsType<Error>(notFound.Value);
        Assert.Equal("NotFound", error.Code);
    }

    [Fact]
    public async Task DeleteExam_Returns500_OnException()
    {
        // Arrange 
        int examId = 1;
        _mockService.Setup(s => s.DeleteExamAsync(examId)).ThrowsAsync(new Exception("Something failed"));

        // Act 
        var result = await _controller.DeleteExam(examId);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, objectResult.StatusCode);
        var error = Assert.IsType<Error>(objectResult.Value);
        Assert.Equal("InternalServerError", error.Code);
        Assert.Equal("Something failed", error.Message);
    }

    [Fact]

    public async Task GetExamById_ReturnsOk_WithExam()
    {
        // Arrange
        int examId = 1;
        var exam = new Exam
        {
            Id = examId,
            Title = "Sample Exam"
        };
        _mockService.Setup(s => s.GetExamByIdAsync(examId)).ReturnsAsync(exam);

        // Act
        var result = await _controller.GetExamById(examId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedExam = Assert.IsType<Exam>(okResult.Value);
        Assert.Equal(examId, returnedExam.Id);
    }

    [Fact]
    public async Task GetExamById_ReturnsNotFound_WhenMissing()
    {
        // Arrange
        int examId = 1;
        _mockService.Setup(s => s.GetExamByIdAsync(examId)).ReturnsAsync((Exam?)null);

        // Act
        var result = await _controller.GetExamById(examId);

        // Assert
        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        var error = Assert.IsType<Error>(notFound.Value);
        Assert.Equal("NotFound", error.Code);
    }

    [Fact]
    public async Task GetExamById_Returns500_OnException()
    {
        int examId = 1;
        _mockService.Setup(s => s.GetExamByIdAsync(examId)).ThrowsAsync(new Exception("DB Down"));

        var result = await _controller.GetExamById(examId);

        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, objectResult.StatusCode);
        var error = Assert.IsType<Error>(objectResult.Value);
        Assert.Equal("InternalServerError", error.Code);
        Assert.Equal("DB Down", error.Message);
    }

    [Fact]
    public async Task ListExams_ReturnsOk_WithPagedResponse()
    {
        // Arrange
        var pagedResponse = new PagedResponse<Exam>
        {
            Paging = new API.Models.Common.Paging { PageIndex = 1, PageSize = 10, TotalRecords = 1, TotalPages = 1 },
            Items = new List<Exam> { new Exam { Id = 1, Title = "Maths" } }
        };
        _mockService.Setup(s => s.ListExamsAsync(
            1, 10, null, null, null, null, null, null)).ReturnsAsync(pagedResponse);

        // Act
        var result = await _controller.ListExams(1, 10, null, null, null, null, null, null);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returned = Assert.IsType<PagedResponse<Exam>>(okResult.Value);
        Assert.Single(returned.Items);
    }

    [Fact]
    public async Task ListExams_Returns500_OnException()
    {
        _mockService.Setup(s => s.ListExamsAsync(
            1, 10, null, null, null, null, null, null))
            .ThrowsAsync(new Exception("Failed"));

        var result = await _controller.ListExams(1, 10, null, null, null, null, null, null);

        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, objectResult.StatusCode);
        var error = Assert.IsType<Error>(objectResult.Value);
        Assert.Equal("InternalServerError", error.Code);
        Assert.Equal("Internal server error", error.Message);
    }

    [Fact]
    public async Task UpdateExam_ReturnsOk_OnSuccess()
    {
        // Arrange
        int examId = 1;
        var update = new ExamUpdate
        {
            Title = "Updated Exam",
            Description = "Updated Desc",
            DurationMinutes = 90,
            TotalMarks = 120,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddHours(3),
            UpdatedBy = 2
        };
        _mockService.Setup(s => s.UpdateExamAsync(examId, update)).ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateExam(examId, update);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task UpdateExam_ReturnsNotFound_OnMissing()
    {
        // Arrange
        int examId = 1;
        var update = new ExamUpdate
        {
            Title = "Updated Exam",
            Description = "Updated Desc",
            DurationMinutes = 90,
            TotalMarks = 120,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddHours(3),
            UpdatedBy = 2
        };
        _mockService.Setup(s => s.UpdateExamAsync(examId, update)).ReturnsAsync(false);

        // Act
        var result = await _controller.UpdateExam(examId, update);

        // Assert
        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        var error = Assert.IsType<Error>(notFound.Value);
        Assert.Equal("NotFound", error.Code);
    }

    [Fact]
    public async Task UpdateExam_ReturnsBadRequest_WhenModelStateInvalid()
    {
        // Arrange
        int examId = 1;
        var update = new ExamUpdate();
        _controller.ModelState.AddModelError("Title", "Required");

        // Act
        var result = await _controller.UpdateExam(examId, update);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var serializableError = Assert.IsType<SerializableError>(badRequest.Value);
        Assert.True(serializableError.ContainsKey("Title"));
        var errors = serializableError["Title"] as string[];
        Assert.NotNull(errors);
        Assert.Contains("Required", errors!);
    }

    [Fact]
    public async Task UpdateExam_Returns500_OnException()
    {
        int examId = 1;
        var update = new ExamUpdate { Title = "Updated" };
        _mockService.Setup(s => s.UpdateExamAsync(examId, update)).ThrowsAsync(new Exception("DB Error"));
        var result = await _controller.UpdateExam(examId, update);
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, objectResult.StatusCode);
        var error = Assert.IsType<Error>(objectResult.Value);
        Assert.Equal("InternalServerError", error.Code);
        Assert.Equal("Internal server error", error.Message);
    }

}
