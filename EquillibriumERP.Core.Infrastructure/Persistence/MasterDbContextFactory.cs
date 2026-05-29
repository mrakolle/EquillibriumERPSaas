using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace EquillibriumERP.Core.Infrastructure.Persistence;

public class MasterDbContextFactory
    : IDesignTimeDbContextFactory<MasterDbContext>
{
    public MasterDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables()
            .Build();

        var connectionString =
            configuration.GetConnectionString("MasterDatabase");

        var optionsBuilder =
            new DbContextOptionsBuilder<MasterDbContext>();

        optionsBuilder.UseNpgsql(connectionString);

        return new MasterDbContext(optionsBuilder.Options);
    }
}