import { useEffect, useState } from "react";
import { FaPlus } from "react-icons/fa";

import {
    ERPButton,
    ERPModal,
    ERPPageHeader
} from "../../../components/erp";

import { getAllBoms } from "../services/bomService";

import BomList from "../components/BomList";
import BomViewer from "../components/BomViewer";
import BomForm from "../components/BomForm";
//import "../../../styles/erp-modal.css";

import "./BomPage.css";


export default function BomPage() {

    const [boms, setBoms] = useState([]);

    const [view, setView] = useState("list");

    const [selectedBomId, setSelectedBomId] = useState(null);

    const [createdBomId, setCreatedBomId] = useState(null);

    const [showCreatedDialog, setShowCreatedDialog] = useState(false);


    useEffect(() => {

        loadBoms();

    }, []);


    async function loadBoms() {

        try {

            const data = await getAllBoms();

            setBoms(data);

        }
        catch (error) {

            console.error(error);

            setBoms([]);

        }

    }


    return (

        <div className="erp-page">

            <ERPPageHeader

                title="Bills of Materials"

                subtitle="Manage product formulations and manufacturing recipes."

                actions={

                    <ERPButton
                        onClick={() => setView("new")}
                    >

                        <FaPlus />

                        New BOM

                    </ERPButton>

                }

            >
            </ERPPageHeader>
 


            {
                view === "list" && (

                    <BomList
                        boms={boms}
                        onView={(id) => {

                            setSelectedBomId(id);

                            setView("view");

                        }}
                    />

                )

            }
            {
                view === "view" && (

                    <BomViewer
                        bomId={selectedBomId}
                        onCancel={() => {

                            setSelectedBomId(null);

                            setView("list");

                        }}
                    />

                )
            }
            {
                view === "new" && (

                    <BomForm

                        onCancel={() => setView("list")}

                        onCreated={(bomId) => {

                            setCreatedBomId(bomId);

                            setShowCreatedDialog(true);

                        }}

                    />

                )

            }

            <ERPModal open={showCreatedDialog}>

                <h3>Bill of Material Created</h3>

                <p>
                    Bill of Material successfully created.
                    Would you like to add manufacturing process
                    steps now or do it later?
                </p>

                <div
                    style={{
                        display: "flex",
                        justifyContent: "flex-end",
                        gap: "12px",
                        marginTop: "24px"
                    }}
                >

                    <ERPButton
                        onClick={() => {

                            setSelectedBomId(createdBomId);

                            setShowCreatedDialog(false);

                            setView("view");

                        }}
                    >
                        Add Now
                    </ERPButton>

                    <ERPButton
                        variant="secondary"
                        onClick={() => {

                            setShowCreatedDialog(false);

                            setView("list");

                            loadBoms();

                        }}
                    >
                        Later
                    </ERPButton>

                </div>

            </ERPModal>

       </div>

    );

}