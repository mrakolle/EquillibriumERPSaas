import {
    ERPField,
    ERPForm
} from "../../../components/erp";

export default function SupplierContactInformationTab({

    supplier,

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

                    value={supplier.email ?? ""}

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...supplier,

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

                    value={supplier.phone ?? ""}

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...supplier,

                            phone: e.target.value

                        })

                    }

                />

            </ERPField>

            <ERPField
                label="Mobile"
            >

                <input

                    value={supplier.mobile ?? ""}

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...supplier,

                            mobile: e.target.value

                        })

                    }

                />

            </ERPField>

            <ERPField
                label="Website"
            >

                <input

                    value={supplier.website ?? ""}

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...supplier,

                            website: e.target.value

                        })

                    }

                />

            </ERPField>

        </ERPForm>

    );

}