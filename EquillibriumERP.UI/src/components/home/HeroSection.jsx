import { useNavigate } from "react-router-dom";

function HeroSection() {
    const navigate = useNavigate();

    return (
        <section className="hero">
            <div className="page-container">

                <div className="hero-inner">

                    <div className="hero-content">

                        <h1 className="hero-title">
                            The ERP Built for Growing Chemical Manufacturers
                            <div
                                style={{
                                    marginTop: "10px",
                                    fontSize: "0.9rem",
                                    opacity: 0.65
                                }}
                            >
                                Powered by <strong>CHEM-IQ</strong>
                            </div>
                        </h1>

                        <p className="hero-description">
                            Streamline manufacturing, inventory, sales,
                            purchasing, quality assurance, laboratory
                            management, finance and marketplace operations
                            in one modern cloud ERP designed specifically
                            for chemical manufacturers.
                        </p>

                        <div className="hero-actions">

                            <button
                                className="btn btn-primary"
                                onClick={() => navigate("/create-erp")}
                            >
                                Create Your Free ERP
                            </button>

                            <button
                                className="btn btn-outline"
                            >
                                Book a Demo
                            </button>

                        </div>

                    </div>

                    <div className="hero-visual">

                        <div className="hero-message-card">

                            <h3>
                                Built for Chemical Manufacturers
                            </h3>

                            <p>
                                From raw materials to finished products,
                                Equillibrium ERP connects every stage
                                of your operation.
                            </p>

                        </div>

                    </div>

                </div>

            </div>
        </section>
    );
}

export default HeroSection;