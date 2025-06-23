using AutoMapper;
using ExamPortal.Business.Interfaces;
using ExamPortal.Business.Managers;
using Moq;

namespace ExamPortal.Tests.Services;

public class ExamServiceTests
{
    private readonly Mock<IExamRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;

    private readonly ExamService _service;

    public ExamServiceTests()
    {
        _mockRepo = new Mock<IExamRepository>();
        _mockMapper = new Mock<IMapper>();
        _service = new ExamService(_mockRepo.Object, _mockMapper.Object);
    }
    

}
