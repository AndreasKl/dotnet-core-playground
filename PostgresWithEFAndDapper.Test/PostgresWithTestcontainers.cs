using System.Threading.Tasks;
using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Configurations.Databases;
using DotNet.Testcontainers.Containers.Modules.Databases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using PostgresWithEFAndDapper.Blog;
using PostgresWithEFAndDapper.Controllers;
using PostgresWithEFAndDapper.Infrastructure;
using Xunit;

namespace PostgresWithEFAndDapper.Test
{
    public class PostgresWithTestcontainers
    {
        public static readonly ILoggerFactory ConsoleLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        
        [Fact]
        public async Task CanTalkToDatabase()
        {
            var builder = new TestcontainersBuilder<PostgreSqlTestcontainer>()
                .WithDatabase(new PostgreSqlTestcontainerConfiguration
                {
                    Database = "postgres",
                    Username = "postgres",
                    Password = "postgres",
                });

            using var container = builder.Build();
            await container.StartAsync();

            await using var connection = new NpgsqlConnection(container.ConnectionString);
            connection.Open();


            var blogContext =
                new BlogContext(
                    new DbContextOptionsBuilder()
                        .EnableSensitiveDataLogging()
                        .UseLoggerFactory(ConsoleLoggerFactory)
                        .UseNpgsql(container.ConnectionString)
                        .UseSnakeCaseNamingConvention().Options
                );
            blogContext.Database.EnsureCreated();

            var controller = new HomeController(
                blogContext,
                new DbConnectionProvider(container.ConnectionString));
            var actionResult = controller.Index("what");
            Assert.IsType<ViewResult>(actionResult);
        }
    }
}