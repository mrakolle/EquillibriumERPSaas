import { useEffect, useState } from "react";
import { FaPlus } from "react-icons/fa";
import EstimateViewer from "../components/estimates/EstimateViewer";
import EstimateForm from "../components/estimates/EstimateForm";

import {
    ERPButton,
    ERPPageHeader
} from "../../../components/erp";

import EstimateList from "../components/estimates/EstimateList";

import { getAllEstimates } from "../services/estimateService";

import "./EstimatePage.css";

export default function EstimatePage() {

    const [estimates, setEstimates] = useState([]);

    const [view, setView] = useState("list");

    const [selectedEstimateId, setSelectedEstimateId] = useState(null);

    const [createdEstimateId, setCreatedEstimateId] = useState(null);

    const [showCreatedDialog, setShowCreatedDialog] = useState(false);

    useEffect(() => {

        loadEstimates();

    }, []);

    async function loadEstimates() {

        try {

            const data = await getAllEstimates();

            setEstimates(data);

        }
        catch (error) {

            console.error(error);

            setEstimates([]);

        }

    }

    return (

        <div className="erp-page">

            <ERPPageHeader

                title="Quotations"

                subtitle="Create and manage customer estimates."

                actions={

                    <ERPButton
                        onClick={() => setView("new")}
                    >

                        <FaPlus />

                        New Quotation

                    </ERPButton>

                }

            />

            {

                view === "list" && (

                    <EstimateList

                        estimates={estimates}

                        onView={(id) => {

                            setSelectedEstimateId(id);

                            setView("view");

                        }}

                    />

                )

            }

            {

                view === "view" && (

                    <EstimateViewer

                        estimateId={selectedEstimateId}

                        onCancel={() => {

                            setSelectedEstimateId(null);

                            setView("list");

                        }}

                    />

                )

            }

            {

                view === "new" && (

                    <EstimateForm

                        onCancel={() => setView("list")}

                        onCreated={async (estimateId) => {

                            await loadEstimates();

                            setCreatedEstimateId(estimateId);

                            setShowCreatedDialog(true);

                            setView("list");

                        }}

                    />

                )

            }

        </div>

    );

}