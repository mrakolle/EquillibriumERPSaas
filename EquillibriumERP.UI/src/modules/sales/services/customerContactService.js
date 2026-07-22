import { apiFetch } from "../../authentication/services/apiClient";

export async function getCustomerContacts(customerId) {

    const response = await apiFetch(
        `/sales/customer-contacts/get-all/${customerId}`
    );

    if (!response.ok) {
        throw new Error("Failed to load customer contacts.");
    }

    return await response.json();
}

export async function getCustomerContactById(id) {

    const response = await apiFetch(
        `/sales/customer-contacts/get-by/${id}`
    );

    if (!response.ok) {
        throw new Error("Failed to load customer contact.");
    }

    return await response.json();
}

export async function createCustomerContact(request) {

    const response = await apiFetch(
        "/sales/customer-contacts/create",
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(request)
        });

    if (!response.ok) {
        throw new Error("Failed to create customer contact.");
    }

    return await response.json();
}

export async function updateCustomerContact(id, request) {

    const response = await apiFetch(
        `/sales/customer-contacts/update/${id}`,
        {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(request)
        });

    if (!response.ok) {
        throw new Error("Failed to update customer contact.");
    }

    return await response.json();
}

export async function deleteCustomerContact(id) {

    const response = await apiFetch(
        `/sales/customer-contacts/delete/${id}`,
        {
            method: "DELETE"
        });

    if (!response.ok) {
        throw new Error("Failed to delete customer contact.");
    }
}