using System.Runtime.InteropServices;
using ExamPortal.API.Models;
using ExamPortal.API.Models.Entities;
using ExamPortal.DataAccess.Contexts;
using ExamPortal.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Answer = ExamPortal.API.Models.Entities.Answer;
using Exam = ExamPortal.API.Models.Entities.Exam;

namespace ExamPortal.Tests.Repositories
{
    public class AnswerRepositoryTests
    {

        private ExamPortalContext GetDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ExamPortalContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
            return new ExamPortalContext(options);
        }
        private AnswerRepository GetRepository(string dbName)
        {
            return new AnswerRepository(GetDbContext(dbName));
        }

        private API.Models.Entities.Answer CreateAnswer(int id = 0, int questionId = 1)
        {
            return new Answer
            {
                Id = id,
                QuestionId = questionId,
                Question = new API.Models.Entities.Question
                {
                    Exam = new Exam
                    {
                    }
                }
            };
        }

        [Fact]
        public async Task CreateAsync_ReturnsTrue_AndAddsAnswer()
        {
            var ctx = GetDbContext("CreateAnswerDb");
            var repo = new AnswerRepository(ctx);
            var answer = CreateAnswer();

            var result = await repo.CreateAsync(answer);

            Assert.True(result);
            Assert.Single(ctx.Answers);
        }

        [Fact]

        public async Task DeleteAnswerAsync_RemovesAnswer_ReturnsTrue()
        {
            var ctx = GetDbContext("DeleteAnswerDb");
            var answer = CreateAnswer();
            ctx.Answers.Add(answer);
            ctx.SaveChanges();

            var repo = new AnswerRepository(ctx);
            var result = await repo.DeleteAnswerAsync(answer.Id);

            Assert.True(result);
            Assert.Empty(ctx.Answers);
        }
        [Fact]
        public async Task DeleteAnswerAsync_ReturnsFalse_WhenNotFound()
        {
            var repo = GetRepository("DeleteAnswerNotFoundDb");

            var result = await repo.DeleteAnswerAsync(999);

            Assert.False(result);
        }
        [Fact]
        public async Task GetAnswerByIdAsnc_ReturnsAnswer_WhenExists()
        {
            var ctx = GetDbContext("GetAnswerByIdDb");
            var answer = CreateAnswer();
            ctx.Answers.Add(answer);
            ctx.SaveChanges();

            var repo = new AnswerRepository(ctx);
            var result = await repo.GetAnswerByIdAsnc(answer.Id);

            Assert.NotNull(result);
            Assert.Equal(answer.Id, result.Id);
        }

        [Fact]
        public async Task GetAnswerByIdAsnc_ReturnsNull_WhenNotFound()
        {
            var repo = GetRepository("GetAnswerByIdNotFoundDb");

            var result = await repo.GetAnswerByIdAsnc(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetPagedAnswersAsync_ReturnsPaged_WhenItemsFound()
        {
            var ctx = GetDbContext("PagedAnswersDb");
            ctx.Answers.AddRange(new[]{
                    CreateAnswer(id:1)
            });
            ctx.SaveChanges();

            var repo = new AnswerRepository(ctx);
            var result = await repo.GetPagedAnswersAsync(1, 1);

            Assert.Equal(1, result.TotalCount);
            Assert.Single(result.Items);

        }

        [Fact]
        public async Task GetPagedAnswersAsync_ReturnsEmpty_WhenNoItems()
        {
            var ctx = GetDbContext("EmptyPagedAnswersDb");

            var repo = new AnswerRepository(ctx);
            var result = await repo.GetPagedAnswersAsync(1, 1);

            Assert.Equal(0, result.TotalCount);
            Assert.Empty(result.Items);
        }

        [Fact]
        public async Task UpdateAnswerAsync_UpdatesAnswer_ReturnsTrue()
        {
            var ctx = GetDbContext("UpdateAnswerDb");
            var answer = CreateAnswer();
            ctx.Answers.Add(answer);
            ctx.SaveChanges();

            var repo = new AnswerRepository(ctx);
            answer.QuestionId = 99;
            var result = await repo.UpdateAnswerAsync(answer);

            Assert.True(result);
            Assert.Equal(99, ctx.Answers.First().QuestionId);
        }
    }
}