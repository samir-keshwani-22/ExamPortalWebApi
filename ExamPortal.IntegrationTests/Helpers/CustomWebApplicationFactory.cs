using ExamPortal.API;
using ExamPortal.DataAccess.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace ExamPortal.IntegrationTests.Helpers;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ExamPortalContext>));
            // if (descriptor != null)
            // {
            //     services.Remove(descriptor);
            // }

            // services.AddDbContext<ExamPortalContext>(options =>
            // {
            //     options.UseInMemoryDatabase("InMemoryTestDb");
            // });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ExamPortalContext>();
            db.Database.EnsureCreated();
        });
    }

}
