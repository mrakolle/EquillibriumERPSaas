function BomItemsEditor({
    items,
    setItems,
    longestNameLength
}) {
    return (
        <div style={{ marginTop: 30 }}>
            <h3>BOM Items</h3>

            {items.length === 0 && (
                <p>No items added yet.</p>
            )}

            {items.map((item) => (
                <div
                    key={item.id}
                    style={{
                        display: "flex",
                        gap: 10,
                        marginBottom: 10
                    }}
                >
                    <input
                        value={item.rawMaterialName}
                        readOnly
                        style={{
                            width: `${longestNameLength + 2}ch`
                        }}
                    />

                    <input
                        type="number"
                        step="0.0001"
                        min="0"
                        max="1"
                        value={item.quantity}
                        style={{
                            width: "75px",
                            textAlign: "right"
                        }}
                        onChange={(e) => {
                            const value = e.target.value;

                            setItems(prev =>
                                prev.map(i =>
                                    i.id === item.id
                                        ? {
                                            ...i,
                                            quantity: value
                                        }
                                        : i
                                )
                            );
                        }}
                    />

                    <select
                        value={item.uom}
                        onChange={(e) => {
                            const value = e.target.value;

                            setItems(prev =>
                                prev.map(i =>
                                    i.id === item.id
                                        ? {
                                            ...i,
                                            uom: value
                                        }
                                        : i
                                )
                            );
                        }}
                    >
                        <option value="">Select UOM</option>
                        <option value="kg">kg</option>
                        <option value="L">L</option>
                    </select>

                    <button
                        type="button"
                        onClick={() =>
                            setItems(prev =>
                                prev.filter(i => i.id !== item.id)
                            )
                        }
                    >
                        Remove
                    </button>
                </div>
            ))}
        </div>
    );
}

export default BomItemsEditor;