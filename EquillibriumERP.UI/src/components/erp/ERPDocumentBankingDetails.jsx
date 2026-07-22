import "./ERPDocumentBankingDetails.css";

export default function ERPDocumentBankingDetails({

    bankName,
    accountName,
    accountNumber,
    accountType,
    branchName,
    branchCode,
    swiftCode,
    reference

}) {

    return (

        <div className="erp-document-banking-details">

            <h3>

                Banking Details

            </h3>

            <table>

                <tbody>

                    <tr>

                        <th>Bank</th>

                        <td>{bankName}</td>

                    </tr>

                    <tr>

                        <th>Account Name</th>

                        <td>{accountName}</td>

                    </tr>

                    <tr>

                        <th>Account Number</th>

                        <td>{accountNumber}</td>

                    </tr>

                    <tr>

                        <th>Account Type</th>

                        <td>{accountType}</td>

                    </tr>

                    <tr>

                        <th>Branch</th>

                        <td>{branchName}</td>

                    </tr>

                    <tr>

                        <th>Branch Code</th>

                        <td>{branchCode}</td>

                    </tr>

                    <tr>

                        <th>SWIFT</th>

                        <td>{swiftCode}</td>

                    </tr>

                    <tr>

                        <th>Reference</th>

                        <td>{reference}</td>

                    </tr>

                </tbody>

            </table>

        </div>

    );

}