using System.Net;
using System.Net.Http.Json;
using System.Reflection.Emit;
using ExamPortal.API.Models;
using ExamPortal.API.Models.Common;
using ExamPortal.IntegrationTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http.Features;

namespace ExamPortal.IntegrationTests
{
    public class QuestionOptionIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public QuestionOptionIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }


        [Fact]
        public async Task AddQuestionOption_WithValidData_ReturnsCreated()
        {
            var optionCreate = new QuestionOptionCreate
            {
                QuestionId = 1,
                OptionText = "Option 1",
                IsCorrect = true,
                CreatedBy = 1
            };

            var response = await _client.PostAsJsonAsync("/api/question-options", optionCreate);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task AddQuestionOption_WithMissingOptionText_ReturnsBadRequest()
        {
            var optionCreate = new QuestionOptionCreate
            {
                QuestionId = 1,
                IsCorrect = true,
                CreatedBy = 1
            };

            var response = await _client.PostAsJsonAsync("/api/question-options", optionCreate);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteQuestionOption_WithValidId_ReturnsNoContent()
        {
            var optionCreate = new QuestionOptionCreate
            {
                QuestionId = 1,
                OptionText = "To be deleted ",
                IsCorrect = true,
                CreatedBy = 1
            };

            var response = await _client.PostAsJsonAsync("/api/question-options", optionCreate);
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var listResponse = await _client.GetAsync("/api/question-options?pageIndex=1&pageSize=1");
            listResponse.EnsureSuccessStatusCode();
            var paged = await listResponse.Content.ReadFromJsonAsync<PagedResponse<QuestionOption>>();
            var optionId = paged?.Items.First().Id;

            var responseDelete = await _client.DeleteAsync($"/api/question-options/{optionId}");

            responseDelete.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteQuestionOption_WithInvalidId_ReturnsNotFound()
        {
            var response = await _client.DeleteAsync("/api/question-options/999");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        // [Fact]
        // public async Task GetQuestionOptionById_WithValidId_ReturnsOption()
        // {
        //     var optionCreate = new QuestionOptionCreate
        //     {
        //         QuestionId = 12,
        //         OptionText = "GetById Test",
        //         IsCorrect = false,
        //         CreatedBy = 1
        //     };
        //     var addResponse = await _client.PostAsJsonAsync("/api/question-options", optionCreate);
        //     addResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        //     var listResponse = await _client.GetAsync("/api/question-options?pageIndex=1&pageSize=1");
        //     listResponse.EnsureSuccessStatusCode();
        //     var paged = await listResponse.Content.ReadFromJsonAsync<PagedResponse<QuestionOption>>();

        //     var createdOption = paged?.Items.FirstOrDefault(x => x.OptionText == "GetById Test");
        //     createdOption.Should().NotBeNull();
        //     var optionId = createdOption.Id;

        //     var response = await _client.GetAsync($"/api/question-options/{optionId}");
        //     response.StatusCode.Should().Be(HttpStatusCode.OK);

        //     var option = await response.Content.ReadFromJsonAsync<QuestionOption>();
        //     option.Should().NotBeNull();
        //     option.QuestionId.Should().Be(12); 
        // }

        [Fact]
        public async Task GetQuestionOptionById_WithInvalidId_ReturnsNotFound()
        {
            var response = await _client.GetAsync("/api/question-options/999");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ListQuestionOptions_WithPaging_ReturnsPagedResults()
        {
            var response = await _client.GetAsync("/api/question-options?pageIndex=1&pageSize=5");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var paged = await response.Content.ReadFromJsonAsync<PagedResponse<QuestionOption>>();
            paged.Should().NotBeNull();
            paged.Items.Should().NotBeNull();
        }

        [Fact]
        public async Task UpdateQuestionOption_WithValidId_ReturnsOk()
        {
            var optionCreate = new QuestionOptionCreate
            {
                QuestionId = 1,
                OptionText = "Update Test",
                IsCorrect = false,
                CreatedBy = 1
            };
            var addResponse = await _client.PostAsJsonAsync("/api/question-options", optionCreate);
            addResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var listResponse = await _client.GetAsync("/api/question-options?pageIndex=1&pageSize=1");
            listResponse.EnsureSuccessStatusCode();
            var paged = await listResponse.Content.ReadFromJsonAsync<PagedResponse<QuestionOption>>();
            var optionId = paged?.Items.First().Id;

            var optionUpdate = new QuestionOptionUpdate
            {
                QuestionId = 1,
                OptionText = "Updated Option",
                IsCorrect = true,
                UpdatedBy = 2
            };

            var response = await _client.PutAsJsonAsync($"/api/question-options/{optionId}", optionUpdate);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task UpdateQuestionOption_WithInvalidId_ReturnsNotFound()
        {
            var optionUpdate = new QuestionOptionUpdate
            {
                QuestionId = 1,
                OptionText = "Should Not Update",
                IsCorrect = false,
                UpdatedBy = 2
            };

            var response = await _client.PutAsJsonAsync($"/api/question-options/999", optionUpdate);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateQuestionOption_WithMissingQuestionId_ReturnsBadRequest()
        {
            var optionCreate = new QuestionOptionCreate
            {
                QuestionId = 1,
                OptionText = "Update Test",
                IsCorrect = false,
                CreatedBy = 1
            };
            var addResponse = await _client.PostAsJsonAsync("/api/question-options", optionCreate);
            addResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var listResponse = await _client.GetAsync("/api/question-options?pageIndex=1&pageSize=1");
            listResponse.EnsureSuccessStatusCode();
            var paged = await listResponse.Content.ReadFromJsonAsync<PagedResponse<QuestionOption>>();
            var optionId = paged?.Items.First().Id;

            var optionUpdate = new QuestionOptionUpdate
            {
                QuestionId = 0,
                OptionText = "Updated Option",
                IsCorrect = true,
                UpdatedBy = 2
            };

            var response = await _client.PutAsJsonAsync($"/api/question-options/{optionId}", optionUpdate);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}