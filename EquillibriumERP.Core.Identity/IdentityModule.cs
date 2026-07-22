using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using EquillibriumERP.Core.Abstractions;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Abstractions.Identity;
using EquillibriumERP.Core.Abstractions.Modules;
using EquillibriumERP.Core.Identity.Endpoints;
using EquillibriumERP.Core.Identity.Infrastructure;
using EquillibriumERP.Core.Identity.Auth;
using EquillibriumERP.Core.Identity.Services;
using EquillibriumERP.Core.Identity.Domain.Entities;
using EquillibriumERP.Core.Identity.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using EquillibriumERP.Core.Abstractions.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EquillibriumERP.Core.Identity;

public class IdentityModule : IModule
{
    public string Name => "Identity";

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<IdentityDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("TenantDatabase")));

        services
            .AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        services.AddScoped<IUserService, UserService>();

        services.AddScoped<ITenantAuthenticationService, TenantAuthenticationService>();
        services.AddScoped<JwtTokenService>();

        services.Configure<JwtOptions>(config.GetSection("Jwt"));
        services.AddScoped<
                    IPermissionService,
                    UserPermissionService>();
        services.AddSingleton<IModulePermissionProvider, IdentityPermissionProvider>();
        services.AddScoped<IdentitySeeder>();

    }

    public void RegisterModel(ModelBuilder modelBuilder)
    {
        // handled by DbContext configurations
    }

    public void MapEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/")
            .WithTags("Authentication");

        AuthEndpoints.MapAuthEndpoints(group);
    }

    public async Task MigrateAsync(
        IServiceProvider services,
        string schema,
        CancellationToken cancellationToken)
    {
        var db = services.GetRequiredService<IdentityDbContext>();

        await db.Database.GetDbConnection().OpenAsync(cancellationToken);

        await db.Database.ExecuteSqlRawAsync(
            $"SET search_path TO \"{schema}\", public",
            cancellationToken);

        await db.Database.MigrateAsync(cancellationToken);
        
        // SEED AFTER MIGRATION
        var seeder = services.GetRequiredService<IdentitySeeder>();

       await seeder.SeedAsync(
            schema: null,
            ct: cancellationToken);
    }
}