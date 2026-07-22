using System;

namespace EquillibriumERP.Core.Abstractions.MultiTenancy;

public sealed record TenantOnboardingResult(
    Guid TenantId,
    string TenantCode,
    string Schema);