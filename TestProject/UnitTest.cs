using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSCaddie.Repository.Interfaces;
using MSCaddie.Repository.Models;
using MSCaddie.Repository.Services;

namespace TestProject;

public class UnitTest
{        
        
    private static string connectionString =
            Environment.GetEnvironmentVariable("MSCADDIE_TEST_CONNECTIONSTRING")
            ?? "Server=mssql1.unoeuro.com;Database=vgcms_dk_db;User Id=vgcms_dk;Password=passiv";
    [Fact]
    public async Task GetMatchResultDates_UsesRealServiceAndRepositories()
    {
        // Arrange


        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(
            [
                new KeyValuePair<string, string?>("ConnectionStrings:DefaultConnection", connectionString)
            ])
            .Build();

        IServiceCollection services = new ServiceCollection()
            .AddSingleton<IConfiguration>(configuration)
            .AddLogging()
            .AddRepositoryServices()
            .AddScoped<IMatchService, MatchService>()
            .AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

        ServiceProvider serviceProvider = services.BuildServiceProvider();

        IMatchService service = serviceProvider.GetRequiredService<IMatchService>();

        DateTime startDate = new(2026, 1, 1);
        DateTime endDate = new(2026, 12, 31);

        // Act
        IEnumerable<ListEntryModel>? result = await service.GetMatchResultDates(startDate, endDate);

        // Assert
        Assert.NotNull(result);

        List<ListEntryModel> list = result.ToList();
        Assert.All(list, item =>
        {
            Assert.True(item.Key >= 0);
            Assert.True(item.DateTimeValue.HasValue);
        });
    }
}
