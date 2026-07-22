import { apiFetch } from "../../authentication/services/apiClient";

export async function getWorkOrders() {
    const response = await apiFetch("/manufacturing/workorders/GetAll");

    if (!response.ok)
        throw new Error("Failed to load Work Orders.");

    return await response.json();
}

export async function getWorkOrder(id) {
    const response = await apiFetch(`/manufacturing/workorders/${id}`);

    if (!response.ok)
        throw new Error("Failed to load Work Order.");

    return await response.json();
}

export async function createWorkOrder(request) {
    const response = await apiFetch("/manufacturing/workorders/Create", {
        method: "POST",
        body: JSON.stringify(request)
    });

    if (!response.ok)
        throw new Error("Failed to create Work Order.");

    return await response.json();
}

export async function updateWorkOrder(id, request) {
    const response = await apiFetch(`/manufacturing/workorders/${id}`, {
        method: "PUT",
        body: JSON.stringify(request)
    });

    if (!response.ok)
        throw new Error("Failed to update Work Order.");

    return await response.json();
}

export async function deleteWorkOrder(id) {
    const response = await apiFetch(`/manufacturing/workorders/${id}`, {
        method: "DELETE"
    });

    if (!response.ok)
        throw new Error("Failed to delete Work Order.");
}

export async function changeWorkOrderStatus(id, status) {
    const response = await apiFetch(`/manufacturing/workorders/${id}/status`, {
        method: "PATCH",
        body: JSON.stringify({ status })
    });

    if (!response.ok)
        throw new Error("Failed to change Work Order status.");

    return await response.json();
}
export async function startWorkOrder(id) {

    const response = await apiFetch(
        `/manufacturing/workorders/${id}/start`,
        {
            method: "POST"
        });

    if (!response.ok)
        throw new Error("Failed to start Work Order.");

}