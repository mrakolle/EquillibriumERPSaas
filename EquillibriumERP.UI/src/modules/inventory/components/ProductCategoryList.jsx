import ProductCategoryTable from "./ProductCategoryTable";

export default function ProductCategoryList({
    categories,
    onView
}) {

    return (

        <ProductCategoryTable

            categories={categories}

            onView={onView}

        />

    );

}