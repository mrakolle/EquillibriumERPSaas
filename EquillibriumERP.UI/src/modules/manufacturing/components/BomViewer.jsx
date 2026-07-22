import { useEffect, useState } from "react";

import { getBom, updateBom } from "../services/bomService";

import ProductInformationCard from "./ProductInformationCard";
import BomTabs from "./BomTabs";

import "../../../styles/buttons.css";

export default function BomViewer({
    bomId,
    onCancel
}) {

    const [bom, setBom] = useState(null);

    const [materials, setMaterials] = useState([]);
    const [processSteps, setProcessSteps] = useState([]);

    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const [isEditing, setIsEditing] = useState(false);
    const [isSaving, setIsSaving] = useState(false);

    useEffect(() => {

        loadBom();

    }, [bomId]);
    useEffect(() => {

        console.log("processSteps state", processSteps);

    }, [processSteps]);
    async function loadBom() {

        setLoading(true);

        setError(null);

        try {

            const data = await getBom(bomId);

            setBom(data);

        }
        catch (error) {

            console.error(error);

            setError("Unable to load Bill of Material.");

        }
        finally {

            setLoading(false);

        }

    }
    function validateMaterials() {

        for (const item of materials) {

            if (!item.rawMaterialProductId) {
                alert("Every material must have a selected raw material.");
                return false;
            }

            if (Number(item.quantity) <= 0) {
                alert("Material quantity must be greater than zero.");
                return false;
            }

            if (!item.unitOfMeasure) {
                alert("Please select a unit of measure for every material.");
                return false;
            }

        }

        return true;

    }
    

    function validateProcessSteps() {

        for (const step of processSteps) {

            if (Number(step.stepNumber) <= 0) {
                alert("Step number must be greater than zero.");
                return false;
            }

            if (!step.description?.trim()) {
                alert("Every process step requires a description.");
                return false;
            }

            if (!step.type) {
                alert("Every process step requires a step type.");
                return false;
            }

            if (
                Number(step.type) === 2 &&
                !step.rawMaterialProductId
            ) {
                alert("Add Material steps require a raw material.");
                return false;
            }

        }

        return true;

    }

    function buildUpdateRequest() {

        return {

            productId: bom.productId,

            code: bom.code,

            description: bom.description,

            isActive: bom.isActive,

            items: materials.map(item => ({

                rawMaterialProductId: item.rawMaterialProductId,

                quantity: Number(item.quantity),

                unitOfMeasure: item.unitOfMeasure

            })),

            steps: processSteps.map(step => ({

                stepNumber: Number(step.stepNumber),

                description: step.description,

                duration: step.duration ?? "00:00:00",

                type: Number(step.type),

                rawMaterialProductId: step.rawMaterialProductId || null,

                quantityPercentage:
                    step.quantityPercentage == null
                        ? 0
                        : Number(step.quantityPercentage)

            }))

        };

    }
    async function persistBom() {

        const request = buildUpdateRequest();

        console.log("Update Request:", request);

        await updateBom(
            bom.id,
            request
        );

    }

    async function saveBom() {
        console.log("Save BOM clicked");
        console.log("isSaving =", isSaving);
        if (isSaving) {
            return;
        }

        console.log("Validating materials...");

        if (!validateMaterials()) {
            console.log("Material validation failed.");
            return;
        }

        console.log("Materials OK");

        console.log("Validating process steps...");

        if (!validateProcessSteps()) {
            console.log("Process step validation failed.");
            return;
        }

        console.log("Process steps OK");

        console.log("About to set isSaving");
        setIsSaving(true);
        console.log("setIsSaving called");

        try {
            console.log("Calling persistBom()");
            
            await persistBom();

            console.log("persistBom() completed");

            await loadBom();

            setIsEditing(false);

            alert("BOM saved successfully.");

        }
        catch (error) {

            console.error(error);

            alert("Failed to save BOM.");

        }
        finally {

            setIsSaving(false);

        }

    }

    if (loading) {

        return <div>Loading BOM...</div>;

    }

    if (error) {

        return <div>{error}</div>;

    }

    if (!bom) {

        return <div>Bill of Material not found, check if you still logged in</div>;

    }

    return (

        <>

            <ProductInformationCard
                bom={bom}
                onChange={setBom}
                isEditing={isEditing}
            />

            <BomTabs
                bom={bom}
                materials={materials}
                processSteps={processSteps}
                onMaterialsChange={setMaterials}
                onProcessStepsChange={setProcessSteps}
                isEditing={isEditing}
            />

            <div
                style={{
                    display: "flex",
                    justifyContent: "space-between",
                    marginTop: "24px"
                }}
            >

                <div
                    style={{
                        display: "flex",
                        gap: "8px"
                    }}
                >

                    {

                        isEditing ? (

                            <>

                                <button
                                    type="button"
                                    className="erp-primary-button"
                                    onClick={saveBom}
                                >
                                    Save BOM
                                </button>

                                <button
                                    type="button"
                                    className="erp-primary-button"
                                    onClick={() => {

                                        setIsEditing(false);

                                        loadBom();

                                    }}
                                >
                                    Cancel
                                </button>

                            </>

                        ) : (

                            <button
                                type="button"
                                className="erp-primary-button"
                                onClick={() => setIsEditing(true)}
                            >
                                Edit BOM
                            </button>

                        )

                    }

                </div>

                <button
                    type="button"
                    className="erp-primary-button"
                    onClick={onCancel}
                >
                    ← Back
                </button>

            </div>

        </>

    );

}