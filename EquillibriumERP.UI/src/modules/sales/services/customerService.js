import { apiFetch } from "../../authentication/services/apiClient";

export async function getAllCustomers() {

    const response = await apiFetch("/sales/customers/get-all");

    if (!response.ok) {

        const text = await response.text();

        console.error("Status:", response.status);
        console.error("Response:", text);

        throw new Error("Failed to load customers.");

    }

    return await response.json();

}

export async function getCustomerById(id) {

    const response = await apiFetch(`/sales/customers/get-by/${id}`);

    if (!response.ok) {

        throw new Error("Failed to load customer.");

    }

    return await response.json();

}

export async function createCustomer(request) {

    const response = await apiFetch("/sales/customers/create", {

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

        throw new Error("Failed to create customer.");

    }

    return await response.json();

}

export async function updateCustomer(id, request) {

    const response = await apiFetch(`/sales/customers/update/${id}`, {

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

        throw new Error("Failed to update customer.");

    }

    return await response.json();

}

export async function deleteCustomer(id) {

    const response = await apiFetch(`/sales/customers/delete/${id}`, {

        method: "DELETE"

    });

    if (!response.ok) {

        throw new Error("Failed to delete customer.");

    }

}