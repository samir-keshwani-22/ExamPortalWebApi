using ExamPortal.API.Models.Entities;
using ExamPortal.DataAccess.Contexts;
using ExamPortal.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExamPortal.Tests.Repositories
{
    public class QuestionRepositoryTests
    {
        private ExamPortalContext GetDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ExamPortalContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new ExamPortalContext(options);
        }

        private QuestionRepository GetRepository(string dbName)
        {
            return new QuestionRepository(GetDbContext(dbName));
        }
        private Question CreateQuestion(int id = 0, int examId = 1)
        {
            return new Question
            {
                Id = id,
                ExamId = examId,
                Exam = new Exam { Id = examId, Title = "Sample Exam" },
                QuestionText = $"Text {id}",
                QuestionType = "MCQ"
            };
        }

        [Fact]
        public async Task CreateAsync_ReturnsTrue_AndAddsQuestion()
        {
            var ctx = GetDbContext("CreateQuestionDb");
            var repo = new QuestionRepository(ctx);
            var question = CreateQuestion();

            var result = await repo.CreateAsync(question);

            Assert.True(result);
            Assert.Single(ctx.Questions);
        }

        [Fact]
        public async Task DeleteQuestionAsync_RemovesQuestion_ReturnsTrue()
        {
            var ctx = GetDbContext("DeleteQuestionDb");
            var question = CreateQuestion();
            ctx.Questions.Add(question);
            ctx.SaveChanges();

            var repo = new QuestionRepository(ctx);
            var result = await repo.DeleteQuestionAsync(question.Id);

            Assert.True(result);
            Assert.Empty(ctx.Questions);
        }

        [Fact]
        public async Task DeleteQuestionAsync_ReturnsFalse_WhenNotFound()
        {
            var repo = GetRepository("DeleteQuestionNotFoundDb");

            var result = await repo.DeleteQuestionAsync(999);

            Assert.False(result);
        }

        [Fact]
        public async Task GetQuestionByIdAsync_ReturnsQuestion_WhenExists()
        {
            var ctx = GetDbContext("GetQuestionByIdDb");
            var question = CreateQuestion();
            ctx.Questions.Add(question);
            ctx.SaveChanges();

            var repo = new QuestionRepository(ctx);
            var result = await repo.GetQuestionByIdAsync(question.Id);

            Assert.NotNull(result);
            Assert.Equal(question.Id, result.Id);
        }

        [Fact]
        public async Task GetQuestionByIdAsync_ReturnsNull_WhenNotFound()
        {
            var repo = GetRepository("GetQuestionByIdNotFoundDb");

            var result = await repo.GetQuestionByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetPagedQuestionsAsync_ReturnsPaged_WhenItemsFound()
        {
            var ctx = GetDbContext("PagedQuestionsDb");
            ctx.Questions.AddRange(new[]
            {
                CreateQuestion(id: 1)
            });
            ctx.SaveChanges();

            var repo = new QuestionRepository(ctx);
            var result = await repo.GetPagedQuestionsAsync(1, 1);

            Assert.Equal(1, result.TotalCount);
            Assert.Single(result.Items); 
        }

        [Fact]
        public async Task GetPagedQuestionsAsync_ReturnsEmpty_WhenNoItems()
        {
            var ctx = GetDbContext("EmptyPagedQuestionsDb");

            var repo = new QuestionRepository(ctx);
            var result = await repo.GetPagedQuestionsAsync(1, 10);

            Assert.Equal(0, result.TotalCount);
            Assert.Empty(result.Items);
        }

        [Fact]
        public async Task UpdateQuestionAsync_UpdatesQuestion_ReturnsTrue()
        {
            var ctx = GetDbContext("UpdateQuestionDb");
            var question = CreateQuestion();
            ctx.Questions.Add(question);
            ctx.SaveChanges();

            var repo = new QuestionRepository(ctx);
            question.QuestionText = "Updated";
            var result = await repo.UpdateQuestionAsync(question);

            Assert.True(result);
            Assert.Equal("Updated", ctx.Questions.First().QuestionText);
        }

    }
}