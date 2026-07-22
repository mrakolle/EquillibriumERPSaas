import { apiFetch } from "../../authentication/services/apiClient";

export async function getAllProducts() {
    const response = await apiFetch("/products/get-all");

    console.log("Products status:", response.status);

    if (!response.ok) {
        const text = await response.text();
        console.log("Products response:", text);
        throw new Error("Failed to load products.");
    }

    return await response.json();
}
export async function getProductById(productId) {
    const response = await apiFetch(`/products/get-by/${productId}`);

    if (!response.ok) {
        throw new Error("Failed to load product.");
    }

    return await response.json();
}
export async function createProduct(request) {
    const response = await apiFetch("/products/create", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(request)
    });

    if (!response.ok) {
        throw new Error("Failed to create product.");
    }

    return await response.json();
}
export async function updateProduct(id, request) {
    const response = await apiFetch(`/products/update/${id}`, {
        method: "PUT",
        body: JSON.stringify(request),
    });

    if (!response.ok) {
        const text = await response.text();
        console.log("Update response:", text);
        throw new Error("Failed to update product.");
    }

    return await response.json();
}