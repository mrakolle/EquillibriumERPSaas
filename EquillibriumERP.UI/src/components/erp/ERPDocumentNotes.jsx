import "./ERPDocumentNotes.css";

export default function ERPDocumentNotes({

    title = "Notes",

    notes = []

}) {

    if (!notes.length) {

        return null;

    }

    return (

        <div className="erp-document-notes">

            <h3>{title}</h3>

            <ul>

                {

                    notes.map((note, index) => (

                        <li key={index}>

                            {note}

                        </li>

                    ))

                }

            </ul>

        </div>

    );

}