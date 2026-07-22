import { apiFetch } from "../../authentication/services/apiClient";

export async function getAllEstimates() {

    const response = await apiFetch("/sales/estimates");

    if (!response.ok)
        throw new Error("Failed to load Estimates.");

    return await response.json();

}

export async function getEstimate(id) {

    const response = await apiFetch(`/sales/estimates/${id}`);

    if (!response.ok)
        throw new Error("Failed to load Estimate.");

    return await response.json();

}

export async function createEstimate(
    estimate,
    items
) {

    const request = {

        customerId: estimate.customerId,

        reference: estimate.reference,

        expiryDateUtc: estimate.expiryDateUtc,

        notes: estimate.notes,

        items: items.map(item => ({

            productId: item.productId,

            quantity: Number(item.quantity),

            unitPrice: Number(item.unitPrice),

            discountPercent: Number(item.discountPercent),

            taxRate: Number(item.taxRate)

        }))

    };

    const response = await apiFetch("/sales/estimates/Create", {

        method: "POST",

        body: JSON.stringify(request)

    });

    if (!response.ok)
        throw new Error("Failed to create Estimate.");

    return await response.json();

}

export async function updateEstimate(
    id,
    estimate,
    items
) {

    const request = {

        customerId: estimate.customerId,

        reference: estimate.reference,

        expiryDateUtc: estimate.expiryDateUtc,

        notes: estimate.notes,

        items: items.map(item => ({

            productId: item.productId,

            quantity: Number(item.quantity),

            unitPrice: Number(item.unitPrice),

            discountPercent: Number(item.discountPercent),

            taxRate: Number(item.taxRate)

        }))

    };

    const response = await apiFetch(`/sales/estimates/${id}`, {

        method: "PUT",

        body: JSON.stringify(request)

    });

    if (!response.ok)
        throw new Error("Failed to update Estimate.");

    return await response.json();

}