import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../authentication/context/AuthenticationContext";

export default function UserMenu() {

    const navigate = useNavigate();

    const { session, logout } = useAuth();

    const [open, setOpen] = useState(false);

    function handleLogout() {

        logout();

        navigate("/login", {
            replace: true
        });

    }

    return (

        <div
            style={{
                position: "relative"
            }}
        >

            <div
                onClick={() => setOpen(!open)}
                style={{
                    cursor: "pointer",
                    textAlign: "centre"
                }}
            >
                <div>
                    <strong>{session?.tenantName}</strong>
                </div>

                <div
                    style={{
                        fontSize: "0.85rem",
                        opacity: 0.75
                    }}
                >
                    {session?.userName} ▼
                </div>
            </div>

            {
                open && (

                    <div
                        style={{
                            position: "absolute",
                            top: "100%",
                            right: 0,
                            minWidth: "180px",
                            background: "white",
                            border: "1px solid #ddd",
                            boxShadow: "0 2px 8px rgba(0,0,0,.15)"
                        }}
                    >

                        <button
                            onClick={handleLogout}
                            style={{
                                width: "100%",
                                padding: "12px",
                                border: "none",
                                background: "transparent",
                                cursor: "pointer",
                                textAlign: "left"
                            }}
                        >
                            Sign Out
                        </button>

                    </div>

                )
            }

        </div>

    );

}