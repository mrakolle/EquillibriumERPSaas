import { FaEye } from "react-icons/fa";

import {
    ERPStatusBadge,
    ERPTable
} from "../../../../components/erp";


export default function CustomerCategoryTable({

    categories = [],

    onView

}) {

    return (

        <ERPTable>

            <thead>

                <tr>

                    <th>
                        Code
                    </th>

                    <th>
                        Name
                    </th>

                    <th>
                        Description
                    </th>

                    <th
                        style={{
                            textAlign: "center"
                        }}
                    >
                        Status
                    </th>

                    <th
                        style={{
                            textAlign: "center"
                        }}
                    >
                        Actions
                    </th>

                </tr>

            </thead>


            <tbody>

                {

                    categories.length === 0 ? (

                        <tr>

                            <td
                                colSpan={5}
                                style={{
                                    textAlign: "center"
                                }}
                            >

                                No customer categories found.

                            </td>

                        </tr>

                    ) : (

                        categories.map(category => (

                            <tr

                                key={category.id}

                                style={{
                                    cursor: "pointer"
                                }}

                                onClick={() =>
                                    onView(category.id)
                                }

                            >

                                <td>

                                    {category.code}

                                </td>


                                <td>

                                    {category.name}

                                </td>


                                <td>

                                    {category.description || "-"}

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

                                    onClick={(e) =>
                                        e.stopPropagation()
                                    }

                                >

                                    <button

                                        type="button"

                                        className="erp-icon-button"

                                        title="View Customer Category"

                                        onClick={() =>
                                            onView(category.id)
                                        }

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