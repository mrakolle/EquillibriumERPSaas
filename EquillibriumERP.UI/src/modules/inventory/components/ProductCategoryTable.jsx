import { FaEye } from "react-icons/fa";

import {
    ERPStatusBadge,
    ERPTable
} from "../../../components/erp";

export default function ProductCategoryTable({
    categories,
    onView
}) {

    return (

        <ERPTable>

            <thead>

                <tr>

                    <th>Name</th>

                    <th>Product Type</th>

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

                    categories.length === 0 ? (

                        <tr>

                            <td
                                colSpan={4}
                                style={{
                                    textAlign: "center"
                                }}
                            >
                                No product categories found.
                            </td>

                        </tr>

                    ) : (

                        categories.map(category => (

                            <tr
                                key={category.id}
                                style={{
                                    cursor: "pointer"
                                }}
                                onClick={() => onView(category.id)}
                            >

                                <td>

                                    {category.name}

                                </td>

                                <td>

                                    {
                                        Number(category.productType) === 0
                                            ? "Raw Material"
                                            : "Manufactured Product"
                                    }

                                </td>

                                <td
                                    style={{
                                        textAlign: "center"
                                    }}
                                >

                                    <ERPStatusBadge
                                        status={
                                            category.isActive
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
                                        title="View Category"
                                        onClick={() => onView(category.id)}
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