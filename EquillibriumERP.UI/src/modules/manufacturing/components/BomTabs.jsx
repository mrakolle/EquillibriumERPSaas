import { useState } from "react";

import {
    ERPTabs
} from "../../../components/erp";

import MaterialsTab from "./MaterialsTab";
import ProcessStepsTab from "./ProcessStepsTab";
import DocumentsTab from "./DocumentsTab";

export default function BomTabs({
    bom,
    materials,
    processSteps,
    onMaterialsChange,
    onProcessStepsChange,
    isEditing
}) {

    const [activeTab, setActiveTab] = useState("materials");

    const tabs = [

        {
            key: "materials",
            label: "Materials"
        },

        {
            key: "steps",
            label: "Process Steps"
        },

        {
            key: "documents",
            label: "Documents"
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

                activeTab === "materials" && (

                    <MaterialsTab
                        items={materials}
                        onChange={onMaterialsChange}
                        isEditing={isEditing}
                    />

                )

            }

            {

                activeTab === "steps" && (

                    <ProcessStepsTab
                        steps={processSteps}
                        onChange={onProcessStepsChange}
                        isEditing={isEditing}
                    />

                )

            }

            {

                activeTab === "documents" && (

                    <DocumentsTab
                        isEditing={isEditing}
                    />

                )

            }

        </>

    );

}