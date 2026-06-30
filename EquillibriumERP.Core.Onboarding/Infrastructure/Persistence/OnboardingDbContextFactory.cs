using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EquillibriumERP.Core.Onboarding.Persistence;

public class OnboardingDbContextFactory : IDesignTimeDbContextFactory<OnboardingDbContext>
{
    public OnboardingDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<OnboardingDbContext>();

        optionsBuilder.UseNpgsql(
            config.GetConnectionString("MasterDatabase") 
            ?? config.GetConnectionString("DefaultConnection"));

        return new OnboardingDbContext(optionsBuilder.Options);
    }
}