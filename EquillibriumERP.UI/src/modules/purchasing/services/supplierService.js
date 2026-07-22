import { apiFetch } from "../../authentication/services/apiClient";

export async function getAllSuppliers() {

    const response = await apiFetch("/purchasing/suppliers/get-all");

    if (!response.ok) {

        const text = await response.text();

        console.error("Status:", response.status);
        console.error("Response:", text);

        throw new Error("Failed to load suppliers.");

    }

    return await response.json();

}

export async function getSupplierById(id) {

    const response = await apiFetch(`/purchasing/suppliers/get-by/${id}`);

    if (!response.ok) {

        throw new Error("Failed to load supplier.");

    }

    return await response.json();

}

export async function createSupplier(request) {

    const response = await apiFetch("/purchasing/suppliers/create", {

        method: "POST",

        headers: {

            "Content-Type": "application/json"

        },

        body: JSON.stringify(request)

    });

    if (!response.ok) {

        const text = await response.text();

        console.error("API Error:", response.status);
        console.error(text);

        throw new Error("Failed to create supplier.");

    }

    return await response.json();

}

export async function updateSupplier(id, request) {

    const response = await apiFetch(`/purchasing/suppliers/update/${id}`, {

        method: "PUT",

        headers: {

            "Content-Type": "application/json"

        },

        body: JSON.stringify(request)

    });

    if (!response.ok) {

        const text = await response.text();

        console.error("API Error:", response.status);
        console.error(text);

        throw new Error("Failed to update supplier.");

    }

    return await response.json();

}

export async function deleteSupplier(id) {

    const response = await apiFetch(`/purchasing/suppliers/delete/${id}`, {

        method: "DELETE"

    });

    if (!response.ok) {

        throw new Error("Failed to delete supplier.");

    }

}