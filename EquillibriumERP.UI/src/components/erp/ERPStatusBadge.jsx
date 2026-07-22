import "./ERPBadge.css";

export default function ERPStatusBadge({
    status
}) {

    const value = (status ?? "").toLowerCase();

    let className = "erp-status-badge";

    switch (value) {

        case "active":
        case "completed":
            className += " success";
            break;

        case "pending":
        case "scheduled":
            className += " warning";
            break;

        case "in progress":
        case "inprogress":
            className += " info";
            break;

        case "inactive":
        case "cancelled":
        case "rejected":
            className += " danger";
            break;

        default:
            className += " default";
            break;

    }

    return (

        <span className={className}>

            {status}

        </span>

    );

}