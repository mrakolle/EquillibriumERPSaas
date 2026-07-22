import { useEffect, useState } from "react";

import {
    ERPActionBar,
    ERPButton,
    ERPCard,
    ERPDetailsLayout,
    ERPImageCard
} from "../../../components/erp";

import { FaTruck } from "react-icons/fa";

import SupplierInformationForm from "./SupplierInformationForm";
import SupplierTabs from "./SupplierTabs";

import {
    getAllSupplierCategories
} from "../services/supplierCategoryService";

import {
    getSupplierById,
    updateSupplier
} from "../services/supplierService";

export default function SupplierViewer({

    supplierId,

    onCancel,

    onUpdated

}) {

    const [supplier, setSupplier] = useState(null);

    const [categories, setCategories] = useState([]);

    const [isEditing, setIsEditing] = useState(false);

    const [isSaving, setIsSaving] = useState(false);

    useEffect(() => {

        if (supplierId) {

            loadSupplier();

            loadCategories();

        }

    }, [supplierId]);

    async function loadSupplier() {

        try {

            const data =
                await getSupplierById(supplierId);

            setSupplier(data);

        }
        catch (error) {

            console.error(error);

            alert("Failed to load supplier.");

        }

    }

    async function loadCategories() {

        try {

            const data =
                await getAllSupplierCategories();

            setCategories(data);

        }
        catch (error) {

            console.error(error);

        }

    }

    function validateSupplier() {

        if (!supplier.name?.trim()) {

            alert("Supplier Name is required.");

            return false;

        }

        if (!supplier.email?.trim()) {

            alert("Email Address is required.");

            return false;

        }

        if (!supplier.phone?.trim()) {

            alert("Phone Number is required.");

            return false;

        }

        return true;

    }

    async function saveSupplier() {

        if (isSaving) {

            return;

        }

        if (!validateSupplier()) {

            return;

        }

        setIsSaving(true);

        try {

            const request = {

                supplierCode: supplier.supplierCode,

                name: supplier.name,

                supplierCategoryId:
                    supplier.supplierCategoryId || null,

                registrationNumber:
                    supplier.registrationNumber,

                vatNumber:
                    supplier.vatNumber,

                taxNumber:
                    supplier.taxNumber,

                email:
                    supplier.email,

                phone:
                    supplier.phone,

                mobile:
                    supplier.mobile,

                website:
                    supplier.website,

                paymentTerms:
                    Number(supplier.paymentTerms),

                isActive:
                    supplier.isActive

            };

            const updatedSupplier =
                await updateSupplier(
                    supplier.id,
                    request
                );

            setSupplier(updatedSupplier);

            setIsEditing(false);

            onUpdated?.(updatedSupplier);

        }
        catch (error) {

            console.error(error);

            alert("Failed to update supplier.");

        }
        finally {

            setIsSaving(false);

        }

    }

    if (!supplier) {

        return null;

    }

    return (

        <ERPCard

            title="Supplier"

            subtitle="View or edit supplier."

        >

            <ERPDetailsLayout

                left={

                    <SupplierInformationForm

                        supplier={supplier}

                        categories={categories}

                        onChange={setSupplier}

                        isEditing={isEditing}

                    />

                }

                right={

                    <ERPImageCard

                        title="Supplier"

                        image={null}

                        placeholderIcon={<FaTruck />}

                        caption={
                            supplier.name ||
                            "No supplier selected."
                        }

                    />

                }

            />

            <SupplierTabs

                supplier={supplier}

                onChange={setSupplier}

                isEditing={isEditing}

            />

            <ERPActionBar>

                <ERPButton

                    variant="secondary"

                    onClick={onCancel}

                >

                    Close

                </ERPButton>

                {

                    !isEditing ? (

                        <ERPButton

                            onClick={() =>
                                setIsEditing(true)
                            }

                        >

                            Edit

                        </ERPButton>

                    ) : (

                        <>

                            <ERPButton

                                variant="secondary"

                                onClick={() => {

                                    setIsEditing(false);

                                    loadSupplier();

                                }}

                            >

                                Cancel

                            </ERPButton>

                            <ERPButton

                                onClick={saveSupplier}

                                disabled={isSaving}

                            >

                                Save

                            </ERPButton>

                        </>

                    )

                }

            </ERPActionBar>

        </ERPCard>

    );

}