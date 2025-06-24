using AutoMapper;
using ExamPortal.API.Models;
using ExamPortal.API.Models.Common;
using ExamPortal.Business.Interfaces;
using ExamPortal.Business.Managers;
using Microsoft.AspNetCore.Authorization;
using Moq;

namespace ExamPortal.Tests.Services;

public class AnswerServiceTests
{

    private readonly Mock<IAnswerRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly AnswerService _service;
    public AnswerServiceTests()
    {
        _mockRepo = new Mock<IAnswerRepository>();
        _mockMapper = new Mock<IMapper>();
        _service = new AnswerService(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task CreateAnswerAsync_ReturnsTrue_OnSuccess()
    {
        var answerCreate = new AnswerCreate();
        var answerEntity = new API.Models.Entities.Answer
        {
            Question = new API.Models.Entities.Question
            {
                Id = 1,
                ExamId = 1,
                Exam = new API.Models.Entities.Exam()
            }
        };

        _mockMapper.Setup(m => m.Map<API.Models.Entities.Answer>(answerCreate)).Returns(answerEntity);
        _mockRepo.Setup(r => r.CreateAsync(answerEntity)).ReturnsAsync(true);

        var result = await _service.CreateAnswerAsync(answerCreate);

        Assert.True(result);
    }

    [Fact]
    public async Task CreateAnswerAsync_ReturnsFalse_OnRepositoryFail()
    {
        var answerCreate = new AnswerCreate();
        var answerEntity = new API.Models.Entities.Answer
        {
            Question = new API.Models.Entities.Question
            {
                Id = 1,
                ExamId = 1,
                Exam = new API.Models.Entities.Exam()
            }
        };
        _mockMapper.Setup(s => s.Map<API.Models.Entities.Answer>(answerCreate)).Returns(answerEntity);
        _mockRepo.Setup(s => s.CreateAsync(answerEntity)).ReturnsAsync(false);

        var result = await _service.CreateAnswerAsync(answerCreate);

        Assert.False(result);
    }

    [Fact]
    public async Task DeleteAnswerAsync_ReturnsTrue_WhenRepositorySucceeds()
    {
        _mockRepo.Setup(r => r.DeleteAnswerAsync(1)).ReturnsAsync(true);

        var result = await _service.DeleteAnswerAsync(1);

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAnswerAsync_ReturnsFalse_WhenRepositoryFails()
    {
        _mockRepo.Setup(r => r.DeleteAnswerAsync(1)).ReturnsAsync(false);

        var result = await _service.DeleteAnswerAsync(1);

        Assert.False(result);
    }

    [Fact]
    public async Task GetAnswerByIdAsync_ReturnsMappedAnswer_WhenExists()
    {
        var entity = new API.Models.Entities.Answer
        {
            Id = 1,
            Question = new API.Models.Entities.Question
            {
                Id = 1,
                ExamId = 1,
                Exam = new API.Models.Entities.Exam()
            }
        };
        var model = new Answer { Id = 1 };

        _mockRepo.Setup(r => r.GetAnswerByIdAsnc(1)).ReturnsAsync(entity);
        _mockMapper.Setup(m => m.Map<Answer>(entity)).Returns(model);

        var result = await _service.GetAnswerByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }


    [Fact]
    public async Task GetAnswerByIdAsync_ReturnsNull_WhenNotFound()
    {
        _mockRepo.Setup(r => r.GetAnswerByIdAsnc(1)).ReturnsAsync((API.Models.Entities.Answer?)null);

        var result = await _service.GetAnswerByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task ListAnswersAsync_ReturnsPagedResponse_WhenItemsFound()
    {
        var pagedResult = new PagedResult<API.Models.Entities.Answer>
        {
            TotalCount = 1,
            Items = new List<API.Models.Entities.Answer>
                {
                    new API.Models.Entities.Answer
                    {
                        Id = 1,
                        Question = new API.Models.Entities.Question
                        {
                            Id = 1,
                            ExamId = 1,
                            Exam = new API.Models.Entities.Exam()
                        }
                    }

                }
        };
        var mappedItems = new List<Answer>
            {
                new Answer { Id = 1 }
            };


        _mockRepo.Setup(r => r.GetPagedAnswersAsync(1, 1)).ReturnsAsync(pagedResult);
        _mockMapper.Setup(m => m.Map<List<Answer>>(pagedResult.Items)).Returns(mappedItems);

        var result = await _service.ListAnswersAsync(1, 1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Paging.TotalRecords);
        Assert.Single(result.Items);

    }


    [Fact]
    public async Task ListAnswersAsync_ReturnsEmptyList_WhenNoItems()
    {
        var pagedResult = new PagedResult<API.Models.Entities.Answer>
        {
            TotalCount = 0,
            Items = new List<API.Models.Entities.Answer>()
        };

        _mockRepo.Setup(r => r.GetPagedAnswersAsync(1, 10)).ReturnsAsync(pagedResult);
        _mockMapper.Setup(m => m.Map<List<Answer>>(pagedResult.Items)).Returns(new List<Answer>());

        var result = await _service.ListAnswersAsync(1, 10);

        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.Paging.TotalRecords);
        Assert.Equal(0, result.Paging.TotalPages);
    }

    [Fact]
    public async Task UpdateAnswerAsync_ReturnsTrue_WhenUpdateSucceeds()
    {
        var existing = new API.Models.Entities.Answer
        {
            Id = 1,
            Question = new API.Models.Entities.Question
            {
                Id = 1,
                ExamId = 1,
                Exam = new API.Models.Entities.Exam()
            }
        };
        var update = new AnswerUpdate();

        _mockRepo.Setup(r => r.GetAnswerByIdAsnc(1)).ReturnsAsync(existing);
        _mockRepo.Setup(r => r.UpdateAnswerAsync(existing)).ReturnsAsync(true);

        var result = await _service.UpdateAnswerAsync(1, update);

        Assert.True(result);
        _mockMapper.Verify(m => m.Map(update, existing), Times.Once);
    }

    [Fact]
    public async Task UpdateAnswerAsync_ReturnsFalse_WhenAnswerNotFound()
    {
        var update = new AnswerUpdate();
        _mockRepo.Setup(r => r.GetAnswerByIdAsnc(1)).ReturnsAsync((API.Models.Entities.Answer?)null);

        var result = await _service.UpdateAnswerAsync(1, update);

        Assert.False(result);
        _mockMapper.Verify(m => m.Map(It.IsAny<AnswerUpdate>(), It.IsAny<API.Models.Entities.Answer>()), Times.Never);
    }

    [Fact]
    public async Task UpdateAnswerAsync_ReturnsFalse_WhenRepositoryFails()
    {
        var existing = new API.Models.Entities.Answer
        {
            Id = 1,
            Question = new API.Models.Entities.Question
            {
                Id = 1,
                ExamId = 1,
                Exam = new API.Models.Entities.Exam()
            }
        };
        var update = new AnswerUpdate();

        _mockRepo.Setup(r => r.GetAnswerByIdAsnc(1)).ReturnsAsync(existing);
        _mockRepo.Setup(r => r.UpdateAnswerAsync(existing)).ReturnsAsync(false);

        var result = await _service.UpdateAnswerAsync(1, update);

        Assert.False(result);
        _mockMapper.Verify(m => m.Map(update, existing), Times.Once);
    }
}
