using EquillibriumERP.Core.Abstractions.MultiTenancy;
public sealed record CreateTenantRequest(
    CompanyInformation Company,
    AdministratorInformation Administrator,
    SubscriptionInformation Subscription);