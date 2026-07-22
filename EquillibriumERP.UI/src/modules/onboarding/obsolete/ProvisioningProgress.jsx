import { useEffect, useState } from "react";

const steps = [
    "Creating workspace",
    "Configuring company",
    "Provisioning database",
    "Installing core modules",
    "Creating administrator"
];

function ProvisioningProgress({ isProvisioning }) {

    const [currentStep, setCurrentStep] = useState(-1);

    useEffect(() => {

        if (!isProvisioning)
            return;

        const timer = setInterval(() => {

            setCurrentStep(previous => {

                if (previous >= steps.length)
                    return previous;

                return previous + 1;

            });

        }, 1200);

        return () => clearInterval(timer);

    }, [isProvisioning]);

    return (
        <>
            <h2 className="progress-title">
                Creating your ERP...
            </h2>

            <div className="progress-list">

                {steps.map((step, index) => (

                    <div
                        key={step}
                        className="progress-item"
                    >

                        {index < currentStep && "✅"}

                        {index === currentStep && "⏳"}

                        {index > currentStep && "⚪"}

                        <span style={{ marginLeft: 12 }}>
                            {step}
                        </span>

                    </div>

                ))}

            </div>

            {currentStep > steps.length - 1 && (

                <div className="progress-complete">

                    <h3>Your ERP is ready!</h3>

                    <button className="launch-button">
                        Launch EquillibriumERP
                    </button>

                </div>

            )}

        </>
    );
}

export default ProvisioningProgress;