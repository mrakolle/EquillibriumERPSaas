import "./ERPCompanyHeader.css";

export default function ERPCompanyHeader({

    companyName,
    companyAddress,
    companyPhone,
    companyEmail,
    companyWebsite,

    companyRegistrationNumber,
    companyTaxReference,
    companyVatNumber,
    companyCsdNumber,

    logo,
    title,
    documentNumber

}) {

    return (

        <div className="erp-company-header">

            <div className="erp-company-left">

                {

                    logo && (

                        <img
                            src={logo}
                            alt={companyName}
                            className="erp-company-logo"
                        />

                    )

                }

                <div>

                    <h2>{companyName}</h2>

                    <div>{companyAddress}</div>

                    <div>{companyPhone}</div>

                    <div>{companyEmail}</div>

                    <div>{companyWebsite}</div>

                </div>

            </div>

            <div className="erp-company-right">

                <h1>{title}</h1>

                <table className="erp-company-details">

                    <tbody>

                        <tr>

                            <th>Co Reg No:</th>

                            <td>{companyRegistrationNumber}</td>

                        </tr>

                        <tr>

                            <th>Tax Ref:</th>

                            <td>{companyTaxReference}</td>

                        </tr>

                        <tr>

                            <th>VAT No:</th>

                            <td>{companyVatNumber}</td>

                        </tr>

                        <tr>

                            <th>CSD No:</th>

                            <td>{companyCsdNumber}</td>

                        </tr>

                        

                    </tbody>

                </table>

            </div>

        </div>

    );

}