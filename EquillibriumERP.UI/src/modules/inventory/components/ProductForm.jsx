import { useState } from "react";

import {
    ERPActionBar,
    ERPButton,
    ERPCard
} from "../../../components/erp";

import ProductInformation from "../components/ProductInformation";

import { createProduct } from "../services/productService";

export default function ProductForm({
    onCancel,
    onCreated
}) {

    const [product, setProduct] = useState({

        productCode: "",

        name: "",

        productType: 1,

        productCategoryId: "",

        sellingPrice: 0,

        costPrice: 0,

        casNumber: "",

        description: "",

        isActive: true

    });

    const [isSaving, setIsSaving] = useState(false);

    function validateProduct() {

        if (!product.productCode.trim()) {

            alert("Product Code is required.");
            return false;

        }

        if (!product.name.trim()) {

            alert("Product Name is required.");
            return false;

        }

        if (!product.productCategoryId) {

            alert("Please select a Product Category.");
            return false;

        }

        if (Number(product.sellingPrice) < 0) {

            alert("Selling Price cannot be negative.");
            return false;

        }

        if (Number(product.costPrice) < 0) {

            alert("Cost Price cannot be negative.");
            return false;

        }

        return true;

    }

    async function saveProduct() {

        if (isSaving) {

            return;

        }

        if (!validateProduct()) {

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

            console.log("Create Product Request", request);

            const createdProduct = await createProduct(request);

            if (onCreated) {

                onCreated(createdProduct);

            }

        }
        catch (error) {

            console.error(error);

            alert("Failed to create product.");

        }
        finally {

            setIsSaving(false);

        }

    }

    return (

        <ERPCard
            title="New Product"
            subtitle="Create a new product."
        >

            <ProductInformation

                product={product}

                onChange={setProduct}

                isEditing={true}

                isNew={true}

            />

            <ERPActionBar>

                <ERPButton
                    variant="secondary"
                    onClick={onCancel}
                >
                    Cancel
                </ERPButton>

                <ERPButton
                    onClick={saveProduct}
                    disabled={isSaving}
                >
                    Save Product
                </ERPButton>

            </ERPActionBar>

        </ERPCard>

    );

}