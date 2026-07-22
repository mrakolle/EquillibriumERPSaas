import "./EstimateDocumentTable.css";

export default function EstimateDocumentTable({

    items = []

}) {

    return (

        <table className="estimate-document-table">

            <thead>

                <tr>

                    <th>Code</th>

                    <th>Description</th>

                    <th>UOM</th>

                    <th>Qty</th>

                    <th>Unit Price</th>

                    <th>Tax</th>

                    <th>Total</th>

                </tr>

            </thead>

            <tbody>

                {
                    items.map(item => (

                        <tr key={item.id}>

                            <td>
                                {item.productCode}
                            </td>

                            <td>

                                <strong>
                                    {item.productName}
                                </strong>

                                {
                                    item.description && (

                                        <div className="description">

                                            {item.description}

                                        </div>

                                    )
                                }

                            </td>

                            <td>
                                {item.unitOfMeasure}
                            </td>

                            <td>
                                {item.quantity}
                            </td>

                            <td>
                                {Number(item.unitPrice).toFixed(2)}
                            </td>

                            <td>
                                {Number(item.taxAmount).toFixed(2)}
                            </td>

                            <td>
                                {Number(item.lineTotal).toFixed(2)}
                            </td>

                        </tr>

                    ))
                }

            </tbody>

        </table>

    );

}