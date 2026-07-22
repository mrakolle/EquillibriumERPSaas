import "./ERPDocumentTerms.css";

export default function ERPDocumentTerms({

    title = "Terms & Conditions",

    terms = []

}) {

    return (

        <div className="erp-document-terms">

            <h3>{title}</h3>

            <ul>

                {

                    terms.map((term, index) => (

                        <li key={index}>

                            {term}

                        </li>

                    ))

                }

            </ul>

        </div>

    );

}