import ProductTable from "./ProductTable";

export default function ProductList({
    products,
    onView
}) {

    return (

        <ProductTable
            products={products}
            onView={onView}
        />

    );

}