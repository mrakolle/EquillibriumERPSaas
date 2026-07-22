import {

    ERPField,
    ERPForm

} from "../../../../components/erp";


export default function CustomerCategoryInformationForm({

    category,

    onChange,

    isEditing = true,

    isNew = true

}) {

    return (

        <ERPForm>

            <ERPField
                label="Category Code"
                required
            >

                <input

                    value={category?.code ?? ""}

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...category,

                            code: e.target.value

                        })

                    }

                />

            </ERPField>


            <ERPField
                label="Category Name"
                required
            >

                <input

                    value={category?.name ?? ""}

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...category,

                            name: e.target.value

                        })

                    }

                />

            </ERPField>


            <ERPField
                label="Description"
                className="erp-form-full"
            >

                <textarea

                    rows={3}

                    value={category?.description ?? ""}

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...category,

                            description: e.target.value

                        })

                    }

                />

            </ERPField>


            <ERPField
                label="Sort Order"
            >

                <input

                    type="number"

                    value={category?.sortOrder ?? 0}

                    disabled={!isEditing}

                    onChange={(e) =>

                        onChange({

                            ...category,

                            sortOrder: Number(e.target.value)

                        })

                    }

                />

            </ERPField>


            {

                !isNew && (

                    <ERPField>

                        <div className="erp-inline-checkbox">

                            <label>

                                Active

                            </label>

                            <input

                                type="checkbox"

                                checked={category?.isActive ?? true}

                                disabled={!isEditing}

                                onChange={(e) =>

                                    onChange({

                                        ...category,

                                        isActive: e.target.checked

                                    })

                                }

                            />

                        </div>

                    </ERPField>

                )

            }

        </ERPForm>

    );

}