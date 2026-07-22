
function BomMaterialsTable({
    items
}) {
    return (
        <div
            style={{
                border: "1px solid #ddd",
                borderRadius: 6,
                padding: 20
            }}
        >
            <h3
                style={{
                    marginTop: 0
                }}
            >
                Raw Materials
            </h3>

            <table
                style={{
                    width: "100%",
                    borderCollapse: "collapse"
                }}
            >
                <thead>
                    <tr>
                        <th align="left">Material</th>
                        <th align="right">Quantity</th>
                        <th align="left">UOM</th>
                    </tr>
                </thead>

                <tbody>

                    {items.length === 0 ? (
                        <tr>
                            <td colSpan="3">
                                No raw materials.
                            </td>
                        </tr>
                    ) : (
                        items.map(item => (
                            <tr key={item.id}>
                                <td>{item.rawMaterialName}</td>

                                <td align="right">
                                    {Number(item.quantity).toFixed(4)}
                                </td>

                                <td>{item.unitOfMeasure}</td>
                            </tr>
                        ))
                    )}

                </tbody>
            </table>
        </div>
    );
}

export default BomMaterialsTable;