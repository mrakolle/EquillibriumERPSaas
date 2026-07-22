import { apiFetch } from "../../authentication/services/apiClient";

export async function getAllProductCategories() {

    const response = await apiFetch("/products/product-categories/get-all");

    if (!response.ok) {

        throw new Error("Failed to load product categories.");

    }

    return await response.json();

}

export async function getProductCategoryById(id) {

    const response = await apiFetch(`/products/product-categories/get-by/${id}`);

    if (!response.ok) {

        throw new Error("Failed to load product category.");

    }

    return await response.json();

}

export async function createProductCategory(request) {

    const response = await apiFetch("/products/product-categories/create", {

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

        throw new Error(text);

    }

        return await response.json();

    }

export async function updateProductCategory(id, request) {

    const response = await apiFetch(`/products/product-categories/update/${id}`, {

        method: "PUT",

        headers: {

            "Content-Type": "application/json"

        },

        body: JSON.stringify(request)

    });

    if (!response.ok) {

        throw new Error("Failed to update product category.");

    }

    return await response.json();

}

export async function deleteProductCategory(id) {

    const response = await apiFetch(`/products/product-categories/delete/${id}`, {

        method: "DELETE"

    });

    if (!response.ok) {

        throw new Error("Failed to delete product category.");

    }

}