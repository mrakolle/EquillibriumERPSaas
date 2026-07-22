import { FaEye } from "react-icons/fa";

import {
    ERPStatusBadge,
    ERPTable
} from "../../../components/erp";

export default function SupplierTable({
    suppliers,
    onView
}) {

    return (

        <ERPTable>

            <thead>

                <tr>.      

                    <th>Supplier Code</th>

                    <th>Name</th>

                    <th>Email</th>

                    <th style={{ textAlign: "center" }}>
                        Status
                    </th>

                    <th style={{ textAlign: "center" }}>
                        Actions
                    </th>

                </tr>

            </thead>

            <tbody>

                {

                    suppliers.length === 0 ? (

                        <tr>

                            <td
                                colSpan={5}
                                style={{
                                    textAlign: "center"
                                }}
                            >
                                No suppliers found.
                            </td>

                        </tr>

                    ) : (

                        suppliers.map(supplier => (

                            <tr
                                key={supplier.id}
                                style={{
                                    cursor: "pointer"
                                }}
                                onClick={() => onView(supplier.id)}
                            >

                                <td>

                                    {supplier.supplierCode}

                                </td>

                                <td>

                                    {supplier.name}

                                </td>

                                <td>

                                    {supplier.email}

                                </td>

                                <td
                                    style={{
                                        textAlign: "center"
                                    }}
                                >

                                    <ERPStatusBadge
                                        status={
                                            supplier.isActive
                                                ? "Active"
                                                : "Inactive"
                                        }
                                    />

                                </td>

                                <td
                                    style={{
                                        textAlign: "center"
                                    }}
                                    onClick={(e) => e.stopPropagation()}
                                >

                                    <button
                                        type="button"
                                        className="erp-icon-button"
                                        title="View Supplier"
                                        onClick={() => onView(supplier.id)}
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