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
    createSupplier
} from "../services/supplierService";

import {
    getAllSupplierCategories
} from "../services/supplierCategoryService";

export default function SupplierForm({
    onCancel,
    onCreated
}) {

    const [categories, setCategories] = useState([]);

    const [supplier, setSupplier] = useState({

        supplierCode: "",

        name: "",

        supplierCategoryId: "",

        registrationNumber: "",

        vatNumber: "",

        taxNumber: "",

        email: "",

        phone: "",

        mobile: "",

        website: "",

        paymentTerms: 0,

        isActive: true

    });

    const [isSaving, setIsSaving] = useState(false);

    useEffect(() => {

        async function loadCategories() {

            try {

                const data =
                    await getAllSupplierCategories();

                setCategories(data);

            }
            catch (error) {

                console.error(
                    "Failed to load supplier categories:",
                    error
                );

                setCategories([]);

            }

        }

        loadCategories();

    }, []);

    function validateSupplier() {

        if (!supplier.name.trim()) {

            alert("Supplier Name is required.");

            return false;

        }

        if (!supplier.email.trim()) {

            alert("Email Address is required.");

            return false;

        }

        if (!supplier.phone.trim()) {

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
                    Number(supplier.paymentTerms)

            };

            const createdSupplier =
                await createSupplier(request);

            onCreated?.(createdSupplier);

        }
        catch (error) {

            console.error(error);

            alert("Failed to create supplier.");

        }
        finally {

            setIsSaving(false);

        }

    }

    return (

        <ERPCard

            title="New Supplier"

            subtitle="Create a new supplier."

        >

            <ERPDetailsLayout

                left={

                    <SupplierInformationForm

                        supplier={supplier}

                        categories={categories}

                        onChange={setSupplier}

                        isEditing={true}

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

                isEditing={true}

            />

            <ERPActionBar>

                <ERPButton

                    variant="secondary"

                    onClick={onCancel}

                >

                    Cancel

                </ERPButton>

                <ERPButton

                    onClick={saveSupplier}

                    disabled={isSaving}

                >

                    Save Supplier

                </ERPButton>

            </ERPActionBar>

        </ERPCard>

    );

}