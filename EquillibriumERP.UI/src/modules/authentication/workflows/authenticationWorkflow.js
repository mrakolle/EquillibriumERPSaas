import { validateLogin } from "../validators/authenticationValidator";
import { mapToLoginRequest } from "../mappers/loginMapper";
import { login } from "../services/authenticationService";

export async function authenticate(loginModel) {

    validateLogin(loginModel);

    const request =
        mapToLoginRequest(loginModel);

    const result =
        await login(request);

    return result;

}