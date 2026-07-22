function SubscriptionStep({
    subscription,
    updateField
}) {
    return (

        <div className="subscription-grid">

            <div
                className={
                    subscription.plan === "Free"
                        ? "plan-card selected"
                        : "plan-card"
                }
                onClick={() =>
                    updateField(
                        "subscription",
                        "plan",
                        "Free"
                    )
                }
            >
                <h3>Free</h3>

                <h2>R0</h2>

                <p>Perfect for evaluating Equillibrium ERP.</p>

                <ul>
                    <li>✔ Single Tenant</li>
                    <li>✔ Core ERP</li>
                    <li>✔ Community Support</li>
                </ul>

            </div>

            <div
                className={
                    subscription.plan === "Professional"
                        ? "plan-card selected"
                        : "plan-card"
                }
                onClick={() =>
                    updateField(
                        "subscription",
                        "plan",
                        "Professional"
                    )
                }
            >
                <h3>Professional</h3>

                <h2>Contact Sales</h2>

                <p>For growing manufacturers.</p>

                <ul>
                    <li>✔ Unlimited Users</li>
                    <li>✔ Manufacturing</li>
                    <li>✔ Quality</li>
                    <li>✔ LIMS</li>
                    <li>✔ Marketplace</li>
                </ul>

            </div>

        </div>

    );
}

export default SubscriptionStep;