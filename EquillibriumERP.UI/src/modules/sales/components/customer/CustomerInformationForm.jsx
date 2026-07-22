import {
    ERPField,
    ERPForm
} from "../../../../components/erp";


export default function CustomerInformationForm({

    customer,

    categories = [],

    onChange,

    isEditing = true,

    isNew = true

}) {

    return (

        <ERPForm>


            <ERPField
                label="Customer Code"
                required
            >

                <input

                    value={customer.customerCode ?? ""}

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...customer,

                            customerCode:
                                e.target.value

                        })

                    }

                />

            </ERPField>



            <ERPField
                label="Customer Name"
                required
            >

                <input

                    value={customer.name ?? ""}

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...customer,

                            name:
                                e.target.value

                        })

                    }

                />

            </ERPField>



            <ERPField
                label="Customer Type"
            >

                <select

                    value={customer.customerType ?? 0}

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...customer,

                            customerType:
                                Number(e.target.value)

                        })

                    }

                >

                    <option value={0}>
                        Business
                    </option>

                    <option value={1}>
                        Individual
                    </option>

                    <option value={2}>
                        Government
                    </option>


                </select>

            </ERPField>



            <ERPField
                label="Customer Category"
            >

                <select

                    value={
                        customer.customerCategoryId ?? ""
                    }

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...customer,

                            customerCategoryId:
                                e.target.value

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



            <ERPField
                label="Registration Number"
            >

                <input

                    value={
                        customer.registrationNumber ?? ""
                    }

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...customer,

                            registrationNumber:
                                e.target.value

                        })

                    }

                />

            </ERPField>



            <ERPField
                label="VAT Number"
            >

                <input

                    value={
                        customer.vatNumber ?? ""
                    }

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...customer,

                            vatNumber:
                                e.target.value

                        })

                    }

                />

            </ERPField>



            <ERPField
                label="Tax Number"
            >

                <input

                    value={
                        customer.taxNumber ?? ""
                    }

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...customer,

                            taxNumber:
                                e.target.value

                        })

                    }

                />

            </ERPField>



            <ERPField
                label="Credit Limit"
            >

                <input

                    type="number"

                    value={
                        customer.creditLimit ?? 0
                    }

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...customer,

                            creditLimit:
                                Number(e.target.value)

                        })

                    }

                />

            </ERPField>



            <ERPField
                label="Payment Terms"
            >

                <input

                    type="number"

                    value={
                        customer.paymentTerms ?? 0
                    }

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...customer,

                            paymentTerms:
                                Number(e.target.value)

                        })

                    }

                />

            </ERPField>


        </ERPForm>

    );

}