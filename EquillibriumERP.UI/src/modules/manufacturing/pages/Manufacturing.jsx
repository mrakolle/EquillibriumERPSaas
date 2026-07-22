import { Outlet } from "react-router-dom";

export default function Manufacturing() {
    return (
        <div className="workspace-page">
            <div className="page-header">
                <h1>Manufacturing</h1>
                <p>Manage Bills of Materials and Work Orders.</p>
            </div>

            <Outlet />
        </div>
    );
}