using ExamPortal.API.Models.Entities;
using ExamPortal.Business.Interfaces;
using ExamPortal.DataAccess.Contexts;
using ExamPortal.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;

namespace ExamPortal.Tests.Repositories;

public class ExamRepositoryTests
{
    private ExamPortalContext GetDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ExamPortalContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
        return new ExamPortalContext(options);
    }

    private IExamRepository GetRepository(string dbName)
    {
        return new ExamRepository(GetDbContext(dbName));
    }

    [Fact]
    public async Task CreateAsync_ReturnsTrue_WhenExamAdded()
    {
        var ctx = GetDbContext("CreteExamDb");
        var repo = new ExamRepository(ctx);
        var exam = new Exam
        {
        };
        var result = await repo.CreateAsync(exam);
        Assert.True(result);
        Assert.Single(ctx.Exams);
    }

    // when not added left 

    [Fact]
    public async Task DeleteExamAsync_RemovesExam_ReturnsTrue()
    {
        var context = GetDbContext("DeleteExamDb");
        var exam = new Exam { Title = "ToDelete", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddHours(1) };
        context.Exams.Add(exam);
        context.SaveChanges();

        var repo = new ExamRepository(context);
        var result = await repo.DeleteExamAsync(exam.Id);

        Assert.True(result);
        Assert.Empty(context.Exams);
    }

    [Fact]
    public async Task DeleteExamAsync_ReturnsFalse_WhenExamNotExists()
    {
        var repo = GetRepository("DeleteNonExistingDb");

        var result = await repo.DeleteExamAsync(999);

        Assert.False(result);
    }

    [Fact]
    public async Task GetExamByIdAsync_ReturnsExam_WhenExists()
    {
        var context = GetDbContext("GetByIdDb");
        var exam = new Exam { Title = "Existing", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddHours(2) };
        context.Exams.Add(exam);
        context.SaveChanges();

        var repo = new ExamRepository(context);
        var result = await repo.GetExamByIdAsync(exam.Id);

        Assert.NotNull(result);
        Assert.Equal("Existing", result.Title);
    }

    [Fact]
    public async Task GetExamByIdAsync_ReturnsNull_WhenNotFound()
    {
        var repo = GetRepository("GetByIdNotFoundDb");
        var result = await repo.GetExamByIdAsync(999);
        Assert.Null(result);
    }

    [Fact]
    public async Task GetPagedExamsAsync_ReturnsFilteredPagedResult()
    {
        var context = GetDbContext("PagedDb");
        context.Exams.AddRange(new[]
        {
                new Exam { Title = "Math", StartDate = DateTime.UtcNow.AddDays(1), EndDate = DateTime.UtcNow.AddDays(2), CreatedBy = 1 },
                new Exam { Title = "Science", StartDate = DateTime.UtcNow.AddDays(3), EndDate = DateTime.UtcNow.AddDays(4), CreatedBy = 1 },
                new Exam { Title = "History", StartDate = DateTime.UtcNow.AddDays(5), EndDate = DateTime.UtcNow.AddDays(6), CreatedBy = 2 }
        });
        context.SaveChanges();

        var repo = new ExamRepository(context);
        var result = await repo.GetPagedExamsAsync(1, 2, "S", null, null, null, null, 1);
        Assert.Equal(1, result.TotalCount);
        Assert.Single(result.Items);
        Assert.Equal("Science", result.Items.First().Title);
    }

    [Fact]
    public async Task GetPagedExamsAsync_ReturnsEmpty_WhenNoMatch()
    {
        var context = GetDbContext("EmptyPagedDb");
        context.Exams.Add(new Exam { Title = "Math", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddHours(1), CreatedBy = 1 });
        context.SaveChanges();

        var repo = new ExamRepository(context);
        var result = await repo.GetPagedExamsAsync(1, 10, "Physics", null, null, null, null, null);
        Assert.Equal(0, result.TotalCount);
        Assert.Empty(result.Items);
    }

    [Fact]
    public async Task UpdateExamAsync_UpdatesExam_ReturnsTrue()
    {
        var context = GetDbContext("UpdateDb");
        var exam = new Exam { Title = "Old", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddHours(1) };
        context.Exams.Add(exam);
        context.SaveChanges();

        var repo = new ExamRepository(context);
        exam.Title = "New";
        var result = await repo.UpdateExamAsync(exam);
        Assert.True(result);
        Assert.Equal("New", context.Exams.First().Title);
        
    }


}
