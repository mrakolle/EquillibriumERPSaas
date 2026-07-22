import { useEffect, useState } from "react";
import { getAllCustomers } from "../../services/customerService";
import { createEstimate } from "../../services/estimateService";
import {
    ERPActionBar,
    ERPButton,
    ERPCard,
    ERPDetailsLayout,
    ERPImageCard
} from "../../../../components/erp";

import EstimateInformationForm from "./EstimateInformationForm";
import EstimateItemsGrid from "./EstimateItemsGrid";
import EstimateTotals from "./EstimateTotals";


export default function EstimateForm({
    onCancel,
    onCreated
}) {

    const [estimate, setEstimate] = useState({

        customerId: "",

        customerName: "",

        reference: "",

        expiryDateUtc: "",

        notes: ""

    });
    const [customers, setCustomers] = useState([]);

    const [items, setItems] = useState([]);
    useEffect(() => {

        loadCustomers();

    }, []);

    async function loadCustomers() {

        try {

            const data = await getAllCustomers();

            setCustomers(data);

        }
        catch (error) {

            console.error(error);

            setCustomers([]);

        }

    }

    async function saveEstimate() {

        try {

        const createdEstimate = await createEstimate(
            estimate,
            items
        );

        onCreated(createdEstimate.id);

        }
    catch (error) {

        console.error(error);

        alert("Failed to create quotation.");

    }

}

    return (

        <ERPCard

            title="New Quotation"

            subtitle="Create a quotation for a customer."

        >

            <ERPDetailsLayout

                left={

                    <EstimateInformationForm

                        estimate={estimate}

                        customers={customers}

                        onChange={setEstimate}

                        isEditing={true}

                        isNew={true}

                    />

                }

                right={

                    <ERPImageCard

                        title="Customer"

                        caption={
                            estimate.customerName
                                ? estimate.customerName
                                : "Select a customer."
                        }

                    />

                }

            />

            <EstimateItemsGrid

                items={items}

                onChange={setItems}

                isEditing={true}

            />
            <EstimateTotals
                items={items}
            />

            <ERPActionBar>

                <ERPButton
                    variant="secondary"
                    onClick={onCancel}
                >
                    Cancel
                </ERPButton>

                <ERPButton
                    onClick={saveEstimate}
                >
                    Save Quotation
                </ERPButton>

            </ERPActionBar>

        </ERPCard>

    );

}