using EquillibriumERP.Api.Middleware;
using EquillibriumERP.Api.Endpoints;
using EquillibriumERP.Infrastructure.DependencyInjection;
using EquillibriumERP.Abstractions.Modules;
using EquillibriumERP.Sales.Infrastructure;
using EquillibriumERP.Inventory.Infrastructure;
using EquillibriumERP.Manufacturing.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
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
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<TenantMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapTenantOnboardingEndpoints();

foreach (var m in modules)
{
    m.MapEndpoints(app);
}

app.MapControllers();

app.Run();


/*using EquillibriumERP.Api.Endpoints;
using EquillibriumERP.Infrastructure.DependencyInjection;
using EquillibriumERP.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

//
// =====================================================
// SERVICES
// =====================================================
//

builder.Services.AddOpenApi();

// ✅ Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();

//
// =====================================================
// PIPELINE
// =====================================================
//

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // ✅ Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI();
}

// middleware stays
app.UseMiddleware<TenantMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();

// clean endpoint registration
app.MapTenantOnboardingEndpoints();

app.MapControllers();

app.Run();*/