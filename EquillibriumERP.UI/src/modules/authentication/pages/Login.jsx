import { useState } from "react";
import { useLocation } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthenticationContext";
import LoginBrandPanel from "../components/LoginBrandPanel";
import LoginForm from "../components/LoginForm";

import { createLoginModel } from "../models/loginModel";
import { authenticate } from "../workflows/authenticationWorkflow";

import "../styles/login.css";

function Login() {

    const location = useLocation();

    const [loginModel, setLoginModel] = useState({

        ...createLoginModel(),

        tenantCode: location.state?.tenantCode ?? "",

        emailAddress: location.state?.emailAddress ?? ""

    });
    const navigate = useNavigate();
    const { login } = useAuth();

    async function handleLogin() {

        try {

            console.log(loginModel);

            const result = await authenticate(loginModel);

            login(result);

            navigate("/workspace", {
                replace: true
            });

        }
        catch (error) {

            alert(error.message);

        }

    }

    return (

        <div className="login-page">

            <LoginBrandPanel />

            <LoginForm
                loginModel={loginModel}
                setLoginModel={setLoginModel}
                onLogin={handleLogin}
            />

        </div>

    );

}

export default Login;