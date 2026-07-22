import { Link } from "react-router-dom";

function LoginForm({

    loginModel,
    setLoginModel,
    onLogin

}) {

    return (

        <div className="login-form-card">

            <h2>

                Welcome Back

            </h2>

            <p>

                Sign in to continue to your ERP.

            </p>

            <div className="form-group">

                <label>

                    Company Code

                </label>

                <input
                    type="text"
                    value={loginModel.tenantCode}
                    onChange={(e) =>
                        setLoginModel({
                            ...loginModel,
                            tenantCode: e.target.value
                        })
                    }
                />

            </div>

            <div className="form-group">

                <label>

                    Email Address

                </label>

                <input
                    type="email"
                    value={loginModel.emailAddress}
                    onChange={(e) =>
                        setLoginModel({
                            ...loginModel,
                            emailAddress: e.target.value
                        })
                    }
                />

            </div>

            <div className="form-group">

                <label>

                    Password

                </label>

                <input
                    type="password"
                    value={loginModel.password}
                    onChange={(e) =>
                        setLoginModel({
                            ...loginModel,
                            password: e.target.value
                        })
                    }
                />

            </div>

            <button
                className="btn btn-primary login-button"
                onClick={onLogin}
            >

                Sign In

            </button>

            <div className="login-links">

                <Link to="/forgot-password">

                    Forgot Password?

                </Link>

                <Link to="/create-erp">

                    Create ERP

                </Link>

            </div>

        </div>

    );

}

export default LoginForm;