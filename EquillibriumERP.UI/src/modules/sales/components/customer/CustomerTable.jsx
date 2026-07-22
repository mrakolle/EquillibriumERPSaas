import { FaEye } from "react-icons/fa";

import {
    ERPStatusBadge,
    ERPTable
} from "../../../../components/erp";

export default function CustomerTable({
    customers,
    onView
}) {

    return (

        <ERPTable>

            <thead>

                <tr>

                    <th>Customer Code</th>

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

                    customers.length === 0 ? (

                        <tr>

                            <td
                                colSpan={5}
                                style={{
                                    textAlign: "center"
                                }}
                            >
                                No customers found.
                            </td>

                        </tr>

                    ) : (

                        customers.map(customer => (

                            <tr
                                key={customer.id}
                                style={{
                                    cursor: "pointer"
                                }}
                                onClick={() => onView(customer.id)}
                            >

                                <td>

                                    {customer.customerCode}

                                </td>

                                <td>

                                    {customer.name}

                                </td>

                                <td>

                                    {customer.email}

                                </td>

                                <td
                                    style={{
                                        textAlign: "center"
                                    }}
                                >

                                    <ERPStatusBadge
                                        status={
                                            customer.isActive
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
                                        title="View Customer"
                                        onClick={() => onView(customer.id)}
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