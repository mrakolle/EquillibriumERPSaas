import {
    ERPTable
} from "../../../components/erp";


export default function WorkOrderStepsTab({
    steps = []
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
                        Status
                    </th>

                </tr>

            </thead>


            <tbody>

                {

                    steps.length === 0 ? (

                        <tr>

                            <td
                                colSpan={3}
                                className="erp-table-center"
                            >
                                No process steps.
                            </td>

                        </tr>

                    ) : (

                        steps.map(step => (

                            <tr
                                key={step.id}
                            >

                                <td className="erp-table-center">

                                    {step.stepNumber}

                                </td>


                                <td>

                                    {step.action}

                                </td>


                                <td>

                                    {getStatus(step.status)}

                                </td>


                            </tr>

                        ))

                    )

                }

            </tbody>

        </ERPTable>

    );

}