import { useState } from "react";
import BillOfMaterials from "./BillOfMaterials";


function Manufacturing() {
  const [activePage, setActivePage] = useState("bom");
  const [bomView, setBomView] = useState("list");
  const [selectedBomId, setSelectedBomId] = useState(null);
    return (
        <div style={{ padding: 24 }}>
            <h1>Manufacturing</h1>

            <div style={{ marginTop: 20 }}>
                <button onClick={() => {
                    setActivePage("bom");
                    setBomView("list");
                }}>
                    Bill of Materials
                </button>

                <button
                    style={{ marginLeft: 10 }}
                    onClick={() => setActivePage("workorders")}
                >
                    Work Orders
                </button>

                <button
                    style={{ marginLeft: 10 }}
                    onClick={() => setActivePage("production")}
                >
                    Production Runs
                </button>
                <div style={{ marginTop: 30 }}>
                    {activePage === "bom" && (<BillOfMaterials bomView={bomView}
                        setBomView={setBomView}
                        selectedBomId={selectedBomId}
                        setSelectedBomId={setSelectedBomId}/>)}
                    {activePage === "workorders" && <h2>Work Orders</h2>}
                    {activePage === "production" && <h2>Production Runs</h2>}
                </div>
            </div>
        </div>
    );
}

export default Manufacturing;