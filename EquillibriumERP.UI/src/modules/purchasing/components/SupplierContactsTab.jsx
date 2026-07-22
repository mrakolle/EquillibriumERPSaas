import { useEffect, useState } from "react";

import {
    ERPButton,
    ERPField,
    ERPForm,
    ERPTable
} from "../../../components/erp";

import {
    createSupplierContact,
    getSupplierContacts
} from "../services/supplierContactService";

export default function SupplierContactsTab({

    supplier,

    isEditing

}) {

    const [contacts, setContacts] = useState([]);

    const [isAdding, setIsAdding] = useState(false);

    const [contact, setContact] = useState({

        supplierId: "",

        firstName: "",

        lastName: "",

        position: "",

        email: "",

        phone: "",

        isPrimary: false

    });

    useEffect(() => {

        if (!supplier?.id) {

            setContacts([]);

            return;

        }

        loadContacts();

    }, [supplier?.id]);

    async function loadContacts() {

        try {

            const data = await getSupplierContacts(supplier.id);

            setContacts(data);

        }
        catch (error) {

            console.error(error);

        }

    }

    function beginAdd() {

        setContact({

            supplierId: supplier.id,

            firstName: "",

            lastName: "",

            position: "",

            email: "",

            phone: "",

            isPrimary: false

        });

        setIsAdding(true);

    }

    async function saveContact() {

        try {

            await createSupplierContact(contact);

            setIsAdding(false);

            await loadContacts();

        }
        catch (error) {

            console.error(error);

            alert("Failed to save contact.");

        }

    }

    const columns = [

        {
            header: "First Name",
            accessor: "firstName"
        },

        {
            header: "Last Name",
            accessor: "lastName"
        },

        {
            header: "Position",
            accessor: "position"
        },

        {
            header: "Email",
            accessor: "email"
        },

        {
            header: "Phone",
            accessor: "phone"
        },

        {
            header: "Primary",
            accessor: row => row.isPrimary ? "Yes" : ""
        }

    ];

    if (!supplier?.id) {

        return (

            <div className="erp-table">

                Save the supplier before adding contacts.

            </div>

        );

    }

    return (

        <>

            <ERPTable

                columns={columns}

                data={contacts}

                emptyMessage="No contacts captured."

            />

            {

                isEditing && !isAdding && (

                    <ERPButton

                        onClick={beginAdd}

                    >

                        Add Contact

                    </ERPButton>

                )

            }

            {

                isAdding && (

                    <ERPForm>

                        <ERPField label="First Name">

                            <input

                                value={contact.firstName}

                                onChange={(e) =>

                                    setContact({

                                        ...contact,

                                        firstName: e.target.value

                                    })

                                }

                            />

                        </ERPField>

                        <ERPField label="Last Name">

                            <input

                                value={contact.lastName}

                                onChange={(e) =>

                                    setContact({

                                        ...contact,

                                        lastName: e.target.value

                                    })

                                }

                            />

                        </ERPField>

                        <ERPField label="Position">

                            <input

                                value={contact.position}

                                onChange={(e) =>

                                    setContact({

                                        ...contact,

                                        position: e.target.value

                                    })

                                }

                            />

                        </ERPField>

                        <ERPField label="Email">

                            <input

                                value={contact.email}

                                onChange={(e) =>

                                    setContact({

                                        ...contact,

                                        email: e.target.value

                                    })

                                }

                            />

                        </ERPField>

                        <ERPField label="Phone">

                            <input

                                value={contact.phone}

                                onChange={(e) =>

                                    setContact({

                                        ...contact,

                                        phone: e.target.value

                                    })

                                }

                            />

                        </ERPField>

                        <label>

                            <input

                                type="checkbox"

                                checked={contact.isPrimary}

                                onChange={(e) =>

                                    setContact({

                                        ...contact,

                                        isPrimary: e.target.checked

                                    })

                                }

                            />

                            Primary Contact

                        </label>

                        <div className="erp-actions">

                            <ERPButton

                                variant="secondary"

                                onClick={() => setIsAdding(false)}

                            >

                                Cancel

                            </ERPButton>

                            <ERPButton

                                onClick={saveContact}

                            >

                                Save Contact

                            </ERPButton>

                        </div>

                    </ERPForm>

                )

            }

        </>

    );

}