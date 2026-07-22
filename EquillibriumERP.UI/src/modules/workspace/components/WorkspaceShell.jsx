import "../styles/workspaceShell.css";

import TopBar from "./TopBar";
import SideNavigation from "./SideNavigation";
import ContentArea from "./ContentArea";

export default function WorkspaceShell() {
    return (
        <div className="workspace-shell">

            <div className="workspace-topbar">
                <TopBar />
            </div>

            <div className="workspace-body">

                <div className="workspace-sidebar">
                    <SideNavigation />
                </div>

                <div className="workspace-content">
                    <ContentArea />
                </div>

            </div>

        </div>
    );
}