import "./ERPTable.css";

export default function ERPTable({

    children,

    className = ""

}) {

    return (

        <div className="erp-table-container">

            <table
                className={`erp-table ${className}`}
            >

                {children}

            </table>

        </div>

    );

}