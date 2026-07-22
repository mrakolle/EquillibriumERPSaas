import { useEffect, useState } from "react";

import {
    ERPButton,
    ERPField,
    ERPForm,
    ERPTable
} from "../../../../components/erp";

import {
    createCustomerAddress,
    getCustomerAddresses
} from "../../services/customerAddressService";

export default function CustomerAddressesTab({

    customer,

    isEditing

}) {

    const [addresses, setAddresses] = useState([]);

    const [isAdding, setIsAdding] = useState(false);

    const [address, setAddress] = useState({

        customerId: "",

        addressType: "",

        addressLine1: "",

        addressLine2: "",

        city: "",

        province: "",

        postalCode: "",

        country: "",

        isPrimary: false

    });

    useEffect(() => {

        if (!customer?.id) {

            setAddresses([]);

            return;

        }

        loadAddresses();

    }, [customer?.id]);

    async function loadAddresses() {

        try {

            const data = await getCustomerAddresses(customer.id);

            setAddresses(data);

        }
        catch (error) {

            console.error(error);

        }

    }

    function beginAdd() {

        setAddress({

            customerId: customer.id,

            addressType: "",

            addressLine1: "",

            addressLine2: "",

            city: "",

            province: "",

            postalCode: "",

            country: "",

            isPrimary: false

        });

        setIsAdding(true);

    }

    async function saveAddress() {

        try {

            await createCustomerAddress(address);

            setIsAdding(false);

            await loadAddresses();

        }
        catch (error) {

            console.error(error);

            alert("Failed to save address.");

        }

    }

    const columns = [

        {
            header: "Type",
            accessor: "addressType"
        },

        {
            header: "Address",
            accessor: "addressLine1"
        },

        {
            header: "City",
            accessor: "city"
        },

        {
            header: "Province",
            accessor: "province"
        },

        {
            header: "Postal Code",
            accessor: "postalCode"
        },

        {
            header: "Country",
            accessor: "country"
        },

        {
            header: "Primary",
            accessor: row => row.isPrimary ? "Yes" : ""
        }

    ];

    if (!customer?.id) {

        return (

            <div className="erp-table">

                Save the customer before adding addresses.

            </div>

        );

    }

    return (

        <>

            <ERPTable

                columns={columns}

                data={addresses}

                emptyMessage="No addresses captured."

            />

            {

                isEditing && !isAdding && (

                    <ERPButton onClick={beginAdd}>

                        Add Address

                    </ERPButton>

                )

            }

            {

                isAdding && (

                    <ERPForm>

                        <ERPField label="Address Type">

                            <input
                                value={address.addressType}
                                onChange={(e) =>
                                    setAddress({
                                        ...address,
                                        addressType: e.target.value
                                    })
                                }
                            />

                        </ERPField>

                        <ERPField label="Address Line 1">

                            <input
                                value={address.addressLine1}
                                onChange={(e) =>
                                    setAddress({
                                        ...address,
                                        addressLine1: e.target.value
                                    })
                                }
                            />

                        </ERPField>

                        <ERPField label="Address Line 2">

                            <input
                                value={address.addressLine2}
                                onChange={(e) =>
                                    setAddress({
                                        ...address,
                                        addressLine2: e.target.value
                                    })
                                }
                            />

                        </ERPField>

                        <ERPField label="City">

                            <input
                                value={address.city}
                                onChange={(e) =>
                                    setAddress({
                                        ...address,
                                        city: e.target.value
                                    })
                                }
                            />

                        </ERPField>

                        <ERPField label="Province">

                            <input
                                value={address.province}
                                onChange={(e) =>
                                    setAddress({
                                        ...address,
                                        province: e.target.value
                                    })
                                }
                            />

                        </ERPField>

                        <ERPField label="Postal Code">

                            <input
                                value={address.postalCode}
                                onChange={(e) =>
                                    setAddress({
                                        ...address,
                                        postalCode: e.target.value
                                    })
                                }
                            />

                        </ERPField>

                        <ERPField label="Country">

                            <input
                                value={address.country}
                                onChange={(e) =>
                                    setAddress({
                                        ...address,
                                        country: e.target.value
                                    })
                                }
                            />

                        </ERPField>

                        <label>

                            <input
                                type="checkbox"
                                checked={address.isPrimary}
                                onChange={(e) =>
                                    setAddress({
                                        ...address,
                                        isPrimary: e.target.checked
                                    })
                                }
                            />

                            Primary Address

                        </label>

                        <div className="erp-actions">

                            <ERPButton
                                variant="secondary"
                                onClick={() => setIsAdding(false)}
                            >
                                Cancel
                            </ERPButton>

                            <ERPButton
                                onClick={saveAddress}
                            >
                                Save Address
                            </ERPButton>

                        </div>

                    </ERPForm>

                )

            }

        </>

    );

}