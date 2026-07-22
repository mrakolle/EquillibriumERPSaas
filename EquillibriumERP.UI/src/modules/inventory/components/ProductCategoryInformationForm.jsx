import { ERPField } from "../../../components/erp";

export default function ProductCategoryInformationForm({

    category,

    onChange,

    isEditing = true,

    isNew = true

}) {

    function update(field, value) {

        if (!onChange) {

            return;

        }

        onChange({

            ...category,

            [field]: value

        });

    }

    return (

        <div>

            <ERPField
                label="Category Name"
                required
            >

                <input
                    value={category.name ?? ""}
                    disabled={!isEditing}
                    onChange={(e) =>
                        update("name", e.target.value)
                    }
                />

            </ERPField>

            <ERPField
                label="Product Type"
                required
            >

                <select
                    value={category.productType ?? 0}
                    disabled={!isEditing}
                    onChange={(e) =>
                        update(
                            "productType",
                            Number(e.target.value)
                        )
                    }
                >

                    <option value={0}>
                        Raw Material
                    </option>

                    <option value={1}>
                        Manufactured Product
                    </option>

                </select>

            </ERPField>

            <ERPField
                label="Description"
            >

                <textarea
                    rows={3}
                    value={category.description ?? ""}
                    disabled={!isEditing}
                    onChange={(e) =>
                        update(
                            "description",
                            e.target.value
                        )
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
                                checked={category.isActive ?? false}
                                disabled={!isEditing}
                                onChange={(e) =>
                                    update(
                                        "isActive",
                                        e.target.checked
                                    )
                                }
                            />

                        </div>

                    </ERPField>

                )

            }

        </div>

    );

}