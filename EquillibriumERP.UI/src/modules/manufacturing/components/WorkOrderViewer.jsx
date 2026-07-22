import { useEffect, useState } from "react";

import WorkOrderInformationCard from "./WorkOrderInformationCard";
import WorkOrderTabs from "./WorkOrderTabs";

import {
    getWorkOrder,
    startWorkOrder
} from "../services/workOrdersService";

import "../../../styles/buttons.css";

export default function WorkOrderViewer({
    workOrderId,
    onCancel
}) {

    const [workOrder, setWorkOrder] = useState(null);

    const [isEditing, setIsEditing] = useState(false);

    const [isSaving] = useState(false);

    useEffect(() => {

        loadWorkOrder();

    }, [workOrderId]);

    async function loadWorkOrder() {

        try {

            const data = await getWorkOrder(workOrderId);

            setWorkOrder(data);

        }
        catch (error) {

            console.error(error);

        }

    }

    async function handleStartWorkOrder() {

        try {

            console.log("Starting Work Order:", workOrder.id);

            await startWorkOrder(workOrder.id);

            await loadWorkOrder();

        }
        catch (error) {

            console.error(error);

            alert(error.message);

        }

    }

    async function handleStartStep(step) {

        console.log("Start Step", step);

    }

    async function handleCompleteStep(step) {

        console.log("Complete Step", step);

    }

    if (!workOrder) {

        return (

            <div className="erp-card">
                Loading Work Order...
            </div>

        );

    }

    return (

        <>

            <WorkOrderInformationCard
                workOrder={workOrder}
                onChange={setWorkOrder}
                isEditing={isEditing}
            />

            <WorkOrderTabs
                workOrder={workOrder}
                isEditing={isEditing}
                onStartStep={handleStartStep}
                onCompleteStep={handleCompleteStep}
            />

            <div
                style={{
                    display: "flex",
                    justifyContent: "space-between",
                    marginTop: "24px"
                }}
            >

                <div
                    style={{
                        display: "flex",
                        gap: "8px"
                    }}
                >

                    <button
                        type="button"
                        className="erp-primary-button"
                        disabled={workOrder.status !== 0}
                        onClick={handleStartWorkOrder}
                    >
                        Start Work Order
                    </button>

                    {

                        isEditing ? (

                            <>

                                <button
                                    type="button"
                                    className="erp-primary-button"
                                    disabled={isSaving}
                                >
                                    Save Work Order
                                </button>

                                <button
                                    type="button"
                                    className="erp-primary-button"
                                    onClick={() => {

                                        setIsEditing(false);

                                        loadWorkOrder();

                                    }}
                                >
                                    Cancel
                                </button>

                            </>

                        ) : (

                            <button
                                type="button"
                                className="erp-primary-button"
                                disabled={workOrder.status !== 0}
                                onClick={() => setIsEditing(true)}
                            >
                                Edit Work Order
                            </button>

                        )

                    }

                </div>

                <button
                    type="button"
                    className="erp-primary-button"
                    onClick={onCancel}
                >
                    ← Back
                </button>

            </div>

        </>

    );

}