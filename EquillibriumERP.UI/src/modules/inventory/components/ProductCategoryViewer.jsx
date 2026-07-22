import { useEffect, useState } from "react";

import {
    ERPActionBar,
    ERPButton,
    ERPCard
} from "../../../components/erp";

import ProductCategoryInformation from "./ProductCategoryInformation";

import {
    getProductCategoryById,
    updateProductCategory
} from "../services/productCategoryService";

export default function ProductCategoryViewer({

    categoryId,

    onCancel,

    onUpdated

}) {

    const [category, setCategory] = useState(null);

    const [originalCategory, setOriginalCategory] = useState(null);

    const [isEditing, setIsEditing] = useState(false);

    const [isSaving, setIsSaving] = useState(false);

    useEffect(() => {

        loadCategory();

    }, [categoryId]);

    async function loadCategory() {

        try {

            const data = await getProductCategoryById(categoryId);

            setCategory(data);

            setOriginalCategory(data);

            setIsEditing(false);

        }
        catch (error) {

            console.error(error);

            alert("Unable to load product category.");

        }

    }

    async function saveChanges() {

        if (isSaving) {

            return;

        }

        setIsSaving(true);

        try {

            const request = {

                name: category.name,

                productType: Number(category.productType),

                description: category.description,

                isActive: category.isActive

            };

            await updateProductCategory(category.id, request);

            if (onUpdated) {

                onUpdated();

                return;

            }

            await loadCategory();

        }
        catch (error) {

            console.error(error);

            alert("Failed to update product category.");

        }
        finally {

            setIsSaving(false);

        }

    }

    function cancelEdit() {

        setCategory(originalCategory);

        setIsEditing(false);

    }

    if (!category) {

        return <div>Loading Product Category...</div>;

    }

    return (

        <ERPCard

            title="Product Category"

            subtitle="View product category."

        >

            <ProductCategoryInformation

                category={category}

                onChange={setCategory}

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