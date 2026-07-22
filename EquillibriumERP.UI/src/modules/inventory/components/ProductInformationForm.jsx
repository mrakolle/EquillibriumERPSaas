import { useEffect, useState } from "react";

import {
    ERPField,
    ERPForm
} from "../../../components/erp";

import { getAllProductCategories } from "../services/productCategoryService";

export default function ProductInformationForm({

    product,

    onChange,

    isEditing = true,

    isNew = true

}) {

    const [categories, setCategories] = useState([]);

    useEffect(() => {

        loadCategories();

    }, []);

    async function loadCategories() {

        try {

            const data = await getAllProductCategories();

            setCategories(data);

        }
        catch (error) {

            console.error(error);

            setCategories([]);

        }

    }

    return (

        <div>

            <ERPField
                label="Product Code"
                required
            >

                <input
                    value={product.productCode ?? ""}
                    disabled={!isEditing}
                    onChange={(e) =>
                        onChange({
                            ...product,
                            productCode: e.target.value
                        })
                    }
                />

            </ERPField>

            <ERPField
                label="Product Name"
                required
            >

                <input
                    value={product.name ?? ""}
                    disabled={!isEditing}
                    onChange={(e) =>
                        onChange({
                            ...product,
                            name: e.target.value
                        })
                    }
                />

            </ERPField>

            <ERPField
                label="Product Type"
                required
            >

                <select
                    value={product.productType ?? 0}
                    disabled={!isEditing}
                    onChange={(e) =>
                        onChange({
                            ...product,
                            productType: Number(e.target.value)
                        })
                    }
                >

                    <option value={0}>
                        Raw Material
                    </option>

                    <option value={1}>
                        Manufactured
                    </option>

                </select>

            </ERPField>

            <ERPField
                label="Category"
                required
            >

                <select
                    value={product.productCategoryId ?? ""}
                    disabled={!isEditing}
                    onChange={(e) =>
                        onChange({
                            ...product,
                            productCategoryId: e.target.value
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
                label="Selling Price"
            >

                <input
                    type="number"
                    step="0.01"
                    value={product.sellingPrice ?? 0}
                    disabled={!isEditing}
                    onChange={(e) =>
                        onChange({
                            ...product,
                            sellingPrice: Number(e.target.value)
                        })
                    }
                />

            </ERPField>

            <ERPField
                label="Cost Price"
            >

                <input
                    type="number"
                    step="0.01"
                    value={product.costPrice ?? 0}
                    disabled={!isEditing}
                    onChange={(e) =>
                        onChange({
                            ...product,
                            costPrice: Number(e.target.value)
                        })
                    }
                />

            </ERPField>

            <ERPField
                label="CAS Number"
            >

                <input
                    value={product.casNumber ?? ""}
                    disabled={!isEditing}
                    onChange={(e) =>
                        onChange({
                            ...product,
                            casNumber: e.target.value
                        })
                    }
                />

            </ERPField>

            <div />

            <ERPField
                label="Description"
                className="erp-form-full"
            >

                <textarea
                    rows={2}
                    value={product.description ?? ""}
                    disabled={!isEditing}
                    onChange={(e) =>
                        onChange({
                            ...product,
                            description: e.target.value
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
                                checked={product.isActive}
                                disabled={!isEditing}
                                onChange={(e) =>
                                    onChange({
                                        ...product,
                                        isActive: e.target.checked
                                    })
                                }
                            />

                        </div>

                    </ERPField>

                )

            }

        </div>

    );

}