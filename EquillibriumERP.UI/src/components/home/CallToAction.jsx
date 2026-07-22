import { useNavigate } from "react-router-dom";

function CallToAction() {
    const navigate = useNavigate();

    return (
        <section className="cta">

            <div className="page-container">

                <div className="cta-inner">

                    <h2 className="cta-title">
                        Ready to Transform Your Chemical Manufacturing Business?
                    </h2>

                    <p className="cta-text">
                        Start with the Free Edition today and upgrade as your
                        business grows. No installation. No infrastructure.
                        Just a modern cloud ERP built for chemical manufacturers.
                    </p>

                    <div className="cta-actions">

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

            </div>

        </section>
    );
}

export default CallToAction;