function Footer() {
    return (
        <footer className="footer">

            <div className="page-container">

                <div className="footer-grid">

                    <div className="footer-brand">

                        <img
                            src="/logo.png"
                            alt="Equillibrium ERP"
                            className="footer-logo"
                        />

                        <div className="footer-title">
                            Equillibrium ERP
                        </div>

                        <p className="footer-text">
                            Cloud ERP built specifically for chemical
                            manufacturers, powered by CHEM-IQ.
                        </p>

                    </div>

                    <div>

                        <h3 className="footer-heading">
                            Product
                        </h3>

                        <div className="footer-links">
                            <a href="#">Features</a>
                            <a href="#">Pricing</a>
                            <a href="#">Marketplace</a>
                        </div>

                    </div>

                    <div>

                        <h3 className="footer-heading">
                            Company
                        </h3>

                        <div className="footer-links">
                            <a href="#">About</a>
                            <a href="#">Contact</a>
                            <a href="#">Book Demo</a>
                        </div>

                    </div>

                    <div>

                        <h3 className="footer-heading">
                            Resources
                        </h3>

                        <div className="footer-links">
                            <a href="#">Documentation</a>
                            <a href="#">Support</a>
                            <a href="#">Privacy</a>
                        </div>

                    </div>

                </div>

                <div className="footer-bottom">
                    © 2026 Equillibrium ERP. Powered by CHEM-IQ.
                </div>

            </div>

        </footer>
    );
}

export default Footer;