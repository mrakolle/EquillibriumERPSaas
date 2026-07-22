import {
    ERPTable
} from "../../../components/erp";


export default function DocumentsTab() {

    const documents = [

        {
            id: 1,
            type: "SDS",
            name: "Safety Data Sheet.pdf",
            revision: "4",
            uploaded: "2026-07-12"
        },

        {
            id: 2,
            type: "TDS",
            name: "Technical Data Sheet.pdf",
            revision: "2",
            uploaded: "2026-07-10"
        },

        {
            id: 3,
            type: "Label",
            name: "5L Product Label.ai",
            revision: "1",
            uploaded: "2026-07-01"
        }

    ];


    return (

        <ERPTable>

            <thead>

                <tr>

                    <th>
                        Type
                    </th>

                    <th>
                        Document
                    </th>

                    <th>
                        Revision
                    </th>

                    <th>
                        Uploaded
                    </th>

                    <th className="erp-table-actions">
                        Action
                    </th>

                </tr>

            </thead>


            <tbody>

                {

                    documents.map(document => (

                        <tr
                            key={document.id}
                        >

                            <td>
                                {document.type}
                            </td>

                            <td>
                                {document.name}
                            </td>

                            <td>
                                {document.revision}
                            </td>

                            <td>
                                {document.uploaded}
                            </td>

                            <td className="erp-table-actions">

                                <button
                                    type="button"
                                    className="erp-icon-button"
                                >
                                    View
                                </button>

                            </td>

                        </tr>

                    ))

                }

            </tbody>

        </ERPTable>

    );

}