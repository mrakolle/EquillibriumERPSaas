import "./ERPDocumentViewer.css";

import ERPDocumentHeader from "./ERPDocumentHeader";
import ERPDocumentBody from "./ERPDocumentBody";
import ERPDocumentFooter from "./ERPDocumentFooter";

export default function ERPDocumentViewer({

    header,

    body,

    footer

}) {

    return (

        <div className="erp-document-page">

            <ERPDocumentHeader>

                {header}

            </ERPDocumentHeader>

            <ERPDocumentBody>

                {body}

            </ERPDocumentBody>

            <ERPDocumentFooter>

                {footer}

            </ERPDocumentFooter>

        </div>

    );

}