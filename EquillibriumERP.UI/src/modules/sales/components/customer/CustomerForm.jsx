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

import CustomerInformationForm from "./CustomerInformationForm";
import CustomerTabs from "./CustomerTabs";

import {
    createCustomer
} from "../../services/customerService";

import {
    getAllCustomerCategories
} from "../../services/customerCategoryService";


export default function CustomerForm({
    onCancel,
    onCreated
}) {

    const [categories, setCategories] = useState([]);

    const [customer, setCustomer] = useState({

        customerCode: "",

        name: "",

        customerType: 0,

        customerCategoryId: "",

        registrationNumber: "",

        vatNumber: "",

        taxNumber: "",

        email: "",

        phone: "",

        mobile: "",

        website: "",

        creditLimit: 0,

        paymentTerms: 0,

        isActive: true

    });


    const [isSaving, setIsSaving] = useState(false);


    useEffect(() => {

        async function loadCategories() {

            try {

                const data =
                    await getAllCustomerCategories();

                setCategories(data);

            }
            catch (error) {

                console.error(
                    "Failed to load customer categories:",
                    error
                );

                setCategories([]);

            }

        }


        loadCategories();

    }, []);



    function validateCustomer() {

        if (!customer.customerCode.trim()) {

            alert("Customer Code is required.");

            return false;

        }


        if (!customer.name.trim()) {

            alert("Customer Name is required.");

            return false;

        }


        if (!customer.email.trim()) {

            alert("Email Address is required.");

            return false;

        }


        if (!customer.phone.trim()) {

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


            const createdCustomer =
                await createCustomer(request);


            onCreated?.(createdCustomer);

        }
        catch (error) {

            console.error(error);

            alert(
                "Failed to create customer."
            );

        }
        finally {

            setIsSaving(false);

        }

    }



    return (

        <ERPCard

            title="New Customer"

            subtitle="Create a new customer."

        >


            <ERPDetailsLayout

                left={

                    <CustomerInformationForm

                        customer={customer}

                        categories={categories}

                        onChange={setCustomer}

                        isEditing={true}

                        isNew={true}

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


        


    </>

}

            />

            <CustomerTabs

                customer={customer}

                onChange={setCustomer}

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

                    onClick={saveCustomer}

                    disabled={isSaving}

                >

                    Save Customer

                </ERPButton>


            </ERPActionBar>


        </ERPCard>

    );

}