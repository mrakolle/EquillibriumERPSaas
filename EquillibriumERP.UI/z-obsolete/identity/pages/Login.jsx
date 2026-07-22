import { useState } from "react";
import { useAuth } from "../../../../context/AuthContext";
import { login } from "../../services/authApi";
import { useNavigate } from "react-router-dom";

function Login() {
    const { setIsLoggedIn } = useAuth();
    const navigate = useNavigate();
    const [tenantCode, setTenantCode] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [loading, setLoading] = useState(false);
    return (
        <div
            style={{
                height: "100vh",
                display: "flex",
                justifyContent: "center",
                alignItems: "center",
                background: "#0b1f3a",
                color: "white"
            }}
        >
            <div style={{ width: 300 }}>
                <h2>EquillibriumERP Login</h2>

                <input
                    placeholder="Tenant Code"
                    value={tenantCode}
                    onChange={(e) => setTenantCode(e.target.value)}
                    style={{ width: "100%", padding: 10, marginTop: 10 }}
                />

                <input
                    placeholder="Email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    style={{ width: "100%", padding: 10, marginTop: 10 }}
                />

                <input
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    style={{ width: "100%", padding: 10, marginTop: 10 }}
                />

                <button
                    disabled={loading}
                    onClick={async () => {
                        setLoading(true);

                        try {
                            const result = await login({
                                tenantCode,
                                email,
                                password
                            });

                            //alert(JSON.stringify(result));

                            localStorage.setItem("erp_token", result.accessToken);

                            setIsLoggedIn(true);
                            navigate("/dashboard");
                        } catch (err) {
                            console.error(err);
                            alert(err.message);
                        } finally {
                            setLoading(false);
                        }
                    }}
                    style={{
                        width: "100%",
                        marginTop: 15,
                        padding: 10,
                        background: "#1d4ed8",
                        color: "white",
                        border: "none",
                        cursor: "pointer"
                    }}
                >
                   {loading ? "Signing in..." : "Login"}
                </button>
            </div>
        </div>
    );
}

export default Login;