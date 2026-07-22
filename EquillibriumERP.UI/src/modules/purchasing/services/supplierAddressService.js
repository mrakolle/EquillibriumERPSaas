import { apiFetch } from "../../authentication/services/apiClient";

export async function getSupplierAddresses(supplierId) {

    const response = await apiFetch(
        `/purchasing/supplier-addresses/get-by-supplier/${supplierId}`
    );

    if (!response.ok) {

        throw new Error("Failed to load supplier addresses.");

    }

    return await response.json();

}

export async function createSupplierAddress(request) {

    const response = await apiFetch(
        "/purchasing/supplier-addresses/create",
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

        throw new Error("Failed to create supplier address.");

    }

    return await response.json();

}