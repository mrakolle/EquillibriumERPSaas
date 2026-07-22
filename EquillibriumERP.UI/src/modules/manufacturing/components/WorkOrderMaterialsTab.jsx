import {
    ERPTable
} from "../../../components/erp";


export default function WorkOrderMaterialsTab({
    materials
}) {

    return (

        <ERPTable>

            <thead>

                <tr>

                    <th>
                        Raw Material
                    </th>

                    <th>
                        Expected Quantity
                    </th>

                    <th>
                        UOM
                    </th>

                </tr>

            </thead>


            <tbody>

                {

                    materials.length === 0 ? (

                        <tr>

                            <td
                                colSpan={3}
                                className="erp-table-center"
                            >
                                No materials.
                            </td>

                        </tr>

                    ) : (

                        materials.map(material => (

                            <tr
                                key={material.id}
                            >

                                <td>
                                    {
                                        material.productName ??
                                        material.rawMaterialProductId
                                    }
                                </td>

                                <td>
                                    {material.expectedQuantity}
                                </td>

                                <td>
                                    {material.unitOfMeasure}
                                </td>

                            </tr>

                        ))

                    )

                }

            </tbody>

        </ERPTable>

    );

}