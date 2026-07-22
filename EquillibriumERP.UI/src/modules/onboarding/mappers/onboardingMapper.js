export function mapToTenantRequest(onboarding) {

    return {

        company: {

            companyName: onboarding.company.companyName,
            tradingName: onboarding.company.tradingName,
            registrationNumber: onboarding.company.registrationNumber,
            vatNumber: onboarding.company.vatNumber,
            industry: onboarding.company.industry

        },

        administrator: {

            firstName: onboarding.administrator.firstName,
            lastName: onboarding.administrator.lastName,
            emailAddress: onboarding.administrator.email,
            password: onboarding.administrator.password,
            confirmPassword: onboarding.administrator.password

        },

        subscription: {

            plan: onboarding.subscription.plan

        }

    };

}