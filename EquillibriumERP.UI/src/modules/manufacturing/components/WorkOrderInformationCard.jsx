import {
    ERPCard,
    ERPDetailsLayout,
    ERPField,
    ERPImageCard
} from "../../../components/erp";


export default function WorkOrderInformationCard({
    workOrder,
    onChange,
    isEditing
}) {

    function getStatus(status) {

        switch (status) {

            case 0:
                return "Draft";

            case 1:
                return "In Progress";

            case 2:
                return "Completed";

            default:
                return "Unknown";

        }

    }


    return (

        <ERPCard

            title="Work Order Information"

            subtitle="View or edit work order details."

        >

            <ERPDetailsLayout

                left={

                    <div
                        style={{
                            display:"flex",
                            flexDirection:"column",
                            gap:"16px"
                        }}
                    >

                        <ERPField
                            label="Work Order"
                        >

                            <input
                                value={
                                    workOrder.workOrderNumber ?? ""
                                }
                                disabled
                            />

                        </ERPField>


                        <ERPField
                            label="Product"
                        >

                            <input
                                value={
                                    workOrder.productName ?? ""
                                }
                                disabled
                            />

                        </ERPField>


                        <ERPField
                            label="Planned Quantity"
                        >

                            <input
                                type="number"
                                value={
                                    workOrder.plannedQuantity ?? ""
                                }
                                disabled={!isEditing}
                                onChange={(e) =>
                                    onChange({
                                        ...workOrder,
                                        plannedQuantity:
                                            e.target.value
                                    })
                                }
                            />

                        </ERPField>


                        <ERPField
                            label="Unit Of Measure"
                        >

                            <input
                                value={
                                    workOrder.unitOfMeasure ?? ""
                                }
                                disabled
                            />

                        </ERPField>


                        <ERPField
                            label="Status"
                        >

                            <input
                                value={
                                    getStatus(workOrder.status)
                                }
                                disabled
                            />

                        </ERPField>


                    </div>

                }


                right={

                    <ERPImageCard

                        title="Work Order"

                        caption={
                            workOrder.productName ??
                            "No product selected."
                        }

                    />

                }

            />

        </ERPCard>

    );

}