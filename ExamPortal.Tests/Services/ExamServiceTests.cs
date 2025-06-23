using AutoMapper;
using ExamPortal.API.Models;
using ExamPortal.API.Models.Common;
using ExamPortal.Business.Interfaces;
using ExamPortal.Business.Managers;
using Microsoft.AspNetCore.Authorization;
using Moq;

namespace ExamPortal.Tests.Services;

public class ExamServiceTests
{
    private readonly Mock<IExamRepository> _mockRepo ;
    private readonly Mock<IMapper> _mockMapper;

    private readonly ExamService _service;

    public ExamServiceTests()
    {
        _mockRepo = new Mock<IExamRepository>();
        _mockMapper = new Mock<IMapper>();
        _service = new ExamService(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task CreateExamAsync_ReturnsTrue_OnSuccess()
    {
        var examCreate = new ExamCreate { DurationMinutes = 60 };
        var examEntity = new API.Models.Entities.Exam();
        _mockMapper.Setup(m => m.Map<API.Models.Entities.Exam>(examCreate)).Returns(examEntity);
        _mockRepo.Setup(r => r.CreateAsync(examEntity)).ReturnsAsync(true);
        var result = await _service.CreateExamAsync(examCreate);
        Assert.True(result);
    }

    [Fact]
    public async Task CreateExamAsync_ReturnsFalse_WhenRepositoryFails()
    {
        var examCreate = new ExamCreate { Title = "Test", DurationMinutes = 60 };
        var mappedEntity = new API.Models.Entities.Exam();
        _mockMapper.Setup(m => m.Map<API.Models.Entities.Exam>(examCreate)).Returns(mappedEntity);
        _mockRepo.Setup(r => r.CreateAsync(mappedEntity)).ReturnsAsync(false);
        var result = await _service.CreateExamAsync(examCreate);
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteExamAsync_ReturnsTrue_WhenRepositorySucceeds()
    {
        _mockRepo.Setup(r => r.DeleteExamAsync(1)).ReturnsAsync(true);

        var result = await _service.DeleteExamAsync(1);

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteExamAsync_ReturnsFalse_WhenRepositoryFails()
    {
        _mockRepo.Setup(r => r.DeleteExamAsync(1)).ReturnsAsync(false);

        var result = await _service.DeleteExamAsync(1);

        Assert.False(result);
    }


    [Fact]
    public async Task GetExamByIdAsync_ReturnsMappedExam_WhenFound()
    {
        var entity = new API.Models.Entities.Exam { Id = 1 };
        var model = new Exam { Id = 1 };

        _mockRepo.Setup(r => r.GetExamByIdAsync(1)).ReturnsAsync(entity);
        _mockMapper.Setup(m => m.Map<Exam>(entity)).Returns(model);

        var result = await _service.GetExamByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetExamByIdAsync_ReturnsNull_WhenNotFound()
    {
        _mockRepo.Setup(r => r.GetExamByIdAsync(1)).ReturnsAsync((API.Models.Entities.Exam?)null);

        var result = await _service.GetExamByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task ListExamsAsync_ReturnsPagedResponse_WhenExamsFound()
    {
        var pagedResult = new PagedResult<API.Models.Entities.Exam>
        {
            TotalCount = 1,
            Items = new List<API.Models.Entities.Exam> { new API.Models.Entities.Exam { Id = 1 } }
        };

        var mappedItems = new List<Exam> { new Exam { Id = 1 } };

        _mockRepo.Setup(r => r.GetPagedExamsAsync(
            1, 10, null, null, null, null, null, null))
            .ReturnsAsync(pagedResult);

        _mockMapper.Setup(m => m.Map<List<Exam>>(pagedResult.Items)).Returns(mappedItems);

        var result = await _service.ListExamsAsync(1, 10, null, null, null, null, null, null);

        Assert.NotNull(result);
        Assert.Equal(1, result.Paging.TotalRecords);
        Assert.Single(result.Items);
    }

    [Fact]
    public async Task ListExamsAsync_ReturnsEmptyList_WhenNoItems()
    {
        var pagedResult = new PagedResult<API.Models.Entities.Exam>
        {
            TotalCount = 0,
            Items = new List<API.Models.Entities.Exam>()
        };

        _mockRepo.Setup(r => r.GetPagedExamsAsync(
            1, 10, null, null, null, null, null, null)).ReturnsAsync(pagedResult);

        _mockMapper.Setup(m => m.Map<List<Exam>>(pagedResult.Items)).Returns(new List<Exam>());

        var result = await _service.ListExamsAsync(1, 10, null, null, null, null, null, null);

        Assert.Empty(result.Items);
        Assert.Equal(0, result.Paging.TotalPages);
    }

    [Fact]
    public async Task UpdateExamAsync_ReturnsTrue_WhenUpdateSucceeds()
    {
        var existing = new API.Models.Entities.Exam { Id = 1 };
        var update = new ExamUpdate { Title = "Updated" };

        _mockRepo.Setup(r => r.GetExamByIdAsync(1)).ReturnsAsync(existing);
        _mockRepo.Setup(r => r.UpdateExamAsync(existing)).ReturnsAsync(true);

        var result = await _service.UpdateExamAsync(1, update);

        Assert.True(result);
        _mockMapper.Verify(m => m.Map(update, existing), Times.Once);
    }

    [Fact]

    public async Task UpdateExamAsync_ReturnsFalse_WhenExamNotFound()
    {
        _mockRepo.Setup(r => r.GetExamByIdAsync(1)).ReturnsAsync((API.Models.Entities.Exam?)null);
        var result = await _service.UpdateExamAsync(1, new ExamUpdate());
        Assert.False(result);
    }

    [Fact]

    public async Task UpdateExamAsync_ReturnsFalse_WhenRepositoryFails()
    {
        var existing = new API.Models.Entities.Exam { Id = 1 };
        var update = new ExamUpdate
        {
            Title = "Update"
        };
        _mockRepo.Setup(r => r.GetExamByIdAsync(1)).ReturnsAsync(existing);
        _mockRepo.Setup(r => r.UpdateExamAsync(existing)).ReturnsAsync(false);
        var result = await _service.UpdateExamAsync(1, update);
        Assert.False(result);

    }

}
