function BomList({
    boms,
    onView
}) {
    return (
        <table
            style={{
                width: "100%",
                borderCollapse: "collapse"
            }}
        >
            <thead>
                <tr>
                    <th align="left">Code</th>
                    <th align="left">Product</th>
                    <th align="left">Description</th>
                    <th align="left">Status</th>
                    <th align="left">Actions</th>
                </tr>
            </thead>

            <tbody>
                {boms.length === 0 ? (
                    <tr>
                        <td colSpan="5">
                            No Bills of Materials found.
                        </td>
                    </tr>
                ) : (
                    boms.map(bom => (
                        <tr key={bom.id}>
                            <td>{bom.code}</td>
                            <td>{bom.name}</td>
                            <td>{bom.description}</td>
                            <td>
                                {bom.isActive ? "Active" : "Inactive"}
                            </td>

                            <td>
                                <button
                                    type="button"
                                    onClick={() => onView(bom.id)}
                                >
                                    View
                                </button>
                            </td>
                        </tr>
                    ))
                )}
            </tbody>
        </table>
    );
}

export default BomList;