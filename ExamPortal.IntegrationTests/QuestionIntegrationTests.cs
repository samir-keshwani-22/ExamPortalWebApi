using System.Net;
using System.Net.Http.Json;
using ExamPortal.API.Models;
using ExamPortal.API.Models.Common;
using ExamPortal.IntegrationTests.Helpers;
using FluentAssertions;

namespace ExamPortal.IntegrationTests
{
    public class QuestionIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public QuestionIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }
        private async Task<int> CreateExamAsync()
        {
            var exam = new ExamCreate
            {
                Title = "TestExam",
                Description = "TestDesc",
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                DurationMinutes = 45,
                CreatedBy = 1
            };
            var response = await _client.PostAsJsonAsync("/api/exams", exam);
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var listResponse = await _client.GetAsync("/api/exams?pageIndex=1&pageSize=1&title=TestExam");
            var paged = await listResponse.Content.ReadFromJsonAsync<PagedResponse<Exam>>();
            return paged!.Items.First().Id;
        }

        [Fact]
        public async Task AddQuestion_WithValidData_ReturnsCreated()
        {
            var examId = await CreateExamAsync();
            var question = new QuestionCreate
            {
                ExamId = examId,
                QuestionText = "What is 1+1?",
                QuestionType = "SingleChoice",
                Marks = 1,
                Topic = "Math",
                DifficultyLevel = "Easy",
                CreatedBy = 1
            };
            var response = await _client.PostAsJsonAsync("/api/questions", question);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task AddQuestion_WithMissingQuestionText_ReturnsBadRequest()
        {
            var examId = await CreateExamAsync();
            var question = new QuestionCreate
            {
                ExamId = examId,
                QuestionType = "SingleChoice",
                Marks = 1,
                Topic = "Math",
                DifficultyLevel = "Easy",
                CreatedBy = 1
            };

            var response = await _client.PostAsJsonAsync("/api/questions", question);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task GetQuestionById_WithValidId_ReturnsQuestion()
        {
            var examId = await CreateExamAsync();
            var question = new QuestionCreate
            {
                ExamId = examId,
                QuestionText = "GetByIdTest",
                QuestionType = "SingleChoice",
                Marks = 2,
                Topic = "Science",
                DifficultyLevel = "Medium",
                CreatedBy = 1
            };
            await _client.PostAsJsonAsync("/api/questions", question);

            var listResponse = await _client.GetAsync("api/questions?pageIndex=1&pageSize=5&questionText=GetByIdTest");
            var paged = await listResponse.Content.ReadFromJsonAsync<PagedResponse<Question>>();
            var qId = paged!.Items.First(x => x.QuestionText == "GetByIdTest").Id;

            var response = await _client.GetAsync($"/api/questions/{qId}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var returned = await response.Content.ReadFromJsonAsync<Question>();
            returned.Should().NotBeNull();
            returned.QuestionText.Should().Be("GetByIdTest");



        }

        [Fact]
        public async Task GetQuestionById_WithInvalidId_ReturnsNotFound()
        {
            var response = await _client.GetAsync("/api/questions/999");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ListQuestions_WithPaging_ReturnsPagedResults()
        {
            var response = await _client.GetAsync("/api/questions?pageIndex=1&pageSize=5");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var paged = await response.Content.ReadFromJsonAsync<PagedResponse<Question>>();
            paged.Should().NotBeNull();
            paged.Items.Should().NotBeNull();
        }

        [Fact]
        public async Task UpdateQuestion_WithValidId_ReturnsOk()
        {
            var examId = await CreateExamAsync();
            var question = new QuestionCreate
            {
                ExamId = examId,
                QuestionText = "UpdateTest",
                QuestionType = "SingleChoice",
                Marks = 2,
                Topic = "UpdateTopic",
                DifficultyLevel = "Medium",
                CreatedBy = 1
            };
            await _client.PostAsJsonAsync("/api/questions", question);

            var listResponse = await _client.GetAsync("/api/questions?pageIndex=1&pageSize=10&questionText=UpdateTest");
            var paged = await listResponse.Content.ReadFromJsonAsync<PagedResponse<Question>>();
            var qId = paged!.Items.First(x => x.QuestionText == "UpdateTest").Id;

            var update = new QuestionUpdate
            {
                ExamId = examId,
                QuestionText = "UpdatedText",
                QuestionType = "SingleChoice",
                Marks = 5,
                Topic = "UpdatedTopic",
                DifficultyLevel = "Hard",
                UpdatedBy = 2
            };

            var response = await _client.PutAsJsonAsync($"/api/questions/{qId}", update);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task UpdateQuestion_WithInvalidId_ReturnsNotFound()
        {
            var examId = await CreateExamAsync();
            var update = new QuestionUpdate
            {
                ExamId = examId,
                QuestionText = "NoSuchQuestion",
                QuestionType = "SingleChoice",
                Marks = 2,
                Topic = "Math",
                DifficultyLevel = "Easy",
                UpdatedBy = 1
            };
            var response = await _client.PutAsJsonAsync("/api/questions/99999", update);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }


        [Fact]
        public async Task UpdateQuestion_WithMissingExamId_ReturnsBadRequest()
        {
            var examId = await CreateExamAsync();
            var question = new QuestionCreate
            {
                ExamId = examId,
                QuestionText = "UpdateBadReqTest",
                QuestionType = "SingleChoice",
                Marks = 1,
                Topic = "BadReq",
                DifficultyLevel = "Easy",
                CreatedBy = 1
            };
            await _client.PostAsJsonAsync("/api/questions", question);

            var listResponse = await _client.GetAsync("/api/questions?pageIndex=1&pageSize=10&questionText=UpdateBadReqTest");
            var paged = await listResponse.Content.ReadFromJsonAsync<PagedResponse<Question>>();
            var qId = paged!.Items.First(x => x.QuestionText == "UpdateBadReqTest").Id;

            var update = new QuestionUpdate
            {
                ExamId = 0,
                QuestionText = "ShouldFail",
                QuestionType = "SingleChoice",
                Marks = 1,
                Topic = "BadReq",
                DifficultyLevel = "Easy",
                UpdatedBy = 1
            };

            var response = await _client.PutAsJsonAsync($"/api/questions/{qId}", update);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteQuestion_WithValidId_ReturnsNoContent()
        {
            var examId = await CreateExamAsync();
            var question = new QuestionCreate
            {
                ExamId = examId,
                QuestionText = "ToBeDeleted",
                QuestionType = "SingleChoice",
                Marks = 1,
                Topic = "Delete",
                DifficultyLevel = "Easy",
                CreatedBy = 1
            };
            await _client.PostAsJsonAsync("/api/questions", question);
            var listResponse = await _client.GetAsync("/api/questions?pageIndex=1&pageSize=10&questionText=ToBeDeleted");
            var paged = await listResponse.Content.ReadFromJsonAsync<PagedResponse<Question>>();
            var qId = paged!.Items.First(x => x.QuestionText == "ToBeDeleted").Id;

            var response = await _client.DeleteAsync($"/api/questions/{qId}");
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteQuestion_WithInvalidId_ReturnsNotFound()
        {
            var response = await _client.DeleteAsync("/api/questions/99999");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}