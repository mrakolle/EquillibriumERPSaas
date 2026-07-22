import "./ERPDocumentModal.css";

import ERPButton from "./ERPButton";

export default function ERPDocumentModal({

    title,

    children,

    onClose,

    onPrint,

    onDownload

}) {

    return (

        <div className="erp-document-modal-overlay">

            <div className="erp-document-modal">

                <div className="erp-document-modal-header">

                    <h2>{title}</h2>

                    <div
                        className="erp-document-modal-toolbar"
                    >

                        <ERPButton
                            variant="secondary"
                            onClick={onPrint}
                        >
                            Print
                        </ERPButton>

                        <ERPButton
                            variant="secondary"
                            onClick={onDownload}
                        >
                            Download PDF
                        </ERPButton>

                        <ERPButton
                            variant="secondary"
                            onClick={onClose}
                        >
                            Close
                        </ERPButton>

                    </div>

                </div>

                <div className="erp-document-modal-content">

                    {children}

                </div>

            </div>

        </div>

    );

}