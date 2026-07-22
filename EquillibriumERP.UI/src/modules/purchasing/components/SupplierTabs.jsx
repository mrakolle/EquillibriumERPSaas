import { useState } from "react";

import {
    ERPTabs
} from "../../../components/erp";

import SupplierContactInformationTab from "./SupplierContactInformationTab";
import SupplierAddressesTab from "./SupplierAddressesTab";
import SupplierContactsTab from "./SupplierContactsTab";

export default function SupplierTabs({
    supplier,
    onChange,
    isEditing
}) {

    const [activeTab, setActiveTab] = useState("contact");

    const tabs = [

        {
            key: "contact",
            label: "Contact Information"
        },

        {
            key: "addresses",
            label: "Addresses"
        },

        {
            key: "contacts",
            label: "Contacts"
        }

    ];

    return (

        <>

            <ERPTabs
                tabs={tabs}
                activeTab={activeTab}
                onChange={setActiveTab}
            />

            {

                activeTab === "contact" && (

                    <SupplierContactInformationTab
                        supplier={supplier}
                        onChange={onChange}
                        isEditing={isEditing}
                    />

                )

            }

            {

                activeTab === "addresses" && (

                    <SupplierAddressesTab
                        supplier={supplier}
                        onChange={onChange}
                        isEditing={isEditing}
                    />

                )

            }

            {

                activeTab === "contacts" && (

                    <SupplierContactsTab
                        supplier={supplier}
                        onChange={onChange}
                        isEditing={isEditing}
                    />

                )

            }

        </>

    );

}