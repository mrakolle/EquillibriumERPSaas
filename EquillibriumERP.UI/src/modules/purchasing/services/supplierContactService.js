import { apiFetch } from "../../authentication/services/apiClient";

export async function getSupplierContacts(supplierId) {

    const response = await apiFetch(
        `/purchasing/supplier-contacts/get-by-supplier/${supplierId}`
    );

    if (!response.ok) {

        throw new Error("Failed to load supplier contacts.");

    }

    return await response.json();

}

export async function createSupplierContact(request) {

    const response = await apiFetch(
        "/purchasing/supplier-contacts/create",
        {

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

        throw new Error("Failed to create supplier contact.");

    }

    return await response.json();

}