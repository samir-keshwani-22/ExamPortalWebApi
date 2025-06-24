using AutoMapper;
using ExamPortal.API.Models;
using ExamPortal.API.Models.Common;
using ExamPortal.Business.Interfaces;
using ExamPortal.Business.Managers;
using Moq;

namespace ExamPortal.Tests.Services;

public class QuestionServiceTests
{
    private readonly Mock<IQuestionRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;

    private readonly QuestionService _service;

    public QuestionServiceTests()
    {
        _mockRepo = new Mock<IQuestionRepository>();
        _mockMapper = new Mock<IMapper>();
        _service = new QuestionService(_mockRepo.Object, _mockMapper.Object);
    }

    private API.Models.Entities.Exam DummyExam()
    {
        return new API.Models.Entities.Exam
        {
        };
    }

    [Fact]
    public async Task CreateQuestionAsync_ReturnsTrue_OnSuccess()
    {
        var questionCreate = new QuestionCreate();
        var questionEntty = new API.Models.Entities.Question
        {
            Exam = DummyExam()
        };

        _mockMapper.Setup(m => m.Map<API.Models.Entities.Question>(questionCreate)).Returns(questionEntty);
        _mockRepo.Setup(r => r.CreateAsync(questionEntty)).ReturnsAsync(true);

        var result = await _service.CreateQuestionAsync(questionCreate);

        Assert.True(result);

    }

    [Fact]
    public async Task CreateQuestionAsync_ReturnsFalse_OnRepositoryFail()
    {
        var questionCreate = new QuestionCreate();
        var questionEntity = new API.Models.Entities.Question
        {
            Exam = DummyExam()
        };

        _mockMapper.Setup(m => m.Map<API.Models.Entities.Question>(questionCreate)).Returns(questionEntity);
        _mockRepo.Setup(r => r.CreateAsync(questionEntity)).ReturnsAsync(false);

        var result = await _service.CreateQuestionAsync(questionCreate);

        Assert.False(result);
    }

    [Fact]
    public async Task DeleteQuestionAsync_ReturnsTrue_WhenRepositorySucceeds()
    {
        _mockRepo.Setup(r => r.DeleteQuestionAsync(1)).ReturnsAsync(true);

        var result = await _service.DeleteQuestionAsync(1);

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteQuestionAsync_ReturnsFalse_WhenRepositoryFails()
    {
        _mockRepo.Setup(r => r.DeleteQuestionAsync(1)).ReturnsAsync(false);

        var result = await _service.DeleteQuestionAsync(1);

        Assert.False(result);
    }

    [Fact]
    public async Task GetQuestionByIdAsync_ReturnsMappedQuestion_WhenExists()
    {
        var entity = new API.Models.Entities.Question
        {
            Id = 1,
            Exam = DummyExam()
        };
        var model = new API.Models.Question { Id = 1 };

        _mockRepo.Setup(r => r.GetQuestionByIdAsync(1)).ReturnsAsync(entity);
        _mockMapper.Setup(m => m.Map<API.Models.Question>(entity)).Returns(model);

        var result = await _service.GetQuestionByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetQuestionByIdAsync_ReturnsNull_WhenNotFound()
    {
        _mockRepo.Setup(r => r.GetQuestionByIdAsync(1)).ReturnsAsync((API.Models.Entities.Question?)null);

        var result = await _service.GetQuestionByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task ListQuestionsAsync_ReturnsPagedResponse_WhenItemsFound()
    {
        var pagedResult = new PagedResult<API.Models.Entities.Question>
        {
            TotalCount = 1,
            Items = new List<API.Models.Entities.Question>
                {
                    new API.Models.Entities.Question { Id = 1, Exam = DummyExam() }
                }
        };
        var mappedItems = new List<API.Models.Question>
            {
                new API.Models.Question { Id = 1 }
            };

        _mockRepo.Setup(r => r.GetPagedQuestionsAsync(1, 1)).ReturnsAsync(pagedResult);
        _mockMapper.Setup(m => m.Map<List<API.Models.Question>>(pagedResult.Items)).Returns(mappedItems);

        var result = await _service.ListQuestionsAsync(1, 1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Paging.TotalRecords);
        Assert.Single(result.Items);
    }

    [Fact]
    public async Task ListQuestionsAsync_ReturnsEmptyList_WhenNoItems()
    {
        var pagedResult = new PagedResult<API.Models.Entities.Question>
        {
            TotalCount = 0,
            Items = new List<API.Models.Entities.Question>()
        };

        _mockRepo.Setup(r => r.GetPagedQuestionsAsync(1, 10)).ReturnsAsync(pagedResult);
        _mockMapper.Setup(m => m.Map<List<API.Models.Question>>(pagedResult.Items)).Returns(new List<API.Models.Question>());

        var result = await _service.ListQuestionsAsync(1, 10);

        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.Paging.TotalRecords);
        Assert.Equal(0, result.Paging.TotalPages);
    }

    [Fact]
    public async Task UpdateQuestionAsync_ReturnsTrue_WhenUpdateSucceeds()
    {
        var existing = new API.Models.Entities.Question { Id = 1, Exam = DummyExam() };
        var update = new QuestionUpdate();

        _mockRepo.Setup(r => r.GetQuestionByIdAsync(1)).ReturnsAsync(existing);
        _mockRepo.Setup(r => r.UpdateQuestionAsync(existing)).ReturnsAsync(true);

        var result = await _service.UpdateQuestionAsync(1, update);

        Assert.True(result);
        _mockMapper.Verify(m => m.Map(update, existing), Times.Once);
    }

    [Fact]
    public async Task UpdateQuestionAsync_ReturnsFalse_WhenQuestionNotFound()
    {
        var update = new QuestionUpdate();
        _mockRepo.Setup(r => r.GetQuestionByIdAsync(1)).ReturnsAsync((API.Models.Entities.Question?)null);

        var result = await _service.UpdateQuestionAsync(1, update);

        Assert.False(result);
        _mockMapper.Verify(m => m.Map(It.IsAny<QuestionUpdate>(), It.IsAny<API.Models.Entities.Question>()), Times.Never);
    }

    [Fact]
    public async Task UpdateQuestionAsync_ReturnsFalse_WhenRepositoryFails()
    {
        var existing = new API.Models.Entities.Question { Id = 1, Exam = DummyExam() };
        var update = new QuestionUpdate();

        _mockRepo.Setup(r => r.GetQuestionByIdAsync(1)).ReturnsAsync(existing);
        _mockRepo.Setup(r => r.UpdateQuestionAsync(existing)).ReturnsAsync(false);

        var result = await _service.UpdateQuestionAsync(1, update);

        Assert.False(result);
        _mockMapper.Verify(m => m.Map(update, existing), Times.Once);
    }
}
