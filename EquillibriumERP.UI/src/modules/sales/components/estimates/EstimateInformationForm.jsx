import {
    ERPField
} from "../../../../components/erp";

export default function EstimateInformationForm({
    estimate,
    customers = [],
    onChange,
    isEditing = false,
    isNew = false
}) {

    return (

        <div>

            <ERPField
                label="Quotation No."
            >

                <input
                    value={estimate.quoteNumber || "AUTO"}
                    disabled
                />

            </ERPField>

            <ERPField
                label="Reference"
            >

                <input
                    placeholder="Customer RFQ / Tender / Enquiry (Optional)"
                    value={estimate.reference ?? ""}
                    disabled={!isEditing}
                    onChange={(e) =>
                        onChange({
                            ...estimate,
                            reference: e.target.value
                        })
                    }
                />

            </ERPField>

            <ERPField
                label="Customer"
                required
            >

                {

                    isNew ? (

                        <select
                            value={estimate.customerId}
                            onChange={(e) => {

                                const customer = customers.find(
                                    c => String(c.id) === String(e.target.value)
                                );

                                onChange({

                                    ...estimate,

                                    customerId: e.target.value,

                                    customerName: customer?.name ?? ""

                                });

                            }}
                        >

                            <option value="">
                                Select Customer...
                            </option>

                            {

                                customers.map(customer => (

                                    <option
                                        key={customer.id}
                                        value={customer.id}
                                    >
                                        {customer.name}
                                    </option>

                                ))

                            }

                        </select>

                    ) : (

                        <input
                            value={estimate.customerName ?? ""}
                            disabled
                        />

                    )

                }

            </ERPField>

            <ERPField label="Expiry Date">

                <input
                    type="date"
                    value={
                        estimate.expiryDateUtc
                            ? estimate.expiryDateUtc.substring(0, 10)
                            : ""
                    }
                    disabled={!isEditing}
                    onChange={(e) =>
                        onChange({
                            ...estimate,
                            expiryDateUtc: e.target.value
                        })
                    }
                />

            </ERPField>

            <ERPField
                label="Notes"
            >

                <textarea
                    rows={2}
                    value={estimate.notes ?? ""}
                    disabled={!isEditing}
                    onChange={(e) =>
                        onChange({
                            ...estimate,
                            notes: e.target.value
                        })
                    }
                />

            </ERPField>

        </div>

    );

}