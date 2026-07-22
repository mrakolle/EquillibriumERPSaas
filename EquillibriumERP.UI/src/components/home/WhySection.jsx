function WhySection() {
    const reasons = [
        {
            title: "Built for Chemical Manufacturing",
            text: "Designed around formulations, batches, production orders and process manufacturing rather than generic assembly."
        },
        {
            title: "Complete Traceability",
            text: "Track every batch from raw material purchase through production, quality testing and customer delivery."
        },
        {
            title: "One Connected Platform",
            text: "Sales, Manufacturing, Inventory, Purchasing, QA, LIMS and Marketplace operate from a single source of truth."
        }
    ];

    return (
        <section className="why">
            <div className="page-container">

                <div className="why-inner">

                    <div>

                        <h2 className="why-title">
                            Why Chemical Manufacturers Choose Equillibrium ERP
                        </h2>

                        <p className="why-description">
                            Traditional ERP systems are built for general manufacturing.
                            Equillibrium ERP is engineered specifically for process and
                            chemical manufacturers, helping you streamline operations,
                            improve quality and scale confidently.
                        </p>

                    </div>

                    <div className="why-grid">

                        {reasons.map(reason => (
                            <div
                                key={reason.title}
                                className="why-card"
                            >
                                <h3 className="why-card-title">
                                    {reason.title}
                                </h3>

                                <p className="why-card-text">
                                    {reason.text}
                                </p>
                            </div>
                        ))}

                    </div>

                </div>

            </div>
        </section>
    );
}

export default WhySection;