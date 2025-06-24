using AutoMapper;
using ExamPortal.API.Models;
using ExamPortal.API.Models.Common;
using ExamPortal.Business.Interfaces;
using ExamPortal.Business.Managers;
using Moq;

namespace ExamPortal.Tests.Services;

public class QuestionOptionServiceTests
{
    private readonly Mock<IQuestionOptionRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly QuestionOptionService _service;

    public QuestionOptionServiceTests()
    {
        _mockRepo = new Mock<IQuestionOptionRepository>();
        _mockMapper = new Mock<IMapper>();
        _service = new QuestionOptionService(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task CreateQuestionOptionAsync_ReturnsTrue_OnSuccess()
    {
        var createModel = new QuestionOptionCreate();
        var entity = new API.Models.Entities.QuestionOption();
        _mockMapper.Setup(m => m.Map<API.Models.Entities.QuestionOption>(createModel)).Returns(entity);
        _mockRepo.Setup(r => r.CreateAsync(entity)).ReturnsAsync(true);

        var result = await _service.CreateQuestionOptionAsync(createModel);

        Assert.True(result);
    }

    [Fact]
    public async Task CreateQuestionOptionAsync_ReturnsFalse_OnFailure()
    {
        var createModel = new QuestionOptionCreate();
        var entity = new API.Models.Entities.QuestionOption();
        _mockMapper.Setup(m => m.Map<API.Models.Entities.QuestionOption>(createModel)).Returns(entity);
        _mockRepo.Setup(r => r.CreateAsync(entity)).ReturnsAsync(false);

        var result = await _service.CreateQuestionOptionAsync(createModel);

        Assert.False(result);
    }

    [Fact]
    public async Task DeleteQuestionOptionAsync_ReturnsTrue_OnSuccess()
    {
        _mockRepo.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

        var result = await _service.DeleteQuestionOptionAsync(1);

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteQuestionOptionAsync_ReturnsFalse_OnFailure()
    {
        _mockRepo.Setup(r => r.DeleteAsync(1)).ReturnsAsync(false);

        var result = await _service.DeleteQuestionOptionAsync(1);

        Assert.False(result);
    }

    [Fact]
    public async Task GetQuestionOptionByIdAsync_ReturnsMappedOption_WhenExists()
    {
        var entity = new API.Models.Entities.QuestionOption { Id = 1 };
        var model = new QuestionOption { Id = 1 };
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);
        _mockMapper.Setup(m => m.Map<QuestionOption>(entity)).Returns(model);

        var result = await _service.GetQuestionOptionByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetQuestionOptionByIdAsync_ReturnsNull_WhenNotFound()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((API.Models.Entities.QuestionOption?)null);

        var result = await _service.GetQuestionOptionByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task ListQuestionOptionsAsync_ReturnsPagedResponse_WhenItemsFound()
    {
        var pagedResult = new PagedResult<API.Models.Entities.QuestionOption>
        {
            TotalCount = 1,
            Items = new List<API.Models.Entities.QuestionOption>
            {
                new API.Models.Entities.QuestionOption { Id = 1 }
            }
        };
        var mapped = new List<QuestionOption>
        {
            new QuestionOption { Id = 1 }
        };
        _mockRepo.Setup(r => r.GetPagedAsync(1, 1)).ReturnsAsync(pagedResult);
        _mockMapper.Setup(m => m.Map<List<QuestionOption>>(pagedResult.Items)).Returns(mapped);

        var result = await _service.ListQuestionOptionsAsync(1, 1);


        Assert.NotNull(result);
        Assert.Equal(1, result.Paging.TotalRecords);
        Assert.Single(result.Items);
    }

    [Fact]
    public async Task ListQuestionOptionsAsync_ReturnsEmptyList_WhenNoItems()
    {
        var pagedResult = new PagedResult<API.Models.Entities.QuestionOption>
        {
            TotalCount = 0,
            Items = new List<API.Models.Entities.QuestionOption>()
        };
        _mockRepo.Setup(r => r.GetPagedAsync(1, 10)).ReturnsAsync(pagedResult);
        _mockMapper.Setup(m => m.Map<List<QuestionOption>>(pagedResult.Items)).Returns(new List<QuestionOption>());

        var result = await _service.ListQuestionOptionsAsync(1, 10);

        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.Paging.TotalRecords);
        Assert.Equal(0, result.Paging.TotalPages);
    }

    [Fact]
    public async Task UpdateQuestionOptionAsync_ReturnsTrue_WhenUpdateSucceeds()
    {
        var existing = new API.Models.Entities.QuestionOption { Id = 1 };
        var update = new QuestionOptionUpdate();

        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);
        _mockRepo.Setup(r => r.UpdateAsync(existing)).ReturnsAsync(true);

        var result = await _service.UpdateQuestionOptionAsync(1, update);

        Assert.True(result);
        _mockMapper.Verify(m => m.Map(update, existing), Times.Once);
    }

    [Fact]
    public async Task UpdateQuestionOptionAsync_ReturnsFalse_WhenOptionNotFound()
    {
        var update = new QuestionOptionUpdate();
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((API.Models.Entities.QuestionOption?)null);

        var result = await _service.UpdateQuestionOptionAsync(1, update);

        Assert.False(result);
        _mockMapper.Verify(m => m.Map(It.IsAny<QuestionOptionUpdate>(), It.IsAny<API.Models.Entities.QuestionOption>()), Times.Never);
    }

    [Fact]
    public async Task UpdateQuestionOptionAsync_ReturnsFalse_WhenRepositoryFails()
    {
        var existing = new API.Models.Entities.QuestionOption { Id = 1 };
        var update = new QuestionOptionUpdate();

        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);
        _mockRepo.Setup(r => r.UpdateAsync(existing)).ReturnsAsync(false);

        var result = await _service.UpdateQuestionOptionAsync(1, update);

        Assert.False(result);
        _mockMapper.Verify(m => m.Map(update, existing), Times.Once);
    }

}
