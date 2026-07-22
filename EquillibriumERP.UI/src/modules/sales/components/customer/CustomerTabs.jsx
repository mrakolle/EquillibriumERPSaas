import { useState } from "react";

import {
    ERPTabs
} from "../../../../components/erp";

import CustomerContactInformationTab from "./CustomerContactInformationTab";
import CustomerAddressesTab from "./CustomerAddressesTab";
import CustomerContactsTab from "./CustomerContactsTab";

export default function CustomerTabs({
    customer,
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

                    <CustomerContactInformationTab
                        customer={customer}
                        onChange={onChange}
                        isEditing={isEditing}
                    />

                )

            }

            {

                activeTab === "addresses" && (

                    <CustomerAddressesTab
                        customer={customer}
                        onChange={onChange}
                        isEditing={isEditing}
                    />

                )

            }

            {

                activeTab === "contacts" && (

                    <CustomerContactsTab
                        customer={customer}
                        onChange={onChange}
                        isEditing={isEditing}
                    />

                )

            }

        </>

    );

}