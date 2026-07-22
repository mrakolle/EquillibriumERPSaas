import {
    ERPField
} from "../../../components/erp";

import "./BomInformationForm.css";

export default function BomInformationForm({
    bom,
    products = [],
    onChange,
    isEditing = false,
    isNew = false
}) {

    return (

        <div>

            <ERPField
                label="Product"
                required
            >

                {

                    isNew ? (

                        <select
                            value={bom.productId}
                            onChange={(e) =>
                                onChange({
                                    ...bom,
                                    productId: e.target.value
                                })
                            }
                        >

                            <option value="">
                                Select Final Product...
                            </option>

                            {

                                products.map(product => (

                                    <option
                                        key={product.id}
                                        value={product.id}
                                    >
                                        {product.name}
                                    </option>

                                ))

                            }

                        </select>

                    ) : (

                        <input
                            value={bom.name ?? ""}
                            disabled
                        />

                    )

                }

            </ERPField>

            <ERPField
                label="BOM Code"
                required
            >

                <input
                    value={bom.code ?? ""}
                    disabled={!isEditing}
                    onChange={(e) =>
                        onChange({
                            ...bom,
                            code: e.target.value
                        })
                    }
                />

            </ERPField>

            <ERPField
                label="Description"
            >

                <textarea
                    rows={2}
                    value={bom.description ?? ""}
                    disabled={!isEditing}
                    onChange={(e) =>
                        onChange({
                            ...bom,
                            description: e.target.value
                        })
                    }
                />

            </ERPField>

            {

                !isNew && (

                    <div className="erp-inline-checkbox">

                        <label htmlFor="bom-active">
                            Active
                        </label>
                        <input
                            id="bom-active"
                            type="checkbox"
                            checked={bom.isActive}
                            disabled={!isEditing}
                            onChange={(e) =>
                                onChange({
                                    ...bom,
                                    isActive: e.target.checked
                                })
                            }
                        />

                    </div>

                )

            }

        </div>

    );

}