import "./ERPDocumentCustomerBlock.css";

export default function ERPDocumentCustomerBlock({

    customerName,
    customerAddress,
    customerPhone,
    customerEmail,
    customerVatNumber,

    quotationNumber,
    reference,
    documentDate,
    expiryDate

}) {

    return (

        <div className="erp-document-customer-block">

            <div className="erp-document-customer">

                <h3>

                    Bill To

                </h3>

                <div>{customerName}</div>

                {

                    customerAddress && (

                        <div>

                            {customerAddress}

                        </div>

                    )

                }

                {

                    customerPhone && (

                        <div>

                            {customerPhone}

                        </div>

                    )

                }

                {

                    customerEmail && (

                        <div>

                            {customerEmail}

                        </div>

                    )

                }

                {

                    customerVatNumber && (

                        <div>

                            VAT: {customerVatNumber}

                        </div>

                    )

                }

            </div>

            <div className="erp-document-information">

                <table>

                    <tbody>
                        <tr>

                            <th>

                                Quotation No

                            </th>

                            <td>

                                {quotationNumber}

                            </td>

                        </tr>

                        <tr>

                            <th>

                                Reference

                            </th>

                            <td>

                                {reference}

                            </td>

                        </tr>


                        <tr>

                            <th>

                                Date

                            </th>

                            <td>

                                {documentDate}

                            </td>

                        </tr>

                        <tr>

                            <th>

                                Expiry

                            </th>

                            <td>

                                {expiryDate}

                            </td>

                        </tr>

                    </tbody>

                </table>

            </div>

        </div>

    );

}