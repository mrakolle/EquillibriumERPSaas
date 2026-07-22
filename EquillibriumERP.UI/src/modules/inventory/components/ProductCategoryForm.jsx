import { useState } from "react";

import {
    ERPActionBar,
    ERPButton,
    ERPCard
} from "../../../components/erp";

import ProductCategoryInformation from "./ProductCategoryInformation";

import {
    createProductCategory
} from "../services/productCategoryService";

export default function ProductCategoryForm({

    onCancel,

    onCreated

}) {

    const [category, setCategory] = useState({

        name: "",

        productType: 0,

        description: "",

        isActive: true

    });

    const [isSaving, setIsSaving] = useState(false);

    function validate() {

        if (!category.name.trim()) {

            alert("Category Name is required.");

            return false;

        }

        return true;

    }

    async function saveCategory() {

        if (isSaving) {

            return;

        }

        if (!validate()) {

            return;

        }

        setIsSaving(true);

        try {

            const request = {

                name: category.name,

                productType: Number(category.productType),

                description: category.description

            };

            const created = await createProductCategory(request);

            if (onCreated) {

                onCreated(created);

            }

        }
        catch (error) {

            console.error(error);

            alert("Failed to create product category.");

        }
        finally {

            setIsSaving(false);

        }

    }

    return (

        <ERPCard

            title="New Product Category"

            subtitle="Create a new product category."

        >

            <ProductCategoryInformation

                category={category}

                onChange={setCategory}

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

                    onClick={saveCategory}

                    disabled={isSaving}

                >

                    Save Category

                </ERPButton>

            </ERPActionBar>

        </ERPCard>

    );

}