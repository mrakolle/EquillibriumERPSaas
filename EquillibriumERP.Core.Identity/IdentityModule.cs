using System.Data;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EquillibriumERP.Core.Abstractions.Modules;
using EquillibriumERP.Core.Identity.Application.Services;
using EquillibriumERP.Core.Identity.Domain.Entities;
using EquillibriumERP.Core.Identity.Endpoints;
using EquillibriumERP.Core.Identity.Application;
using EquillibriumERP.Core.Identity.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace EquillibriumERP.Core.Identity;

public class IdentityModule : IModule
{
    public string Name => "Identity";

    public void RegisterServices(
        IServiceCollection services,
        IConfiguration config)
    {
        Console.WriteLine("Registering IdentityModule services...");

        services.AddSingleton<IModuleModelBuilder,
            IdentityModelBuilder>();

        services.AddDbContext<IdentityDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("TenantDatabase")!));

        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<AuthService>();
        services.AddScoped<PermissionEngine>();
        services.AddScoped<RoleService>();
        services.AddScoped<UserService>();

        services.Configure<JwtOptions>(config.GetSection("Jwt"));
    }

    public void RegisterModel(ModelBuilder modelBuilder)
    {
    }

    public async Task MigrateAsync(
        IServiceProvider services,
        string schema,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var db = services.GetRequiredService<IdentityDbContext>();

        var connection = db.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync(cancellationToken);

        await db.Database.ExecuteSqlRawAsync(
            $"SET search_path TO \"{schema}\", public",
            cancellationToken);

        await db.Database.MigrateAsync(cancellationToken);
    }

    public void MapEndpoints(WebApplication app)
    {
        Console.WriteLine("Mapping IdentityModule endpoints...222");
        AuthEndpoints.MapAuthEndpoints(app);
        RoleEndpoints.MapRoleEndpoints(app);
        //PermissionEndpoints.MapPermissionEndpoints(app);
        UserEndpoints.MapUserEndpoints(app);
    }
}