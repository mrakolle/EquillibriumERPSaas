import "./Dashboard.css";

import {
    ERPCard,
    ERPDropdownButton,
    ERPPageHeader,
    ERPTable
} from "../../../components/erp";


export default function Dashboard() {

    return (

        <div className="dashboard-page">

            <ERPPageHeader

                title="Dashboard"

                subtitle="Welcome to Equillibrium ERP."

                actions={

                    <ERPDropdownButton label="+ New">

                        <button>
                            New Quotation
                        </button>

                        <button>
                            New Product
                        </button>

                        <button>
                            New BOM
                        </button>

                        <button>
                            New Work Order
                        </button>

                        <button>
                            New Purchase Order
                        </button>

                        <button>
                            New Inspection
                        </button>

                        <button>
                            New Sample
                        </button>

                    </ERPDropdownButton>

                }

            />


            <DashboardSection
                title="Sales"
                rows={[
                    { name:"Quotations", outstanding:27, active:1, completed:32 },
                    { name:"Invoices", outstanding:8, active:0, completed:145 }
                ]}
            />


            <DashboardSection
                title="Inventory"
                rows={[
                    { name:"Stock Counts", outstanding:1, active:0, completed:12 },
                    { name:"Stock Adjustments", outstanding:3, active:2, completed:41 }
                ]}
            />


            <DashboardSection
                title="Manufacturing"
                rows={[
                    { name:"Work Orders", outstanding:18, active:6, completed:203 },
                    { name:"BOM Approvals", outstanding:2, active:1, completed:58 }
                ]}
            />


            <DashboardSection
                title="Purchasing"
                rows={[
                    { name:"Purchase Orders", outstanding:12, active:5, completed:126 }
                ]}
            />


            <DashboardSection
                title="Quality"
                rows={[
                    { name:"Inspections", outstanding:7, active:4, completed:91 },
                    { name:"CAPAs", outstanding:2, active:1, completed:16 }
                ]}
            />


            <DashboardSection
                title="LIMS"
                rows={[
                    { name:"Samples", outstanding:23, active:11, completed:2156 },
                    { name:"Test Requests", outstanding:5, active:8, completed:442 },
                    { name:"COAs", outstanding:2, active:3, completed:438 }
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

        <ERPCard title={title}>


            <ERPTable>

                <thead>

                    <tr>

                        <th></th>

                        <th className="erp-table-center">
                            Outstanding
                        </th>

                        <th className="erp-table-center">
                            Active
                        </th>

                        <th className="erp-table-center">
                            Completed
                        </th>

                    </tr>

                </thead>


                <tbody>

                    {

                        rows.map(row => (

                            <tr
                                key={row.name}
                            >

                                <td>
                                    {row.name}
                                </td>


                                <td className="erp-table-number">

                                    {row.outstanding}

                                </td>


                                <td className="erp-table-number">

                                    {row.active}

                                </td>


                                <td className="erp-table-number">

                                    {row.completed.toLocaleString()}

                                </td>


                            </tr>

                        ))

                    }

                </tbody>


            </ERPTable>


        </ERPCard>

    );

}