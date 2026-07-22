import { apiFetch } from "./apiClient";

export async function login(request) {
    const response = await apiFetch("/auth/login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(request)
    });

    if (!response.ok) {
        throw new Error("Invalid email or password.");
    }

    return await response.json();
}