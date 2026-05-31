using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using EquillibriumERP.Core.Api.Middleware;
using EquillibriumERP.Core.Infrastructure.DependencyInjection;
using EquillibriumERP.Core.Abstractions.Modules;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Infrastructure.MultiTenancy;
using EquillibriumERP.ControlPlane.Endpoints;
using EquillibriumERP.ControlPlane.DependencyInjection;
using EquillibriumERP.Core.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using EquillibriumERP.Core.Infrastructure.Authorization;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// =====================================================
// CORE SERVICES
// =====================================================

builder.Services.AddControlPlane();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<JwtTokenService>();

// =====================================================
// SWAGGER
// =====================================================

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EquillibriumERP API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// =====================================================
// JWT AUTHENTICATION
// =====================================================

var jwt = configuration.GetSection("Jwt");

var key = Encoding.UTF8.GetBytes(jwt["Key"]!);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwt["Issuer"],
                ValidAudience = jwt["Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(key)
            };
    });

builder.Services.AddAuthorization();

builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

// =====================================================
// VALIDATION
// =====================================================

builder.Services.AddFluentValidationAutoValidation();

// =====================================================
// INFRASTRUCTURE
// =====================================================

builder.Services.AddInfrastructure(configuration);

// =====================================================
// TENANT SESSION
// =====================================================

builder.Services.AddScoped<ITenantSession, TenantSession>();

// =====================================================
// MODULE DISCOVERY
// =====================================================

var moduleInterface = typeof(IModule);

var modules = AppDomain.CurrentDomain
    .GetAssemblies()
    .SelectMany(a =>
    {
        try
        {
            return a.GetTypes();
        }
        catch
        {
            return Array.Empty<Type>();
        }
    })
    .Where(t =>
        moduleInterface.IsAssignableFrom(t) &&
        !t.IsInterface &&
        !t.IsAbstract)
    .Select(t => Activator.CreateInstance(t))
    .OfType<IModule>()
    .ToList();

// =====================================================
// MODULE SERVICE REGISTRATION
// =====================================================

foreach (var module in modules)
{
    module.RegisterServices(
        builder.Services,
        configuration
    );
}

// =====================================================
// BUILD
// =====================================================

var app = builder.Build();

// =====================================================
// PIPELINE
// =====================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EquillibriumERP API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseMiddleware<TenantMiddleware>();

app.UseAuthorization();
// =====================================================
// SHARED ENDPOINTS
// =====================================================

//app.MapAuthEndpoints();
//app.MapUserEndpoints();
AuthEndpoints.MapAuthEndpoints(app);
UserEndpoints.MapUserEndpoints(app);
app.MapTenantProvisioningEndpoints();
PermissionEndpoints.MapPermissionEndpoints(app);
RoleEndpoints.MapRoleEndpoints(app);

// =====================================================
// MODULE ENDPOINTS
// =====================================================

foreach (var module in modules)
{
    module.MapEndpoints(app);
}
// =====================================================
// DEBUG ENDPOINTS  to be move to ControlPlane for production use
// =====================================================


// =====================================================
// RUN - Launch the application
// =====================================================
app.Run();

/*
using EquillibriumERP.Core.Api.Middleware;
using EquillibriumERP.Core.Api.Endpoints;
using EquillibriumERP.Core.Infrastructure.DependencyInjection;
using EquillibriumERP.Core.Abstractions.Modules;
using EquillibriumERP.Sales.Infrastructure;
using EquillibriumERP.Inventory.Infrastructure;
using EquillibriumERP.Manufacturing.Infrastructure;
//using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();

var modules = new IModule[]
{
    new SalesModule(),
    new InventoryModule(),
    new ManufacturingModule()
};

foreach (var m in modules)
{
    m.RegisterServices(builder.Services, builder.Configuration);
    builder.Services.AddSingleton(m);
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<TenantMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapTenantOnboardingEndpoints();
/*foreach (var m in modules)
{
    if (m is SalesModule)
        continue;

    m.MapEndpoints(app);
}*/
/*
foreach (var m in modules)
{
    m.MapEndpoints(app);
}

app.MapControllers();

app.Run();
*/
