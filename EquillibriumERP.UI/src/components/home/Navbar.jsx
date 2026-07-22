import { useNavigate } from "react-router-dom";

function Navbar() {
    const navigate = useNavigate();

    return (
        <header className="navbar">
            <div className="page-container">
                <div className="navbar-inner">

                    <div className="navbar-brand">

                        <img
                            src="/logo.png"
                            alt="Equillibrium ERP"
                            className="navbar-logo"
                        />

                        <div className="navbar-brand-text">
                            <h2 className="navbar-title">
                                Equillibrium ERP
                            </h2>

                            <span className="navbar-subtitle">
                                Powered by CHEM-IQ
                            </span>
                        </div>

                    </div>

                    <button
                        className="btn btn-outline"
                        onClick={() => navigate("/Login")}
                    >
                        Login
                    </button>

                </div>
            </div>
        </header>
    );
}

export default Navbar;