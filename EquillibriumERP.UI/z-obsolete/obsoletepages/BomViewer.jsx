import { useEffect, useState } from "react";
import { getBom } from "../services/bomApi";
import BomGeneralInformation from "../../src/components/manufacturing/BomGeneralInformation";
import BomMaterialsTable from "./BomMaterialsTable";
import BomEditor from "../../src/components/manufacturing/BomEditor";

function BomViewer({
    bomId,
    onSaved,
    onCancel
}) {
    const [bom, setBom] = useState(null);
    const [isEditing, setIsEditing] = useState(false);

    useEffect(() => {
        load();
    }, [bomId]);

    async function load() {
        const data = await getBom(bomId);
        console.log(data);
        setBom(data);
    }

    if (!bom) {
        return <div>Loading...</div>;
    }

    if (isEditing) {
        return (
            <BomEditor
                bom={bom}
                onSaved={async () => {
                    await load();
                    setIsEditing(false);

                    if (onSaved) {
                        onSaved();
                    }
                }}
                onCancel={() => setIsEditing(false)}
            />
        );
    }

    return (
        <>
            <BomGeneralInformation
                code={bom.code}
                productName={bom.name}
                description={bom.description}
            />

            <BomMaterialsTable
                items={bom.items}
            />

            <div
                style={{
                    marginTop: 20,
                    display: "flex",
                    gap: 10
                }}
            >
                <button
                    type="button"
                    onClick={() => setIsEditing(true)}
                >
                    Edit
                </button>

                <button
                    type="button"
                    onClick={onCancel}
                >
                    Back
                </button>
            </div>
        </>
    );
}

export default BomViewer;