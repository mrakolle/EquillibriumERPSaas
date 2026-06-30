using EquillibriumERP.Core.Abstractions.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Manufacturing.Domain.Entities;

namespace EquillibriumERP.Manufacturing.Infrastructure.Persistence;

public class ManufacturingDbContext : DbContext
{
    // reserved for tenant-aware filtering
    private readonly ITenantSession _tenantSession;

    public ManufacturingDbContext(
        DbContextOptions<ManufacturingDbContext> options,
        ITenantSession tenantSession)
        : base(options)
    {
        _tenantSession = tenantSession;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(
            typeof(ManufacturingDbContext).Assembly);
    }
    public DbSet<BillOfMaterial> BillOfMaterials => Set<BillOfMaterial>();
    public DbSet<BillOfMaterialItem> BillOfMaterialItems => Set<BillOfMaterialItem>();
    public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();
    public DbSet<WorkOrderMaterial> WorkOrderMaterials => Set<WorkOrderMaterial>();
    public DbSet<MaterialConsumption> MaterialConsumptions => Set<MaterialConsumption>();
    public DbSet<ProductBatch> ProductBatches { get; set; }
    public DbSet<BOMStep> BOMSteps => Set<BOMStep>();
    public DbSet<BOMStepMaterial> BOMStepMaterials => Set<BOMStepMaterial>(); 
    public DbSet<WorkOrderTransaction> WorkOrderTransactions { get; set; }
    public DbSet<StepMaterialConsumption> StepMaterialConsumptions => Set<StepMaterialConsumption>(); 

    //------- Future Use-------------------
    /*public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();
    public DbSet<WorkOrderStep> WorkOrderSteps => Set<WorkOrderStep>();
    public DbSet<WorkOrderStepMaterial> WorkOrderStepMaterials => Set<WorkOrderStepMaterial>();*/ 
}