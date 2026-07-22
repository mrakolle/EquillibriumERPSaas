import { useState } from "react";
import "./CreateERP.css";

import CreateERPForm from "./CreateERPForm";
import ProvisioningProgress from "./ProvisioningProgress";

function CreateERP() {
  const [isProvisioning, setIsProvisioning] = useState(false);
  return (
    <div className="create-erp-page">
      <div className="create-erp-container">

        <div className="left-panel">
          <CreateERPForm
              onCreate={() => setIsProvisioning(true)}
          />
        </div>

        <div className="right-panel">
          <ProvisioningProgress
              isProvisioning={isProvisioning}
          />
        </div>

      </div>

      <footer className="create-erp-footer">
        <p>🔒 Your data is secure with EquillibriumERP</p>

        <p>© 2026 EquillibriumERP. All rights reserved.</p>

        <p>Privacy Policy &nbsp; | &nbsp; Terms of Service</p>
      </footer>
    </div>
  );
}

export default CreateERP;