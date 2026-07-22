import { FaEye } from "react-icons/fa";

import {
    ERPTable,
    ERPStatusBadge
} from "../../../components/erp";

export default function ProductTable({
    products,
    onView
}) {

    return (

        <ERPTable>

            <thead>

                <tr>

                    <th>Code</th>

                    <th>Name</th>

                    <th style={{ textAlign: "center" }}>
                        Price
                    </th>

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

                    products.length === 0 ? (

                        <tr>

                            <td
                                colSpan={5}
                                style={{
                                    textAlign: "center"
                                }}
                            >
                                No products found.
                            </td>

                        </tr>

                    ) : (

                        products.map(product => (

                            <tr
                                key={product.id}
                                style={{
                                    cursor: "pointer"
                                }}
                                onClick={() => onView(product.id)}
                            >

                                <td>
                                    {product.productCode}
                                </td>

                                <td>
                                    {product.name}
                                </td>

                                <td
                                    style={{
                                        textAlign: "center"
                                    }}
                                >
                                    {product.sellingPrice}
                                </td>

                                <td
                                    style={{
                                        textAlign: "center"
                                    }}
                                >

                                    <ERPStatusBadge
                                        status={
                                            product.isActive
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
                                        title="View Product"
                                        onClick={() => onView(product.id)}
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