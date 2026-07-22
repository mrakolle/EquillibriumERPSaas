import { useEffect, useState } from "react";

import {
    ERPActionBar,
    ERPButton,
    ERPCard,
    ERPDetailsLayout,
    ERPImageCard,
    ERPField
} from "../../../../components/erp";

import { FaUserTie } from "react-icons/fa";

import CustomerInformationForm
    from "./CustomerInformationForm";

import CustomerTabs
    from "./CustomerTabs";

import {
    getAllCustomerCategories
} from "../../services/customerCategoryService";

import {
    getCustomerById,
    updateCustomer
} from "../../services/customerService";


export default function CustomerViewer({

    customerId,

    onClose,

    onUpdated

}) {

    const [customer, setCustomer] = useState(null);

    const [categories, setCategories] = useState([]);

    const [isEditing, setIsEditing] = useState(false);

    const [isSaving, setIsSaving] = useState(false);


    useEffect(() => {

        if (customerId) {

            loadCustomer();

            loadCategories();

        }

    }, [customerId]);


    async function loadCustomer() {

        try {

            const data =
                await getCustomerById(customerId);

            setCustomer(data);

        }
        catch (error) {

            console.error(error);

            alert("Failed to load customer.");

        }

    }


    async function loadCategories() {

        try {

            const data =
                await getAllCustomerCategories();

            setCategories(data);

        }
        catch (error) {

            console.error(error);

        }

    }


    function validateCustomer() {

        if (!customer.customerCode?.trim()) {

            alert("Customer Code is required.");

            return false;

        }

        if (!customer.name?.trim()) {

            alert("Customer Name is required.");

            return false;

        }

        if (!customer.email?.trim()) {

            alert("Email Address is required.");

            return false;

        }

        if (!customer.phone?.trim()) {

            alert("Phone Number is required.");

            return false;

        }

        return true;

    }


    async function saveCustomer() {

        if (isSaving) {

            return;

        }

        if (!validateCustomer()) {

            return;

        }

        setIsSaving(true);

        try {

            const request = {

                customerCode: customer.customerCode,

                name: customer.name,

                customerType:
                    Number(customer.customerType),

                customerCategoryId:
                    customer.customerCategoryId || null,

                registrationNumber:
                    customer.registrationNumber,

                vatNumber:
                    customer.vatNumber,

                taxNumber:
                    customer.taxNumber,

                email:
                    customer.email,

                phone:
                    customer.phone,

                mobile:
                    customer.mobile,

                website:
                    customer.website,

                creditLimit:
                    Number(customer.creditLimit),

                paymentTerms:
                    Number(customer.paymentTerms),

                isActive:
                    customer.isActive

            };

            const updatedCustomer =
                await updateCustomer(
                    customer.id,
                    request
                );

            setCustomer(updatedCustomer);

            setIsEditing(false);

            onUpdated?.(updatedCustomer);

        }
        catch (error) {

            console.error(error);

            alert("Failed to update customer.");

        }
        finally {

            setIsSaving(false);

        }

    }


    if (!customer) {

        return null;

    }


    return (

        <ERPCard

            title="Customer"

            subtitle="View or edit customer."

        >

            <ERPDetailsLayout

                left={

                    <CustomerInformationForm

                        customer={customer}

                        categories={categories}

                        onChange={setCustomer}

                        isEditing={isEditing}

                        isNew={false}

                    />

                }

                right={

                    <>

                        <ERPImageCard

                            title="Customer"

                            image={null}

                            placeholderIcon={<FaUserTie />}

                            caption={
                                customer.name ||
                                "No customer selected."
                            }

                        />

                        <div
                            style={{
                                marginTop: "1rem"
                            }}
                        >

                            

                        </div>

                    </>

                }

            />

            <CustomerTabs

                customer={customer}

                onChange={setCustomer}

                isEditing={isEditing}

            />

            <ERPActionBar>

                <ERPButton

                    variant="secondary"

                    onClick={onClose}

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

                                    loadCustomer();

                                }}

                            >

                                Cancel

                            </ERPButton>

                            <ERPButton

                                onClick={saveCustomer}

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