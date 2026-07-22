import WorkOrderExecutionTab from "./WorkOrderExecutionTab";

import "./WorkOrderTabs.css";

export default function WorkOrderTabs({
    workOrder,
    isEditing,
    onStartStep,
    onCompleteStep
}) {

    return (

        <div className="erp-card">

           <WorkOrderExecutionTab
                workOrder={workOrder}
                steps={workOrder.steps ?? []}
                onStartStep={onStartStep}
                onCompleteStep={onCompleteStep}
            />

        </div>

    );

}