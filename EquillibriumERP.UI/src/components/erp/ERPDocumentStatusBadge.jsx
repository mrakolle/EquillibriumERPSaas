import "./ERPBadge.css";

export default function ERPDocumentStatusBadge({ status }) {

    const statusMap = {

        0: { text: "Draft", className: "default" },
        1: { text: "Sent", className: "info" },
        2: { text: "Accepted", className: "success" },
        3: { text: "Rejected", className: "danger" },
        4: { text: "Expired", className: "warning" },
        5: { text: "Cancelled", className: "danger" },

        Draft: { text: "Draft", className: "default" },
        Sent: { text: "Sent", className: "info" },
        Accepted: { text: "Accepted", className: "success" },
        Rejected: { text: "Rejected", className: "danger" },
        Expired: { text: "Expired", className: "warning" },
        Cancelled: { text: "Cancelled", className: "danger" }

    };

    const badge = statusMap[status] ?? {
        text: String(status),
        className: "default"
    };

    return (
        <span className={`erp-status-badge ${badge.className}`}>
            {badge.text}
        </span>
    );
}