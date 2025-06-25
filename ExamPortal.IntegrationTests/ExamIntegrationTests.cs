using System.Net;
using System.Net.Http.Json;
using ExamPortal.API.Models;
using ExamPortal.API.Models.Common;
using ExamPortal.IntegrationTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http.Features;

namespace ExamPortal.IntegrationTests;

public class ExamIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    public ExamIntegrationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task AddExam_WithValidData_ReturnsCreated()
    {
        // Arrange 
        var examCreate = new ExamCreate
        {
            Title = "Integration Test Exam",
            Description = "Sample Exam Description",
            StartDate = DateTime.UtcNow.AddDays(1),
            EndDate = DateTime.UtcNow.AddDays(2),
            DurationMinutes = 90,
            CreatedBy = 1
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/exams", examCreate);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task AddExam_WithMissingTitle_ReturnsBadRequest()
    {
        // Arrange
        var examCreate = new ExamCreate
        {
            Description = "Missing title test",
            StartDate = DateTime.UtcNow.AddDays(1),
            EndDate = DateTime.UtcNow.AddDays(2),
            DurationMinutes = 60,
            CreatedBy = 1
        };

        // Act 
        var response = await _client.PostAsJsonAsync("/api/exams", examCreate);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
 
    [Fact]
    public async Task DeleteExam_WithValidId_ReturnsNoContent()
    {
        var examCreate = new ExamCreate
        {
            Title = "Delete Test",
            Description = "To be deleted",
            StartDate = DateTime.UtcNow.AddDays(1),
            EndDate = DateTime.UtcNow.AddDays(2),
            DurationMinutes = 30,
            CreatedBy = 1
        };

        var addResponse = await _client.PostAsJsonAsync("/api/exams", examCreate);
        addResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var listResponse = await _client.GetAsync("/api/exams?pageIndex=1&pageSize=1&title=Delete Test");
        listResponse.EnsureSuccessStatusCode();
        var paged = await listResponse.Content.ReadFromJsonAsync<PagedResponse<Exam>>();
        var examId = paged?.Items.First().Id;

        var response = await _client.DeleteAsync($"/api/exams/{examId}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteExam_WithInvalidId_ReturnsNotFound()
    {
        var response = await _client.DeleteAsync($"/api/exams/999");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetExamById_WithValidId_ReturnsExam()
    {
        var examCreate = new ExamCreate
        {
            Title = "GetById Test",
            Description = "GetById Desc",
            StartDate = DateTime.UtcNow.AddDays(1),
            EndDate = DateTime.UtcNow.AddDays(2),
            DurationMinutes = 60,
            CreatedBy = 1
        };

        var addResponse = await _client.PostAsJsonAsync("/api/exams", examCreate);
        addResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var listResponse = await _client.GetAsync("/api/exams?pageIndex=1&pageSize=1&title=GetById Test");
        listResponse.EnsureSuccessStatusCode();
        var paged = await listResponse.Content.ReadFromJsonAsync<PagedResponse<Exam>>();
        var examId = paged?.Items.First().Id;

        var response = await _client.GetAsync($"/api/exams/{examId}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var exam = await response.Content.ReadFromJsonAsync<Exam>();
        exam.Should().NotBeNull();
        exam.Title.Should().Be("GetById Test");
    }

    [Fact]
    public async Task GetExamById_WithInvalidId_ReturnsNotFound()
    {
        var response = await _client.GetAsync($"/api/exams/999");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ListExams_WithPagingAndFilter_ReturnsPagedResults()
    {
        var response = await _client.GetAsync($"/api/exams?pageIndex=1&pageSize=5&title=Integration Test Exam");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var paged = await response.Content.ReadFromJsonAsync<PagedResponse<Exam>>();
        paged.Should().NotBeNull();
        paged.Items.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateExam_WithValidId_ReturnsOk()
    {
        var examCreate = new ExamCreate
        {
            Title = "Update Test",
            Description = "Update Desc",
            StartDate = DateTime.UtcNow.AddDays(1),
            EndDate = DateTime.UtcNow.AddDays(2),
            DurationMinutes = 45,
            CreatedBy = 1
        };

        var addResponse = await _client.PostAsJsonAsync("/api/exams", examCreate);
        addResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var listResponse = await _client.GetAsync("/api/exams?pageIndex=1&pageSize=1&title=Update Test");
        listResponse.EnsureSuccessStatusCode();

        var paged = await listResponse.Content.ReadFromJsonAsync<PagedResponse<Exam>>();
        var examId = paged?.Items.First().Id;

        var examUpdate = new ExamUpdate
        {
            Title = "Updated Title"
        };

        var response = await _client.PutAsJsonAsync($"/api/exams/{examId}", examUpdate);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task UpdateExam_WithInvalidId_ReturnsNotFound()
    {
        var examUpdate = new ExamUpdate
        {
            Title = "Should Fail",
            Description = "Should Fail",
            StartDate = DateTime.UtcNow.AddDays(3),
            EndDate = DateTime.UtcNow.AddDays(4),
            DurationMinutes = 50
        };

        var response = await _client.PutAsJsonAsync($"/api/exams/999", examUpdate);
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
