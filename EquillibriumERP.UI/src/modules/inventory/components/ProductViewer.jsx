import { useEffect, useState } from "react";

import {
    ERPActionBar,
    ERPButton,
    ERPCard
} from "../../../components/erp";

import ProductInformation from "./ProductInformation";

import {
    getProductById,
    updateProduct
} from "../services/productService";

export default function ProductViewer({
    productId,
    onCancel,
    onUpdated
}) {

    const [product, setProduct] = useState(null);

    const [originalProduct, setOriginalProduct] = useState(null);

    const [isEditing, setIsEditing] = useState(false);

    const [isSaving, setIsSaving] = useState(false);

    useEffect(() => {

        loadProduct();

    }, [productId]);

    async function loadProduct() {

        try {

            const data = await getProductById(productId);


            setProduct(data);

            setOriginalProduct(data);

            setIsEditing(false);

        }
        catch (error) {

            console.error(error);

            alert("Unable to load product.");

        }

    }

    async function saveChanges() {

        if (isSaving) {

            return;

        }

        setIsSaving(true);

        try {

            const request = {

                productCode: product.productCode,

                name: product.name,

                productType: Number(product.productType),

                productCategoryId: product.productCategoryId,

                sellingPrice: Number(product.sellingPrice),

                costPrice: Number(product.costPrice),

                casNumber: product.casNumber,

                description: product.description,

                isActive: product.isActive

            };

            await updateProduct(product.id, request);

            if (onUpdated) {

                onUpdated();

            }

        }
        catch (error) {

            console.error(error);

            alert("Failed to update product.");

        }
        finally {

            setIsSaving(false);

        }

    }

    function cancelEdit() {

        setProduct(originalProduct);

        setIsEditing(false);

    }

    if (!product) {

        return <div>Loading Product...</div>;

    }

    return (

        <ERPCard

            title="Product Details"

            subtitle="View product information."

        >

            <ProductInformation

                product={product}

                onChange={setProduct}

                isEditing={isEditing}

                isNew={false}

            />

            <ERPActionBar>

                {

                    isEditing ? (

                        <>

                            <ERPButton

                                variant="secondary"

                                onClick={cancelEdit}

                            >

                                Cancel

                            </ERPButton>

                            <ERPButton

                                onClick={saveChanges}

                                disabled={isSaving}

                            >

                                Save Changes

                            </ERPButton>

                        </>

                    ) : (

                        <>

                            <ERPButton

                                variant="secondary"

                                onClick={onCancel}

                            >

                                ← Back

                            </ERPButton>

                            <ERPButton

                                onClick={() => setIsEditing(true)}

                            >

                                Edit

                            </ERPButton>

                        </>

                    )

                }

            </ERPActionBar>

        </ERPCard>

    );

}