function ProvisionStep({ onboarding }) {

    return (

        <div className="provision-summary">

            <h2>
                Review Your ERP
            </h2>

            <div className="summary-card">

                <div className="summary-row">
                    <strong>Company</strong>
                    <span>{onboarding.company.companyName || "-"}</span>
                </div>

                <div className="summary-row">
                    <strong>Trading Name</strong>
                    <span>{onboarding.company.tradingName || "-"}</span>
                </div>

                <div className="summary-row">
                    <strong>Industry</strong>
                    <span>{onboarding.company.industry}</span>
                </div>

                <div className="summary-row">
                    <strong>Administrator</strong>
                    <span>
                        {onboarding.administrator.firstName}{" "}
                        {onboarding.administrator.lastName}
                    </span>
                </div>

                <div className="summary-row">
                    <strong>Email</strong>
                    <span>{onboarding.administrator.email}</span>
                </div>

                <div className="summary-row">
                    <strong>Subscription</strong>
                    <span>{onboarding.subscription.plan}</span>
                </div>

            </div>

            <p className="provision-note">
                Clicking <strong>Create ERP</strong> will provision
                your tenant, create the administrator account,
                seed the ERP and prepare your workspace.
            </p>

        </div>

    );

}

export default ProvisionStep;