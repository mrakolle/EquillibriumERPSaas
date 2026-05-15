using EquillibriumERP.Api.Endpoints;
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

app.Run();