using System;
using System.Linq;

namespace EquillibriumERP.Core.Onboarding.Domain;

public static class TenantCodeGenerator
{
    public static string Generate(string tenantName)
    {
        if (string.IsNullOrWhiteSpace(tenantName))
            throw new ArgumentException("Tenant name is required.", nameof(tenantName));

        var prefix = new string(
            tenantName
                .Where(char.IsLetterOrDigit)
                .Take(4)
                .ToArray())
            .ToUpperInvariant();

        var suffix = Random.Shared.Next(10, 100);

        return $"{prefix}{suffix}";
    }
}