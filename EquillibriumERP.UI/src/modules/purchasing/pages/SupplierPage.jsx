import { useState } from "react";
import { FaPlus } from "react-icons/fa";

import {
    ERPButton,
    ERPPageHeader
} from "../../../components/erp";

import SupplierTable from "../components/SupplierTable";
import SupplierForm from "../components/SupplierForm";
import SupplierViewer from "../components/SupplierViewer";

import useSuppliers from "../hooks/useSuppliers";

export default function SupplierPage() {

    const {

        suppliers,

        reload

    } = useSuppliers();

    const [view, setView] = useState("list");

    const [selectedSupplierId, setSelectedSupplierId] = useState(null);

    return (

        <div className="erp-page">

            <ERPPageHeader

                title="Suppliers"

                subtitle="Manage suppliers."

                actions={

                    <ERPButton
                        onClick={() => setView("new")}
                    >

                        <FaPlus />

                        New Supplier

                    </ERPButton>

                }

            />

            {

                view === "list" && (

                    <SupplierTable

                        suppliers={suppliers}

                        onView={(id) => {

                            setSelectedSupplierId(id);

                            setView("view");

                        }}

                    />

                )

            }

            {

                view === "new" && (

                    <SupplierForm

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

                    <SupplierViewer

                        supplierId={selectedSupplierId}

                        onCancel={() => {

                            reload();

                            setSelectedSupplierId(null);

                            setView("list");

                        }}

                    />

                )

            }

        </div>

    );

}