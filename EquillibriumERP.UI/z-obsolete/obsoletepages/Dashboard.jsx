export default function Dashboard() {

    return (

        <div
            style={{
                padding: "24px"
            }}
        >

            <h1>
                Dashboard
            </h1>

            <p>
                Welcome to Equillibrium ERP.
            </p>

            <DashboardSection
                title="Sales"
                rows={[
                    { name: "Quotations", outstanding: 27, active: 1, completed: 32 },
                    { name: "Invoices", outstanding: 8, active: 0, completed: 145 }
                ]}
            />

            <DashboardSection
                title="Inventory"
                rows={[
                    { name: "Stock Counts", outstanding: 1, active: 0, completed: 12 },
                    { name: "Stock Adjustments", outstanding: 3, active: 2, completed: 41 }
                ]}
            />

            <DashboardSection
                title="Manufacturing"
                rows={[
                    { name: "Work Orders", outstanding: 18, active: 6, completed: 203 },
                    { name: "BOM Approvals", outstanding: 2, active: 1, completed: 58 }
                ]}
            />

            <DashboardSection
                title="Purchasing"
                rows={[
                    { name: "Purchase Orders", outstanding: 12, active: 5, completed: 126 }
                ]}
            />

            <DashboardSection
                title="Quality"
                rows={[
                    { name: "Inspections", outstanding: 7, active: 4, completed: 91 },
                    { name: "CAPAs", outstanding: 2, active: 1, completed: 16 }
                ]}
            />

            <DashboardSection
                title="LIMS"
                rows={[
                    { name: "Samples", outstanding: 23, active: 11, completed: 2156 },
                    { name: "Test Requests", outstanding: 5, active: 8, completed: 442 },
                    { name: "COAs", outstanding: 2, active: 3, completed: 438 }
                ]}
            />

        </div>

    );

}

function DashboardSection({

    title,

    rows

}) {

    return (

        <div
            style={{
                marginTop: "30px",
                border: "1px solid #ddd",
                borderRadius: "8px",
                background: "#ffffff",
                overflow: "hidden"
            }}
        >

            <div
                style={{
                    padding: "14px 18px",
                    background: "#f5f5f5",
                    fontWeight: "600",
                    fontSize: "1rem"
                }}
            >
                {title}
            </div>

            <table
                style={{
                    width: "100%",
                    borderCollapse: "collapse"
                }}
            >

                <thead>

                    <tr>

                        <th
                            style={headerStyle}
                        >
                        </th>

                        <th
                            style={headerStyle}
                        >
                            Outstanding
                        </th>

                        <th
                            style={headerStyle}
                        >
                            Active
                        </th>

                        <th
                            style={headerStyle}
                        >
                            Completed
                        </th>

                    </tr>

                </thead>

                <tbody>

                    {

                        rows.map(row => (

                            <tr key={row.name}>

                                <td
                                    style={cellStyle}
                                >
                                    {row.name}
                                </td>

                                <td
                                    style={numberCellStyle}
                                >
                                    {row.outstanding}
                                </td>

                                <td
                                    style={numberCellStyle}
                                >
                                    {row.active}
                                </td>

                                <td
                                    style={numberCellStyle}
                                >
                                    {row.completed.toLocaleString()}
                                </td>

                            </tr>

                        ))

                    }

                </tbody>

            </table>

        </div>

    );

}

const headerStyle = {

    textAlign: "center",

    padding: "12px",

    borderBottom: "1px solid #ddd",

    fontWeight: "600"

};

const cellStyle = {

    padding: "12px 16px",

    borderBottom: "1px solid #eee"

};

const numberCellStyle = {

    textAlign: "center",

    padding: "12px",

    borderBottom: "1px solid #eee",

    fontWeight: "600"

};