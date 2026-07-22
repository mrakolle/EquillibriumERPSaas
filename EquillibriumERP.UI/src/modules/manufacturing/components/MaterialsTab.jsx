import { useEffect, useState } from "react";

import "./MaterialsTab.css";
import { ERPTable } from "../../../components/erp";

import { FaTrash, FaPlus } from "react-icons/fa";
import {
    ERPButton
} from "../../../components/erp";

import { getAllProducts } from "../../inventory/services/productService";

export default function MaterialsTab({
    items,
    onChange,
    isEditing
}) {

    const materials = items ?? [];

    const [products, setProducts] = useState([]);

    useEffect(() => {

        loadProducts();

    }, []);

    async function loadProducts() {

        try {

            const data = await getAllProducts();

            setProducts(data);

        }
        catch (error) {

            console.error(error);

        }

    }

    function addMaterial() {

       onChange([
            ...materials,
            {
                id: crypto.randomUUID(),
                rawMaterialProductId: "",
                rawMaterialName: "",
                quantity: 0.0000,
                unitOfMeasure: ""
            }
        ]);

    }

    function removeMaterial(id) {

        onChange(

            materials.filter(
                material => material.id !== id
            )

        );

    }

    function updateMaterial(id, field, value) {

        onChange(

            materials.map(material => {

                if (material.id !== id) {

                    return material;

                }

                if (field === "rawMaterialProductId") {

                    const product = products.find(
                        p => String(p.id) === String(value)
                    );

                    return {

                        ...material,

                        rawMaterialProductId: value,

                        rawMaterialName: product?.name ?? "",

                        unitOfMeasure: product?.unitOfMeasure ?? ""

                    };

                }

                return {

                    ...material,

                    [field]: value

                };

            })

        );

    }

    return (

        <>

            {
                isEditing && (

                    <ERPButton
                        size="small"
                        onClick={addMaterial}
                    >
                        <FaPlus />
                        Add Material
                    </ERPButton>

                )
            }

            <ERPTable className="materials-table">

                <thead>

                    <tr>

                        <th>Raw Material</th>
                        <th>Quantity</th>
                        <th>Unit</th>

                        {
                            isEditing && (
                                <th>Action</th>
                            )
                        }

                    </tr>

                </thead>

                <tbody>

                    {

                        materials.map(item => (

                            <tr key={item.id}>

                                <td>

                                    {

                                        isEditing ? (
                                            <select
                                                value={item.rawMaterialProductId ?? ""}
                                                onChange={(e) =>
                                                    updateMaterial(
                                                        item.id,
                                                        "rawMaterialProductId",
                                                        e.target.value
                                                    )
                                                }
                                            >

                                                <option value="">
                                                    Select Raw Material
                                                </option>

                                                {
                                                    products.map(product => (

                                                        <option
                                                            key={product.id}
                                                            value={product.id}
                                                        >
                                                            {product.code} - {product.name}
                                                        </option>

                                                    ))
                                                }

                                            </select>

                                        ) : (

                                            item.rawMaterialName

                                        )

                                    }

                                </td>

                                <td>

                                    {

                                        isEditing ? (

                                            <input
                                                type="number"
                                                step="0.0001"
                                                value={item.quantity}
                                                onChange={(e) =>
                                                    updateMaterial(
                                                        item.id,
                                                        "quantity",
                                                        Number(e.target.value)
                                                    )
                                                }
                                            />

                                        ) : (

                                            Number(item.quantity).toFixed(4)

                                        )

                                    }

                                </td>

                                <td>

                                    {

                                        isEditing ? (

                                            <select
                                                value={item.unitOfMeasure ?? ""}
                                                onChange={(e) =>
                                                    updateMaterial(
                                                        item.id,
                                                        "unitOfMeasure",
                                                        e.target.value
                                                    )
                                                }
                                            >
                                                <option value="">Select...</option>

                                                <option value="kg">kg</option>
                                                <option value="g">g</option>
                                                <option value="mg">mg</option>

                                                <option value="L">L</option>
                                                <option value="mL">mL</option>

                                                <option value="Each">Each</option>
                                            </select>
                                        ) : (

                                            item.unitOfMeasure

                                        )

                                    }

                                </td>

                                {

                                    isEditing && (

                                        <td>

                                            <button
                                                type="button"
                                                className="erp-icon-button"
                                                onClick={() => removeMaterial(item.id)}
                                            >
                                                <FaTrash />
                                            </button>

                                        </td>

                                    )

                                }

                            </tr>

                        ))

                    }

                </tbody>

            </ERPTable>

        </>

    );

}