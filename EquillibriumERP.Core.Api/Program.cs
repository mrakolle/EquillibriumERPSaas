using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using EquillibriumERP.Core.Api.Middleware;
using EquillibriumERP.Core.Infrastructure.DependencyInjection;
using EquillibriumERP.Core.Abstractions.Modules;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Infrastructure.Persistence;
using EquillibriumERP.Core.Infrastructure.MultiTenancy;
using EquillibriumERP.Core.Identity.Auth;
using Microsoft.AspNetCore.Authorization;
using EquillibriumERP.ControlPlane.Interfaces;
using EquillibriumERP.ControlPlane.Services;
using EquillibriumERP.Core.Infrastructure.Authorization;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// =====================================================
// CORE SERVICES
// =====================================================

//builder.Services.AddControlPlane();


builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("React", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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
// JWT AUTHENTICATION / AUTHORIZATION
// =====================================================

var jwt = configuration.GetSection("Jwt");

var issuer = jwt["Issuer"]
    ?? throw new InvalidOperationException("JWT Issuer is missing from configuration.");

var audience = jwt["Audience"]
    ?? throw new InvalidOperationException("JWT Audience is missing from configuration.");

var signingKey = jwt["SigningKey"]
    ?? throw new InvalidOperationException("JWT SigningKey is missing from configuration.");

if (string.IsNullOrWhiteSpace(signingKey))
    throw new InvalidOperationException("JWT SigningKey cannot be empty.");

var key = Encoding.UTF8.GetBytes(signingKey);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = issuer,
            ValidAudience = audience,

            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization();

// Permission-based authorization (ERP layer)
/*builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();*/

// Optional: clean extension hook (if you added it earlier)
// builder.Services.AddPermissionAuthorization();

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
    Console.WriteLine($"Registering services for module: {module.Name}");
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

app.UseCors("React");

app.UseAuthentication();

app.UseMiddleware<TenantMiddleware>();

app.UseAuthorization();
// =====================================================
// SHARED ENDPOINTS
// =====================================================

//PermissionEndpoints.MapPermissionEndpoints(app);
//TenantProvisioningEndpoints.MapTenantProvisioningEndpoints(app);


// =====================================================
// MODULE ENDPOINTS
// =====================================================
   

foreach (var module in modules)
{
    Console.WriteLine($"Mapping endpoints for module: {module.Name}");
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
using EquillibriumERP.Sales;
using EquillibriumERP.Inventory;
using EquillibriumERP.Manufacturing;
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
