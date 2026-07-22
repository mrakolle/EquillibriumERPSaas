import { FaEye } from "react-icons/fa";

import {
    ERPDocumentStatusBadge,
    ERPTable
} from "../../../../components/erp";

export default function EstimateTable({
    estimates,
    onView
}) {

    return (

        <ERPTable>

            <thead>

                <tr>

                    <th>Quote #</th>

                    <th>Customer</th>

                    <th>Date</th>

                    <th className="erp-table-center">
                        Items
                    </th>

                    <th className="erp-table-right">
                        Total
                    </th>

                    <th className="erp-table-center">
                        Status
                    </th>

                    <th className="erp-table-actions">
                        Actions
                    </th>

                </tr>

            </thead>

            <tbody>

                {

                    estimates.length === 0 ? (

                        <tr>

                            <td
                                colSpan={7}
                                className="erp-table-center"
                            >
                                No Estimates found.
                            </td>

                        </tr>

                    ) : (

                        estimates.map((estimate) => (

                            <tr
                                key={estimate.id}
                                style={{ cursor: "pointer" }}
                                onClick={() => onView(estimate.id)}
                            >

                                <td>
                                    {estimate.referenceNumber}
                                </td>

                                <td>
                                    {estimate.customerName}
                                </td>

                                <td>
                                    {new Date(
                                        estimate.estimateDateUtc
                                    ).toLocaleDateString()}
                                </td>

                                <td className="erp-table-center">
                                    {estimate.items?.length ?? 0}
                                </td>

                                <td className="erp-table-right">
                                    {estimate.totalAmount?.toLocaleString(
                                        undefined,
                                        {
                                            style: "currency",
                                            currency: "ZAR"
                                        }
                                    )}
                                </td>

                                <td className="erp-table-center">

                                    <ERPDocumentStatusBadge
                                        status={estimate.status}
                                    />

                                </td>

                                <td
                                    className="erp-table-actions"
                                    onClick={(e) => e.stopPropagation()}
                                >

                                    <button
                                        type="button"
                                        className="erp-icon-button"
                                        title="View Estimate"
                                        onClick={() => onView(estimate.id)}
                                    >
                                        <FaEye />
                                    </button>

                                </td>

                            </tr>

                        ))

                    )

                }

            </tbody>

        </ERPTable>

    );

}