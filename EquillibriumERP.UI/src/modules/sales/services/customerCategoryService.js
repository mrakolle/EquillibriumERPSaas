import { apiFetch } from "../../authentication/services/apiClient";


export async function getAllCustomerCategories() {

    const response = await apiFetch(
        "/sales/customer-categories/get-all"
    );


    console.log(
        "Customer Categories Status:",
        response.status
    );


    const text = await response.text();


    console.log(
        "Customer Categories Response:",
        text
    );


    if (!response.ok) {

        throw new Error(
            "Failed to load customer categories."
        );

    }


    return text
        ? JSON.parse(text)
        : [];

}



export async function getCustomerCategoryById(id) {

    const response = await apiFetch(
        `/sales/customer-categories/get-by/${id}`
    );


    if (!response.ok) {

        throw new Error(
            "Failed to load customer category."
        );

    }


    return await response.json();

}



export async function createCustomerCategory(request) {

    const response = await apiFetch("/sales/customer-categories/create", {
    method: "POST",
    headers: {
        "Content-Type": "application/json"
    },
        body: JSON.stringify(request)
    });

    if (!response.ok) {

        const text = await response.text();

        console.error("Status:", response.status);
        console.error("Response:", text);

        throw new Error("Failed to create customer category.");
    }

    return await response.json();

}



export async function updateCustomerCategory(
    id,
    request
) {

    const response = await apiFetch(
        `/sales/customer-categories/update/${id}`,
        {

            method: "PUT",

            headers: {

                "Content-Type": "application/json"

            },

            body: JSON.stringify(request)

        }
    );


    if (!response.ok) {

        throw new Error(
            "Failed to update customer category."
        );

    }


    return await response.json();

}



export async function deleteCustomerCategory(id) {

    const response = await apiFetch(
        `/sales/customer-categories/delete/${id}`,
        {

            method: "DELETE"

        }
    );


    if (!response.ok) {

        throw new Error(
            "Failed to delete customer category."
        );

    }

}