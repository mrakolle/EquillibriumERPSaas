import WorkOrderTable from "./WorkOrderTable";

export default function WorkOrderList({
    workOrders,
    onView
}) {

    return (

        <WorkOrderTable
            workOrders={workOrders}
            onView={onView}
        />

    );

}