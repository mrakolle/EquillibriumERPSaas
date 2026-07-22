import { useState } from "react";
import "../styles/onboarding.css";
import { useNavigate } from "react-router-dom";
import CompanyStep from "../components/CompanyStep";
import AdministratorStep from "../components/AdministratorStep";
import SubscriptionStep from "../components/SubscriptionStep";
import ProvisionStep from "../components/ProvisionStep";
import { provisionERP } from "../workflows/onboardingWorkflow";

function CreateERP() {

    const [onboarding, setOnboarding] = useState({

        company: {

            companyName: "",
            tradingName: "",
            registrationNumber: "",
            vatNumber: "",
            industry: "Chemical Manufacturing"

        },

        administrator: {

            firstName: "",
            lastName: "",
            email: "",
            password: ""

        },

        subscription: {

            plan: "Free"

        }

    });
    const [currentStep, setCurrentStep] = useState(1);
    const [isProvisioning, setIsProvisioning] = useState(false);
    const navigate = useNavigate();
    
    function updateField(section, field, value) {

        setOnboarding(current => ({

            ...current,

            [section]: {

                ...current[section],

                [field]: value

            }

        }));
    }
    function nextStep() {

        if (currentStep < 4) {
            setCurrentStep(currentStep + 1);
        }
    }

    function previousStep() {

        if (currentStep > 1) {
            setCurrentStep(currentStep - 1);
        }
    }
    async function handleProvisionERP() {

        try {

            setIsProvisioning(true);

            const result = await provisionERP(onboarding);

            console.log(result);

            navigate("/Login", {
                replace: true,
                state: {
                    tenantId: result.tenantId,
                    tenantCode: result.tenantCode,
                    schema: result.schema,
                    emailAddress: onboarding.administrator.email
                }
            });

        }
        catch (error) {

            console.error(error);

            alert(error.message);

        }
        finally {

            setIsProvisioning(false);

        }

    }
    return (
        <div className="onboarding-page">

            <div className="onboarding-card">

                <h1>Create Your Free ERP</h1>

                <p>
                    Let's start by creating your company.
                </p>

                <div className="onboarding-steps">

                    <div className={`step ${currentStep === 1 ? "active" : ""}`}>
                        1. Company
                    </div>

                    <div className={`step ${currentStep === 2 ? "active" : ""}`}>
                        2. Administrator
                    </div>

                    <div className={`step ${currentStep === 3 ? "active" : ""}`}>
                        3. Subscription
                    </div>

                    <div className={`step ${currentStep === 4 ? "active" : ""}`}>
                        4. Provision
                    </div>

                </div>

                {currentStep === 1 && (

                    <CompanyStep
                        company={onboarding.company}
                        updateField={updateField}
                    />

                )}

                {currentStep === 2 && (

                    <AdministratorStep
                        administrator={onboarding.administrator}
                        updateField={updateField}
                    />

                )}

                {currentStep === 3 && (

                    <SubscriptionStep
                        subscription={onboarding.subscription}
                        updateField={updateField}
                    />
                )}
                {currentStep === 4 && (

                    <ProvisionStep
                        onboarding={onboarding}
                    />

                )}

                <div className="actions">

                    {currentStep > 1 && (

                    <button
                            className="back-button"
                            onClick={previousStep}
                        >
                            ← Back
                    </button>

                    )}

                    <button
                        className="next-button"
                        onClick={
                            currentStep === 4
                                ? handleProvisionERP
                                : nextStep
                        }
                    >
                        {currentStep === 4
                            ? "Create ERP"
                            : "Continue →"}
                    </button>

                </div>

            </div>

        </div>
    );
}

export default CreateERP;