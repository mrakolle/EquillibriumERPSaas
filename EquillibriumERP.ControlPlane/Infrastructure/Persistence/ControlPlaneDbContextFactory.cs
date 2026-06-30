using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EquillibriumERP.ControlPlane.Infrastructure.Persistence;

public class ControlPlaneDbContextFactory : IDesignTimeDbContextFactory<ControlPlaneDbContext>
{
    public ControlPlaneDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ControlPlaneDbContext>();

        optionsBuilder.UseNpgsql(
            config.GetConnectionString("MasterDatabase") 
            ?? config.GetConnectionString("DefaultConnection"));

        return new ControlPlaneDbContext(optionsBuilder.Options);
    }
}