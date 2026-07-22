import { apiFetch } from "../../authentication/services/apiClient";

export async function createTenant(request) {

    const response = await apiFetch("/onboarding/tenants", {

        method: "POST",

        headers: {
            "Content-Type": "application/json"
        },

        body: JSON.stringify(request)

    });

    if (!response.ok) {

        throw new Error("Failed to create ERP.");

    }

    return await response.json();

}