import { createPortal } from "react-dom";

import "./ERPModal.css";

export default function ERPModal({

    open,
    children

}) {

    if (!open) return null;

    return createPortal(

        <div className="erp-modal-overlay">

            <div
                className="erp-modal"
                onClick={(e) => e.stopPropagation()}
            >

                {children}

            </div>

        </div>,

        document.body

    );

}