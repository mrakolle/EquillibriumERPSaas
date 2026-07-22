import { FaEye } from "react-icons/fa";

import {
    ERPTable
} from "../../../components/erp";


export default function WorkOrderTable({
    workOrders,
    onView
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

        <ERPTable>

            <thead>

                <tr>

                    <th>
                        Work Order
                    </th>

                    <th>
                        Product
                    </th>

                    <th>
                        Planned Quantity
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

                    workOrders.map((workOrder) => (

                        <tr
                            key={workOrder.id}
                        >

                            <td>
                                {workOrder.workOrderNumber}
                            </td>


                            <td>
                                {workOrder.productName}
                            </td>


                            <td>
                                {workOrder.plannedQuantity}
                            </td>


                            <td>
                                {workOrder.unitOfMeasure}
                            </td>


                            <td>
                                {getStatus(workOrder.status)}
                            </td>


                            <td className="erp-table-actions">

                                <button
                                    type="button"
                                    className="erp-icon-button"
                                    title="View Work Order"
                                    onClick={() =>
                                        onView(workOrder.id)
                                    }
                                >
                                    <FaEye />
                                </button>

                            </td>

                        </tr>

                    ))

                }

            </tbody>

        </ERPTable>

    );

}