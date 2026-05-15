using Microsoft.AspNetCore.Mvc;
using EquillibriumERP.Infrastructure.Persistence;

namespace EquillibriumERP.Api.Controllers;

[ApiController]
[Route("debug")]
public class DebugController : ControllerBase
{
    private readonly TenantDbContext _db;

    public DebugController(TenantDbContext db)
    {
        _db = db;
    }

    [HttpGet("tenant-db")]
    public IActionResult GetTenantDbInfo()
    {
        return Ok(new
        {
            //Schema = _db.Schema,
            Provider = _db.Database.ProviderName,
            Entities = _db.Model.GetEntityTypes().Select(x => x.Name)
        });
    }
}