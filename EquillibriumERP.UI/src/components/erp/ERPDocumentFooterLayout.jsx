import "./ERPDocumentFooterLayout.css";

export default function ERPDocumentFooterLayout({

    left,

    right

}) {

    return (

        <div className="erp-document-footer-layout">

            <div className="erp-document-footer-left">

                {left}

            </div>

            <div className="erp-document-footer-right">

                {right}

            </div>

        </div>

    );

}