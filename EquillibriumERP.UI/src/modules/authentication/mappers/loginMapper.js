export function mapToLoginRequest(loginModel) {

    return {

        tenantCode: loginModel.tenantCode,

        email: loginModel.emailAddress,

        password: loginModel.password

    };

}