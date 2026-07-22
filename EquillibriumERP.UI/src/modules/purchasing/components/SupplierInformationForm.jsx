import {
    ERPField,
    ERPForm
} from "../../../components/erp";

export default function SupplierInformationForm({

    supplier,

    categories = [],

    onChange,

    isEditing = true

}) {

    return (

        <ERPForm>

            <ERPField
                label="Supplier Code"
            >

                <input

                    value={supplier.supplierCode ?? ""}

                    disabled={true}

                />

            </ERPField>

            <ERPField
                label="Supplier Name"
                required
            >

                <input

                    value={supplier.name ?? ""}

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...supplier,

                            name: e.target.value

                        })

                    }

                />

            </ERPField>

            <ERPField
                label="Supplier Category"
            >

                <select

                    value={supplier.supplierCategoryId ?? ""}

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...supplier,

                            supplierCategoryId: e.target.value

                        })

                    }

                >

                    <option value="">
                        Select Category...
                    </option>

                    {

                        categories.map(category => (

                            <option
                                key={category.id}
                                value={category.id}
                            >

                                {category.name}

                            </option>

                        ))

                    }

                </select>

            </ERPField>

            <ERPField label="Registration Number">

                <input

                    value={supplier.registrationNumber ?? ""}

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...supplier,

                            registrationNumber: e.target.value

                        })

                    }

                />

            </ERPField>

            <ERPField label="VAT Number">

                <input

                    value={supplier.vatNumber ?? ""}

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...supplier,

                            vatNumber: e.target.value

                        })

                    }

                />

            </ERPField>

            <ERPField label="Tax Number">

                <input

                    value={supplier.taxNumber ?? ""}

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...supplier,

                            taxNumber: e.target.value

                        })

                    }

                />

            </ERPField>

            <ERPField label="Payment Terms">

                <input

                    type="number"

                    value={supplier.paymentTerms ?? 0}

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...supplier,

                            paymentTerms: Number(e.target.value)

                        })

                    }

                />

            </ERPField>

        </ERPForm>

    );

}