using ExamPortal.API.Models.Entities;
using ExamPortal.DataAccess.Contexts;
using ExamPortal.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExamPortal.Tests.Repositories
{
    public class QuestionOptionRepositoryTests
    {
        private ExamPortalContext GetDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ExamPortalContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new ExamPortalContext(options);
        }

        private QuestionOptionRepository GetRepository(string dbName)
        {
            return new QuestionOptionRepository(GetDbContext(dbName));
        }

        private QuestionOption CreateOption(int id = 0, int questionId = 1)
        {
            return new QuestionOption
            {
                Id = id,
                QuestionId = questionId,
            };
        }

        [Fact]
        public async Task CreateAsync_ReturnsTrue_AndAddsOption()
        {
            var ctx = GetDbContext("CreateOptionDb");
            var repo = new QuestionOptionRepository(ctx);
            var option = CreateOption();

            var result = await repo.CreateAsync(option);

            Assert.True(result);
            Assert.Single(ctx.QuestionOptions);
        }

        [Fact]
        public async Task DeleteAsync_RemovesOption_ReturnsTrue()
        {
            var ctx = GetDbContext("DeleteOptionDb");
            var option = CreateOption();
            ctx.QuestionOptions.Add(option);
            ctx.SaveChanges();

            var repo = new QuestionOptionRepository(ctx);
            var result = await repo.DeleteAsync(option.Id);

            Assert.True(result);
            Assert.Empty(ctx.QuestionOptions);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalse_WhenNotFound()
        {
            var repo = GetRepository("DeleteOptionNotFoundDb");

            var result = await repo.DeleteAsync(999);

            Assert.False(result);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsOption_WhenExists()
        {
            var ctx = GetDbContext("GetOptionByIdDb");
            var option = CreateOption();
            ctx.QuestionOptions.Add(option);
            ctx.SaveChanges();

            var repo = new QuestionOptionRepository(ctx);
            var result = await repo.GetByIdAsync(option.Id);

            Assert.NotNull(result);
            Assert.Equal(option.Id, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            var repo = GetRepository("GetOptionByIdNotFoundDb");

            var result = await repo.GetByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetPagedAsync_ReturnsPaged_WhenItemsFound()
        {
            var ctx = GetDbContext("PagedOptionsDb");
            ctx.QuestionOptions.AddRange(new[]
            {
                CreateOption(id: 1)
            });
            ctx.SaveChanges();

            var repo = new QuestionOptionRepository(ctx);
            var result = await repo.GetPagedAsync(1, 2);

            Assert.Equal(1, result.TotalCount);
            Assert.Single(result.Items);
        }

        [Fact]
        public async Task GetPagedAsync_ReturnsEmpty_WhenNoItems()
        {
            var ctx = GetDbContext("EmptyPagedOptionsDb");

            var repo = new QuestionOptionRepository(ctx);
            var result = await repo.GetPagedAsync(1, 10);

            Assert.Equal(0, result.TotalCount);
            Assert.Empty(result.Items);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesOption_ReturnsTrue()
        {
            var ctx = GetDbContext("UpdateOptionDb");
            var option = CreateOption();
            ctx.QuestionOptions.Add(option);
            ctx.SaveChanges();

            var repo = new QuestionOptionRepository(ctx);
            option.QuestionId = 99; 
            var result = await repo.UpdateAsync(option);

            Assert.True(result);
            Assert.Equal(99, ctx.QuestionOptions.First().QuestionId);
        }
    }
}