function BomGeneralInformation({
    products,
    value,
    onProductChanged
}) {
    const selectedProduct =
        products.find(p => p.id === value);

    function handleChange(event) {
        const product =
            products.find(p => p.id === event.target.value);

        onProductChanged(product ?? null);
    }

    return (
        <div>
            <label>Product</label>
            <br />

            <select
                value={value}
                onChange={handleChange}
                style={{ width: "300px" }}
            >
                <option value="">
                    Select Product...
                </option>

                {products
                    .filter(p => p.productType === 1)
                    .map(product => (
                        <option
                            key={product.id}
                            value={product.id}
                        >
                            {product.name}
                        </option>
                    ))}
            </select>

            <div style={{ marginTop: 20 }}>
                <label>Description</label>
                <br />

                <textarea
                    value={selectedProduct?.description ?? ""}
                    readOnly
                    rows={4}
                    style={{
                        width: "300px",
                        resize: "vertical"
                    }}
                />
            </div>
        </div>
    );
}

export default BomGeneralInformation;