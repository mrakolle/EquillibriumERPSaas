import {
    ERPField,
    ERPForm
} from "../../../../components/erp";

export default function CustomerContactInformationTab({

    customer,

    onChange,

    isEditing

}) {

    return (

        <ERPForm>

            <ERPField
                label="Email"
                required
            >

                <input

                    type="email"

                    value={customer.email ?? ""}

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...customer,

                            email: e.target.value

                        })

                    }

                />

            </ERPField>

            <ERPField
                label="Phone"
                required
            >

                <input

                    value={customer.phone ?? ""}

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...customer,

                            phone: e.target.value

                        })

                    }

                />

            </ERPField>

            <ERPField
                label="Mobile"
            >

                <input

                    value={customer.mobile ?? ""}

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...customer,

                            mobile: e.target.value

                        })

                    }

                />

            </ERPField>

            <ERPField
                label="Website"
            >

                <input

                    value={customer.website ?? ""}

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...customer,

                            website: e.target.value

                        })

                    }

                />

            </ERPField>

        </ERPForm>

    );

}