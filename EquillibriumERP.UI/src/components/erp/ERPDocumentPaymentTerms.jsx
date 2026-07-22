import "./ERPDocumentPaymentTerms.css";

export default function ERPDocumentPaymentTerms({

    validity,

    paymentTerms,

    delivery,

    pricesIncludeVat

}) {

    return (

        <div className="erp-document-payment-terms">

            <h3>

                Commercial Terms

            </h3>

            <table>

                <tbody>

                    <tr>

                        <th>

                            Quote Validity

                        </th>

                        <td>

                            {validity}

                        </td>

                    </tr>

                    <tr>

                        <th>

                            Payment

                        </th>

                        <td>

                            {paymentTerms}

                        </td>

                    </tr>

                    <tr>

                        <th>

                            Delivery

                        </th>

                        <td>

                            {delivery}

                        </td>

                    </tr>

                    <tr>

                        <th>

                            Prices

                        </th>

                        <td>

                            {

                                pricesIncludeVat

                                    ? "Inclusive of VAT"

                                    : "Exclusive of VAT"

                            }

                        </td>

                    </tr>

                </tbody>

            </table>

        </div>

    );

}