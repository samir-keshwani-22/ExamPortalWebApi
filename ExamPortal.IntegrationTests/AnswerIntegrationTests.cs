using ExamPortal.API.Models;
using ExamPortal.IntegrationTests.Helpers;
using Xunit;

namespace ExamPortal.IntegrationTests
{
    public class AnswerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public AnswerIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task AddAnswer_WithValidData_ReturnsCreated()
        {
            var answerCreate = new AnswerCreate
            {
                QuestionId=1,
                
            };
        }
    }
}