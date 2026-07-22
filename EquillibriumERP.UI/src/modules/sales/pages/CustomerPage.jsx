import { useState } from "react";
import { FaPlus } from "react-icons/fa";

import {
    ERPButton,
    ERPPageHeader
} from "../../../components/erp";

import CustomerTable from "../components/customer/CustomerTable";
import CustomerForm from "../components/customer/CustomerForm";
import CustomerViewer from "../components/customer/CustomerViewer";

import useCustomers from "../hooks/useCustomers";

export default function CustomerPage() {

    const {

        customers,

        reload

    } = useCustomers();

    const [view, setView] = useState("list");

    const [selectedCustomerId, setSelectedCustomerId] = useState(null);

    return (

        <div className="erp-page">

            <ERPPageHeader

                title="Customers"

                subtitle="Manage customers."

                actions={

                    <ERPButton
                        onClick={() => setView("new")}
                    >

                        <FaPlus />

                        New Customer

                    </ERPButton>

                }

            />

            {

                view === "list" && (

                    <CustomerTable

                        customers={customers}

                        onView={(id) => {

                            setSelectedCustomerId(id);

                            setView("view");

                        }}

                    />

                )

            }

            {

                view === "new" && (

                    <CustomerForm

                        onCancel={() => setView("list")}

                        onCreated={() => {

                            reload();

                            setView("list");

                        }}

                    />

                )

            }

            {

                view === "view" && (

                    <CustomerViewer

                        customerId={selectedCustomerId}

                        onCancel={() => {

                            reload();

                            setSelectedCustomerId(null);

                            setView("list");

                        }}

                    />

                )

            }

        </div>

    );

}