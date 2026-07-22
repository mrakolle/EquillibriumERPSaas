import {
    DollarSign,
    ShoppingCart,
    Boxes,
    FlaskConical,
    ClipboardList,
    CalendarClock,
    ShieldCheck,
    TestTube,
    Store
} from "lucide-react";

function FeatureGrid() {
    const features = [
        {
            icon: DollarSign,
            title: "Sales & CRM",
            description:
                "Manage quotations, sales orders, invoicing and customer relationships."
        },
        {
            icon: FlaskConical,
            title: "Chemical Manufacturing",
            description:
                "Run production using formulations, recipes and process manufacturing workflows."
        },
        {
            icon: ClipboardList,
            title: "Bills of Materials",
            description:
                "Maintain formulations, ingredients, percentages and production instructions."
        },
        {
            icon: Boxes,
            title: "Inventory Management",
            description:
                "Track raw materials, finished goods, stock movements and warehouse balances."
        },
        {
            icon: ShoppingCart,
            title: "Purchasing",
            description:
                "Manage suppliers, purchase orders, receiving and procurement."
        },
        {
            icon: CalendarClock,
            title: "Production Planning",
            description:
                "Plan batches, schedule work orders and monitor production progress."
        },
        {
            icon: ShieldCheck,
            title: "Quality Assurance",
            description:
                "Manage inspections, quality checks and compliance requirements."
        },
        {
            icon: TestTube,
            title: "LIMS",
            description:
                "Capture laboratory tests, certificates of analysis and batch results."
        },
        {
            icon: Store,
            title: "Marketplace",
            description:
                "Connect customers directly with your products through an integrated marketplace."
        }
    ];

    return (
        <section className="features">
            <div className="page-container">

                <div className="features-header">

                    <h2 className="features-title">
                        Everything You Need to Run Your Factory
                    </h2>

                    <p className="features-subtitle">
                        One connected platform for chemical manufacturing,
                        operations, quality and commerce.
                    </p>

                </div>

                <div className="grid-3">

                    {features.map((feature) => {

                        const Icon = feature.icon;

                        return (
                            <div
                                key={feature.title}
                                className="feature-card"
                            >
                                <div className="feature-icon">
                                    <Icon size={36} />
                                </div>

                                <h3 className="feature-title">
                                    {feature.title}
                                </h3>

                                <p className="feature-description">
                                    {feature.description}
                                </p>

                            </div>
                        );
                    })}

                </div>

            </div>
        </section>
    );
}

export default FeatureGrid;





/*const features = [
    {
        icon: "💼",
        title: "Sales & CRM",
        description: "Manage customers, quotations, sales orders and invoicing from a single platform."
    },
    {
        icon: "📦",
        title: "Inventory Management",
        description: "Track raw materials, finished goods, stock movements and warehouse balances."
    },
    {
        icon: "🧪",
        title: "Chemical Manufacturing",
        description: "Control formulations, production batches and manufacturing execution."
    },
    {
        icon: "📋",
        title: "Bills of Materials",
        description: "Create and maintain reusable BOMs for every manufactured product."
    },
    {
        icon: "⚙️",
        title: "Production Planning",
        description: "Schedule work orders and monitor production progress in real time."
    },
    {
        icon: "🛒",
        title: "Purchasing",
        description: "Manage suppliers, purchase orders and incoming inventory."
    },
    {
        icon: "🔬",
        title: "Batch Traceability",
        description: "Maintain complete traceability from raw materials to finished products."
    },
    {
        icon: "🧬",
        title: "Laboratory Information Management (LIMS)",
        description: "Manage laboratory workflows, testing, results and quality data with integrated LIMS capabilities."
    },
    {
        icon: "🌐",
        title: "Marketplace",
        description: "Connect with suppliers, customers and industry partners through an integrated ERP marketplace."
    },
    {
        icon: "📈",
        title: "Financial Management",
        description: "Gain visibility into costs, profitability and business performance."
    }
];

function FeatureGrid() {
    return (
        <section
            style={{
                maxWidth: 1200,
                margin: "0 auto",
                padding: "20px 40px 80px"
            }}
        >
            <h2
                style={{
                    textAlign: "center",
                    fontSize: 38,
                    color: "#0B3C5D",
                    marginBottom: 50
                }}
            >
                Everything You Need to Run Your Chemical Manufacturing Business
            </h2>

            <div
                style={{
                    display: "grid",
                    gridTemplateColumns: "repeat(auto-fit, minmax(260px, 1fr))",
                    gap: 24
                }}
            >
                {features.map(feature => (
                    <div
                        key={feature.title}
                        style={{
                            background: "#FFFFFF",
                            borderRadius: 16,
                            padding: 28,
                            boxShadow: "0 8px 24px rgba(0,0,0,0.08)"
                        }}
                    >
                        <div
                            style={{
                                fontSize: 42,
                                marginBottom: 16
                            }}
                        >
                            {feature.icon}
                        </div>

                        <h3
                            style={{
                                color: "#0B3C5D",
                                marginBottom: 12
                            }}
                        >
                            {feature.title}
                        </h3>

                        <p
                            style={{
                                color: "#475569",
                                lineHeight: 1.6,
                                margin: 0
                            }}
                        >
                            {feature.description}
                        </p>
                    </div>
                ))}
            </div>
        </section>
    );
}

export default FeatureGrid;*/