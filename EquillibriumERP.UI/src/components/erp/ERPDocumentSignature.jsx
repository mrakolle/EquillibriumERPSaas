import "./ERPDocumentSignature.css";

export default function ERPDocumentSignature({

    preparedBy = "Prepared By",

    approvedBy = "Approved By"

}) {

    return (

        <div className="erp-document-signature">

            <div className="erp-document-signature-block">

                <div className="erp-document-signature-line" />

                <div className="erp-document-signature-label">

                    {preparedBy}

                </div>

                <div className="erp-document-signature-date">

                    Date: ______________________

                </div>

            </div>

            <div className="erp-document-signature-block">

                <div className="erp-document-signature-line" />

                <div className="erp-document-signature-label">

                    {approvedBy}

                </div>

                <div className="erp-document-signature-date">

                    Date: ______________________

                </div>

            </div>

        </div>

    );

}