import { createTenant } from "../services/onboardingService";
import { mapToTenantRequest } from "../mappers/onboardingMapper";

export async function provisionERP(onboarding) {

    const request = mapToTenantRequest(onboarding);

    const response = await createTenant(request);

    return response;

}