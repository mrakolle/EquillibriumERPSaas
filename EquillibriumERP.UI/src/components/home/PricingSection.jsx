import { Fragment } from "react";
import { useNavigate } from "react-router-dom";
import "./PricingSection.css";

function PricingSection() {

    const navigate = useNavigate();

    const plans = [
        {
            name: "Free",
            price: "R 0",
            featured: false,
        },
        {
            name: "Tier 1",
            price: "Coming Soon",
            featured: true,
        },
        {
            name: "Tier 2",
            price: "Coming Soon",
            featured: false,
        },
        {
            name: "Enterprise",
            price: "Contact Us",
            featured: false,
        },
    ];

    const featureGroups = [

        {
            category: "Sales",
            items: [
                { name: "Quotations", values: ["✓", "✓", "✓", "✓"] },
                { name: "Invoices", values: ["✓", "✓", "✓", "✓"] },
                { name: "Customers", values: ["✓", "✓", "✓", "✓"] }
            ]
        },

        {
            category: "Purchasing",
            items: [
                { name: "Suppliers", values: ["✓", "✓", "✓", "✓"] },
                { name: "Purchase Orders", values: ["✓", "✓", "✓", "✓"] }
            ]
        },

        {
            category: "Manufacturing",
            items: [
                { name: "Chemical Manufacturing", values: ["✓", "✓", "✓", "✓"] },
                { name: "Bill of Materials", values: ["✓", "✓", "✓", "✓"] },
                { name: "Work Orders", values: ["✓", "✓", "✓", "✓"] },
                { name: "Production Scheduling", values: ["—", "✓", "✓", "✓"] }
            ]
        },

        {
            category: "Inventory",
            items: [
                { name: "Inventory Management", values: ["—", "✓", "✓", "✓"] },
                { name: "Stock Movements", values: ["—", "✓", "✓", "✓"] },
                { name: "Warehouse Management", values: ["—", "✓", "✓", "✓"] }
            ]
        },

        {
            category: "Quality",
            items: [
                { name: "Quality Assurance", values: ["—", "—", "✓", "✓"] },
                { name: "LIMS", values: ["—", "—", "✓", "✓"] },
                { name: "Batch Traceability", values: ["—", "—", "✓", "✓"] }
            ]
        },

        {
            category: "Enterprise",
            items: [
                { name: "Marketplace", values: ["—", "—", "—", "✓"] },
                { name: "API Integrations", values: ["—", "—", "—", "✓"] },
                { name: "Custom Modules", values: ["—", "—", "—", "✓"] }
            ]
        }

    ];

    return (

        <section className="pricing">

            <div className="page-container">

                <div className="pricing-header">

                    <h2 className="pricing-title">
                        Pricing That Grows With Your Business
                    </h2>

                    <p className="pricing-subtitle">
                        Start free and unlock additional capabilities as your
                        manufacturing business grows.
                    </p>

                </div>

                <div className="pricing-table-container">

                    <table className="pricing-table">

                        <thead>

                            <tr>

                                <th className="feature-column">
                                    Features
                                </th>

                                {plans.map(plan => (

                                    <th
                                        key={plan.name}
                                        className={plan.featured ? "featured" : ""}
                                    >

                                        {plan.featured && (
                                            <div className="popular-badge">
                                                MOST POPULAR
                                            </div>
                                        )}

                                        <div className="plan-name">
                                            {plan.name}
                                        </div>

                                        <div className="plan-price">
                                            {plan.price}
                                        </div>

                                    </th>

                                ))}

                            </tr>

                        </thead>

                        <tbody>

                            {featureGroups.map(group => (

                                <Fragment key={group.category}>

                                    <tr className="category-row">
                                        <td colSpan={5}>
                                            {group.category}
                                        </td>
                                    </tr>

                                    {group.items.map(item => (

                                        <tr key={item.name}>

                                            <td className="feature-name">
                                                {item.name}
                                            </td>

                                            {item.values.map((value, index) => (

                                                <td
                                                    key={index}
                                                    className={
                                                        plans[index].featured
                                                            ? "featured"
                                                            : ""
                                                    }
                                                >

                                                    {value === "✓"
                                                        ? (
                                                            <span className="check">
                                                                ✓
                                                            </span>
                                                        )
                                                        : (
                                                            <span className="dash">
                                                                —
                                                            </span>
                                                        )}

                                                </td>

                                            ))}

                                        </tr>

                                    ))}

                                </Fragment>

                            ))}

                            <tr className="action-row">

                                <td></td>

                                {plans.map(plan => (

                                    <td
                                        key={plan.name}
                                        className={
                                            plan.featured
                                                ? "featured"
                                                : ""
                                        }
                                    >

                                        <button
                                            className={
                                                plan.featured
                                                    ? "btn btn-primary"
                                                    : "btn btn-outline"
                                            }
                                            onClick={() => navigate("/create-erp")}
                                        >
                                            {plan.name === "Enterprise"
                                                ? "Contact Us"
                                                : "Get Started"}
                                        </button>

                                    </td>

                                ))}

                            </tr>

                        </tbody>

                    </table>

                </div>

            </div>

        </section>

    );

}

export default PricingSection;









/*import { useNavigate } from "react-router-dom";

function PricingSection() {
    const navigate = useNavigate();

    const plans = [
        {
            name: "Free",
            price: "R 0 ",
            featured: false,
            features: [
                "Sales (Quotations & Invoicing)",
                "Purchasing",
                "Chemical Manufacturing",
                "Bills of Materials"
            ]
        },
        {
            name: "Tier 1",
            price: "Coming Soon",
            featured: true,
            features: [
                "Everything in Free",
                "Inventory Management",
                "Stock Movements",
                "Warehouse Management"
            ]
        },
        {
            name: "Tier 2",
            price: "Coming Soon",
            featured: false,
            features: [
                "Everything in Tier 1",
                "Quality Assurance",
                "LIMS",
                "Batch Traceability"
            ]
        },
        {
            name: "Enterprise",
            price: "Contact Us",
            featured: false,
            features: [
                "Everything in Tier 2",
                "Marketplace",
                "API Integrations",
                "Custom Modules"
            ]
        }
    ];

    return (
        <section className="pricing">
            <div className="page-container">

                <div className="pricing-header">

                    <h2 className="pricing-title">
                        Pricing That Grows With Your Business
                    </h2>

                    <p className="pricing-subtitle">
                        Start free and unlock more capabilities as your
                        manufacturing operation grows.
                    </p>

                </div>

                <div className="pricing-grid">

                    {plans.map(plan => (
                        <div
                            key={plan.name}
                            className={`pricing-card ${plan.featured ? "featured" : ""}`}
                        >
                            <h3 className="pricing-name">
                                {plan.name}
                            </h3>

                            <div className="pricing-price">
                                {plan.price}
                            </div>

                            <div className="pricing-features">

                                {plan.features.map(feature => (
                                    <div
                                        key={feature}
                                        className="pricing-feature"
                                    >
                                        ✓ {feature}
                                    </div>
                                ))}

                            </div>

                            <button
                                className={
                                    plan.featured
                                        ? "btn btn-primary"
                                        : "btn btn-outline"
                                }
                                onClick={() => navigate("/create-erp")}
                            >
                                Get Started
                            </button>

                        </div>
                    ))}

                </div>

            </div>
        </section>
    );
}

export default PricingSection;*/