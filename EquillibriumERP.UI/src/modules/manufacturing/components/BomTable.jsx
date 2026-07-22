import { FaEye } from "react-icons/fa";

import {
    ERPStatusBadge,
    ERPTable
} from "../../../components/erp";

export default function BomTable({
    boms,
    onView
}) {

    return (

        <ERPTable>

            <thead>

                <tr>

                    <th>BOM Code</th>

                    <th>Product</th>

                    <th>Description</th>

                    <th className="erp-table-center">
                        Materials
                    </th>

                    <th className="erp-table-center">
                        Steps
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

                    boms.length === 0 ? (

                        <tr>

                            <td
                                colSpan={7}
                                className="erp-table-center"
                            >
                                No Bills of Material found.
                            </td>

                        </tr>

                    ) : (

                        boms.map((bom) => (

                            <tr
                                key={bom.id}
                                style={{ cursor: "pointer" }}
                                onClick={() => onView(bom.id)}
                            >

                                <td>
                                    {bom.code}
                                </td>

                                <td>
                                    {bom.name}
                                </td>

                                <td>
                                    {bom.description || "-"}
                                </td>

                                <td className="erp-table-center">
                                    {bom.items?.length ?? 0}
                                </td>

                                <td className="erp-table-center">
                                    {bom.steps?.length ?? 0}
                                </td>

                                <td className="erp-table-center">

                                    <ERPStatusBadge
                                        status={
                                            bom.isActive
                                                ? "Active"
                                                : "Inactive"
                                        }
                                    />

                                </td>

                                <td
                                    className="erp-table-actions"
                                    onClick={(e) => e.stopPropagation()}
                                >

                                    <button
                                        type="button"
                                        className="erp-icon-button"
                                        title="View Bill of Material"
                                        onClick={() => onView(bom.id)}
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