import { apiFetch } from "../../authentication/services/apiClient";

export async function getAllSupplierCategories() {

    const response = await apiFetch("/purchasing/supplier-categories/get-all");

    if (!response.ok) {

        const text = await response.text();

        console.error("Status:", response.status);
        console.error("Response:", text);

        throw new Error("Failed to load supplier categories.");

    }

    return await response.json();

}