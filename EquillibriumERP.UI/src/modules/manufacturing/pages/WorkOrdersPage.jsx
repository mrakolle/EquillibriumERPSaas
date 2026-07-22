import { useState } from "react";

import useWorkOrders from "../hooks/useWorkOrders";

import WorkOrderList from "../components/WorkOrderList";
import WorkOrderViewer from "../components/WorkOrderViewer";
import WorkOrderForm from "../components/WorkOrderForm";

import { FaPlus } from "react-icons/fa";

import "./WorkOrdersPage.css";

export default function WorkOrdersPage() {

    const {
        workOrders,
        reload,
        addWorkOrder
    } = useWorkOrders();


    const [view, setView] = useState("list");

    const [selectedWorkOrderId, setSelectedWorkOrderId] = useState(null);


    async function handleCreate(request) {

        try {

            await addWorkOrder(request);

            await reload();

            setView("list");

        }
        catch (error) {

            console.error(error);

            alert("Failed to create Work Order.");

        }

    }


    return (

        <div className="erp-page">

            <div className="erp-page-header">

                <div className="erp-page-title">
                    Work Orders
                </div>


                <div className="erp-page-actions">

                    <button
                        type="button"
                        className="erp-add-button"
                        onClick={() => setView("new")}
                    >
                        <FaPlus />

                        <span>
                            New Work Order
                        </span>

                    </button>

                </div>

            </div>


            <div className="erp-page-divider"></div>


            {
                view === "list" && (

                    <WorkOrderList
                        workOrders={workOrders}
                        onView={(id) => {

                            setSelectedWorkOrderId(id);

                            setView("view");

                        }}
                    />

                )
            }


            {
                view === "new" && (

                    <WorkOrderForm
                        onSave={handleCreate}
                        onCancel={() => setView("list")}
                    />

                )
            }


            {
                view === "view" && (

                    <WorkOrderViewer
                        workOrderId={selectedWorkOrderId}
                        onCancel={() => {

                            setSelectedWorkOrderId(null);

                            setView("list");

                        }}
                    />

                )
            }


        </div>

    );

}