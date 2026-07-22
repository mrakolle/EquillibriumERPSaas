export function validateLogin(loginModel) {

    if (!loginModel.tenantCode?.trim()) {

        throw new Error("Company Code is required.");

    }

    if (!loginModel.emailAddress?.trim()) {

        throw new Error("Email Address is required.");

    }

    if (!loginModel.password?.trim()) {

        throw new Error("Password is required.");

    }

}