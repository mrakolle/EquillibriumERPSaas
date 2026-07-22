import {
    ERPButton,
    ERPTable
} from "../../../components/erp";


export default function WorkOrderExecutionTab({
    workOrder,
    steps = [],
    onStartStep,
    onCompleteStep
}) {

    function getStatus(status) {

        switch (status) {

            case 0:
                return "Pending";

            case 1:
                return "In Progress";

            case 2:
                return "Completed";

            default:
                return "Unknown";

        }

    }


    return (

        <ERPTable>

            <thead>

                <tr>

                    <th className="erp-table-center">
                        Step
                    </th>

                    <th>
                        Description
                    </th>

                    <th>
                        Duration
                    </th>

                    <th>
                        Material
                    </th>

                    <th>
                        Expected Qty
                    </th>

                    <th>
                        Material Consumed
                    </th>

                    <th>
                        UOM
                    </th>

                    <th>
                        Status
                    </th>

                    <th className="erp-table-actions">
                        Actions
                    </th>

                </tr>

            </thead>


            <tbody>

                {

                    steps.length === 0 ? (

                        <tr>

                            <td
                                colSpan={9}
                                className="erp-table-center"
                            >
                                No execution steps.
                            </td>

                        </tr>

                    ) : (

                        steps.map(step => {

                            const workOrderStarted =
                                workOrder?.status === 1;


                            const previousStepCompleted =
                                step.stepNumber === 1 ||
                                steps.find(
                                    x =>
                                        x.stepNumber === step.stepNumber - 1
                                )?.status === 2;


                            const canStart =
                                workOrderStarted &&
                                step.status === 0 &&
                                previousStepCompleted;


                            const canComplete =
                                step.status === 1;


                            return (

                                <tr key={step.id}>

                                    <td className="erp-table-center">
                                        {step.stepNumber}
                                    </td>


                                    <td>
                                        {step.description}
                                    </td>


                                    <td>
                                        {step.plannedDuration ?? "-"}
                                    </td>


                                    <td>
                                        {step.material ?? "-"}
                                    </td>


                                    <td>
                                        {step.expectedQuantity ?? "-"}
                                    </td>


                                    <td>
                                        {step.consumedQuantity ?? "-"}
                                    </td>


                                    <td>
                                        {step.unitOfMeasure ?? "-"}
                                    </td>


                                    <td>
                                        {getStatus(step.status)}
                                    </td>


                                    <td className="erp-table-actions">

                                        {
                                            step.status === 0 && (

                                                <ERPButton
                                                    variant="primary"
                                                    disabled={!canStart}
                                                    onClick={() =>
                                                        onStartStep?.(step)
                                                    }
                                                >
                                                    Start
                                                </ERPButton>

                                            )
                                        }


                                        {
                                            step.status === 1 && (

                                                <ERPButton
                                                    variant="success"
                                                    disabled={!canComplete}
                                                    onClick={() =>
                                                        onCompleteStep?.(step)
                                                    }
                                                >
                                                    Complete
                                                </ERPButton>

                                            )
                                        }


                                        {
                                            step.status === 2 && (

                                                <span>
                                                    ✔
                                                </span>

                                            )
                                        }

                                    </td>

                                </tr>

                            );

                        })

                    )

                }

            </tbody>

        </ERPTable>

    );

}