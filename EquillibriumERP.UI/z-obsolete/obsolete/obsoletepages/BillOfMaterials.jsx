import { useEffect, useState } from "react";
import { getAllBoms } from "../../services/bomApi";
import BomWorkspace from "./BomWorkspace";
import BomList from "./BomList";
import BomViewer from "./BomViewer";
import BomEditor from "../../../components/manufacturing/BomEditor";

function BillOfMaterials({
    bomView,
    setBomView,
    selectedBomId,
    setSelectedBomId
}) {
    const [boms, setBoms] = useState([]);

    useEffect(() => {
        load();
    }, []);

    async function load() {
        const data = await getAllBoms();
        setBoms(data);
    }

    let content = null;

    switch (bomView) {
        case "new":
            content = (
                <BomEditor
                    onSaved={() => {
                        load();
                        setBomView("list");
                    }}
                    onCancel={() => setBomView("list")}
                />
            );
            break;

        case "view":
            content = (
                <BomViewer
                    bomId={selectedBomId}
                    onCancel={() => setBomView("list")}
                    onSaved={load}
                />
            );
            break;

        default:
            content = (
                <BomList
                    boms={boms}
                    onNew={() => setBomView("new")}
                    onView={(bomId) => {
                        setSelectedBomId(bomId);
                        setBomView("view");
                    }}
                />
            );
            break;
    }

    return (
        <BomWorkspace
            title="Bill of Materials"
            headerActions={
                bomView === "list" && (
                    <button
                        type="button"
                        onClick={() => setBomView("new")}
                    >
                        New BOM
                    </button>
                )
            }
        >
            {content}
        </BomWorkspace>
    );
}

export default BillOfMaterials;