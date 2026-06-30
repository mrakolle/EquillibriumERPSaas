using System.Net;
using EquillibriumERP.Core.Abstractions;
using EquillibriumERP.Core.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EquillibriumERP.LIMS;

public class LIMSModule : IModule
{
    public string Name => throw new NotImplementedException();

    public void MapEndpoints(WebApplication app)
    {
        //throw new NotImplementedException();
    }

    public Task MigrateAsync(IServiceProvider services, string schema, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void RegisterModel(ModelBuilder modelBuilder)
    {
        //throw new NotImplementedException();
    }

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        //throw new NotImplementedException();
    }
}
