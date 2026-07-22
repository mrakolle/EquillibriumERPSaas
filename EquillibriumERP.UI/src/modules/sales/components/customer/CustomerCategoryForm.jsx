import { useState } from "react";

import {
    ERPActionBar,
    ERPButton,
    ERPCard
} from "../../../../components/erp";

import CustomerCategoryInformation from "./CustomerCategoryInformation";

import {
    createCustomerCategory
} from "../../services/customerCategoryService";


export default function CustomerCategoryForm({

    onCancel,

    onCreated

}) {

    const [category, setCategory] = useState({

        code: "",

        name: "",

        description: "",

        sortOrder: 0,

        isActive: true

    });

    const [isSaving, setIsSaving] = useState(false);


    function validateCategory() {

        if (!category.code.trim()) {

            alert("Category Code is required.");

            return false;

        }

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

        if (!validateCategory()) {

            return;

        }

        setIsSaving(true);

        try {

            const request = {

                code: category.code,

                name: category.name,

                description: category.description,

                sortOrder: Number(category.sortOrder),

                isActive: category.isActive

            };

            console.log(request);

            const createdCategory =
                await createCustomerCategory(request);

            onCreated?.(createdCategory);

        }
        catch (error) {

            console.error(error);

            alert("Failed to create customer category.");

        }
        finally {

            setIsSaving(false);

        }

    }


    return (

        <ERPCard

            title="New Customer Category"

            subtitle="Create a new customer category."

        >

            <CustomerCategoryInformation

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