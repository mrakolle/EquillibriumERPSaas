

import { apiFetch } from "../../src/modules/authentication/services/apiClient";

export async function getAllBoms() {
    const response = await apiFetch("/manufacturing/bom/GetAll");

    if (!response.ok)
        throw new Error("Failed to load BOMs.");

    return await response.json();
}

export async function getBom(id) {
    const response = await apiFetch(`/manufacturing/bom/${id}`);

    if (!response.ok)
        throw new Error("Failed to load BOM.");

    return await response.json();
}

export async function createBom(request) {
    const response = await apiFetch("/manufacturing/bom/Create", {
        method: "POST",
        body: JSON.stringify(request)
    });

    if (!response.ok)
        throw new Error("Failed to create BOM.");

    return await response.json();
}

export async function updateBom(id, request) {
    const response = await apiFetch(`/manufacturing/bom/${id}`, {
        method: "PUT",
        body: JSON.stringify(request)
    });

    if (!response.ok)
        throw new Error("Failed to update BOM.");
}