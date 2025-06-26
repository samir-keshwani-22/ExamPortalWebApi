using System.ComponentModel.Design.Serialization;
using System.Net;
using System.Net.Http.Json;
using System.Net.Sockets;
using ExamPortal.API.Models;
using ExamPortal.API.Models.Common;
using ExamPortal.IntegrationTests.Helpers;
using FluentAssertions;
using NetTopologySuite.Operation.Valid;
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
                QuestionId = 10,
                // doubt 
                SelectedOptionId = 2
            };
            var response = await _client.PostAsJsonAsync("/api/answers", answerCreate);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        // [Fact]
        // public async Task AddAnswer_WithMissingQuestionId_ReturnsBadRequest()
        // {
        //     var answerCreate = new AnswerCreate
        //     {
        //         // doubt 
        //         SelectedOptionId = 2
        //     };
        //     var response = await _client.PostAsJsonAsync("/api/answers", answerCreate);
     

        //     response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        // }

        [Fact]
        public async Task DeleteAnswer_WithValidId_ReturnsNoContent()
        {
            var answerCreate = new AnswerCreate { QuestionId = 1, SelectedOptionId = 2 };
            var addResponse = await _client.PostAsJsonAsync("/api/answers", answerCreate);
            addResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            // Find ID
            var listResponse = await _client.GetAsync("/api/answers?pageIndex=1&pageSize=1");
            listResponse.EnsureSuccessStatusCode();
            var paged = await listResponse.Content.ReadFromJsonAsync<PagedResponse<Answer>>();
            var answerId = paged?.Items.First().Id;

            var response = await _client.DeleteAsync($"/api/answers/{answerId}");
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        }

        [Fact]
        public async Task DeleteAnswer_WithInvalidId_ReturnsNotFound()
        {
            var response = await _client.DeleteAsync($"/api/answers/999");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAnswerById_WithValidId_ReturnsAnswer()
        {
            var answerCreate = new AnswerCreate
            {
                QuestionId = 1,
                SelectedOptionId = 2
            };
            var addResponse = await _client.PostAsJsonAsync("/api/answers", answerCreate);
            addResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var listResponse = await _client.GetAsync("/api/answers?pageIndex=1&pageSize=1");
            listResponse.EnsureSuccessStatusCode();
            var paged = await listResponse.Content.ReadFromJsonAsync<PagedResponse<Answer>>();
            var answerId = paged?.Items.First().Id;

            var response = await _client.GetAsync($"/api/answers/{answerId}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var answer = await response.Content.ReadFromJsonAsync<Answer>();
            answer.Should().NotBeNull();
            answer.QuestionId.Should().Be(1);
            answer.SelectedOptionId.Should().Be(2);
        }

        [Fact]
        public async Task GetAnswerById_WithInvalidId_ReturnsNotFound()
        {
            var response = await _client.GetAsync($"/api/answers/999");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ListAnswers_WithPaging_ReturnsPagedResults()
        {
            var response = await _client.GetAsync("/api/answers?pageIndex=1&pageSize=5");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var paged = await response.Content.ReadFromJsonAsync<PagedResponse<Answer>>();
            paged.Should().NotBeNull();
            paged.Items.Should().NotBeNull();
        }

        [Fact]
        public async Task UpdateAnswer_WithValidId_ReturnsOk()
        {
            var answerCreate = new AnswerCreate
            {
                QuestionId = 1,
                SelectedOptionId = 2
            };
            var addResponse = await _client.PostAsJsonAsync("/api/answers", answerCreate);
            addResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var listResponse = await _client.GetAsync("/api/answers?pageIndex=1&pageSize=1");
            listResponse.EnsureSuccessStatusCode();
            var paged = await listResponse.Content.ReadFromJsonAsync<PagedResponse<Answer>>();
            var answerId = paged?.Items.First().Id;

            var answerUpdate = new AnswerUpdate
            {
                QuestionId = 1,
                SelectedOptionId = 3
            };

            var response = await _client.PutAsJsonAsync($"/api/answers/{answerId}", answerUpdate);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task UpdateAnswer_WithInvalidId_ReturnsNotFound()
        {
            var response = await _client.PutAsJsonAsync($"/api/answers/999", new AnswerUpdate { QuestionId = 1, SelectedOptionId = 3 });
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }


        [Fact]
        public async Task UpdateAnswer_WithMissingQuestionId_ReturnsBadRequest()
        {
            var answerCreate = new AnswerCreate { QuestionId = 1, SelectedOptionId = 2 };
            var addResponse = await _client.PostAsJsonAsync("/api/answers", answerCreate);
            addResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var listResponse = await _client.GetAsync("/api/answers?pageIndex=1&pageSize=1");
            listResponse.EnsureSuccessStatusCode();
            var paged = await listResponse.Content.ReadFromJsonAsync<PagedResponse<Answer>>();
            var answerId = paged?.Items.First().Id;

            var answerUpdate = new AnswerUpdate
            {
                QuestionId = 0,
                SelectedOptionId = 3
            };

            var response = await _client.PutAsJsonAsync($"/api/answers/{answerId}", answerUpdate);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}