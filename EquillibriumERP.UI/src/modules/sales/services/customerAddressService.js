import { apiFetch } from "../../authentication/services/apiClient";

export async function getCustomerAddresses(customerId) {

    const response = await apiFetch(
        `/sales/customer-addresses/get-all/${customerId}`
    );

    if (!response.ok) {
        throw new Error("Failed to load customer addresses.");
    }

    return await response.json();
}

export async function getCustomerAddressById(id) {

    const response = await apiFetch(
        `/sales/customer-addresses/get-by/${id}`
    );

    if (!response.ok) {
        throw new Error("Failed to load customer address.");
    }

    return await response.json();
}

export async function createCustomerAddress(request) {

    const response = await apiFetch(
        "/sales/customer-addresses/create",
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(request)
        });

    if (!response.ok) {
        throw new Error("Failed to create customer address.");
    }

    return await response.json();
}

export async function updateCustomerAddress(id, request) {

    const response = await apiFetch(
        `/sales/customer-addresses/update/${id}`,
        {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(request)
        });

    if (!response.ok) {
        throw new Error("Failed to update customer address.");
    }

    return await response.json();
}

export async function deleteCustomerAddress(id) {

    const response = await apiFetch(
        `/sales/customer-addresses/delete/${id}`,
        {
            method: "DELETE"
        });

    if (!response.ok) {
        throw new Error("Failed to delete customer address.");
    }
}