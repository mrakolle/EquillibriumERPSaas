import { useEffect, useState } from "react";

import "./ProcessStepsTab.css";

import {
    ERPButton,
    ERPTable
} from "../../../components/erp";

import { FaPlus, FaTrash } from "react-icons/fa";

import { getAllProducts } from "../../inventory/services/productService";


export default function ProcessStepsTab({
    steps,
    onChange,
    isEditing
}) {

    const STEP_TYPES = [
        { value: 1, label: "Preparation" },
        { value: 2, label: "Add Material" },
        { value: 3, label: "Mixing" },
        { value: 4, label: "Inspection" },
        { value: 5, label: "Packaging" }
    ];


    const processSteps = steps ?? [];

    const [products, setProducts] = useState([]);


    useEffect(() => {

        loadProducts();

    }, []);

å
    async function loadProducts() {

        try {

            const data = await getAllProducts();

            setProducts(data);

        }
        catch (error) {

            console.error(error);

        }

    }


    function addStep() {

        onChange([
            ...processSteps,
            {
                id: crypto.randomUUID(),
                stepNumber: processSteps.length + 1,
                description: "",
                durationMinutes: 0,
                type: "",
                rawMaterialProductId: "",
                quantityPercentage: 0.0000
            }
        ]);

    }


    function removeStep(id) {

        const updated = processSteps
            .filter(step => step.id !== id)
            .map((step, index) => ({
                ...step,
                stepNumber: index + 1
            }));

        onChange(updated);

    }


    function updateStep(id, field, value) {

        onChange(

            processSteps.map(step =>

                step.id === id
                    ? { ...step, [field]: value }
                    : step

            )

        );

    }


    return (

        <>

            {
                isEditing && (

                    <ERPButton
                        variant="primary"
                        onClick={addStep}
                    >
                        <FaPlus />
                        Add Step
                    </ERPButton>

                )
            }


            <ERPTable>

                <thead>

                    <tr>

                        <th className="erp-table-center">
                            #
                        </th>

                        <th>
                            Type
                        </th>

                        <th>
                            Description
                        </th>

                        <th>
                            Duration (min)
                        </th>

                        <th>
                            Raw Material
                        </th>

                        <th>
                            Qty %
                        </th>

                        {
                            isEditing && (

                                <th className="erp-table-actions">
                                    Action
                                </th>

                            )
                        }

                    </tr>

                </thead>


                <tbody>


                    {

                        processSteps.length === 0 && (

                            <tr>

                                <td
                                    colSpan={isEditing ? 7 : 6}
                                    className="erp-table-center"
                                >
                                    No process steps defined.
                                </td>

                            </tr>

                        )

                    }


                    {

                        processSteps.map(step => (

                            <tr key={step.id}>


                                <td className="erp-table-center">

                                    {
                                        isEditing ? (

                                            <input
                                                type="number"
                                                style={{ width: "55px" }}
                                                min="1"
                                                value={step.stepNumber}
                                                onChange={(e) =>
                                                    updateStep(
                                                        step.id,
                                                        "stepNumber",
                                                        Number(e.target.value)
                                                    )
                                                }
                                            />

                                        ) : (

                                            step.stepNumber

                                        )
                                    }

                                </td>



                                <td>

                                    {
                                        isEditing ? (

                                            <select
                                                value={step.type}
                                                onChange={(e) =>
                                                    updateStep(
                                                        step.id,
                                                        "type",
                                                        Number(e.target.value)
                                                    )
                                                }
                                            >

                                                <option value="">
                                                    Select...
                                                </option>

                                                {
                                                    STEP_TYPES.map(type => (

                                                        <option
                                                            key={type.value}
                                                            value={type.value}
                                                        >
                                                            {type.label}
                                                        </option>

                                                    ))
                                                }

                                            </select>

                                        ) : (

                                            STEP_TYPES.find(
                                                t => t.value === Number(step.type)
                                            )?.label ?? ""

                                        )
                                    }

                                </td>



                                <td>

                                    {
                                        isEditing ? (

                                            <textarea
                                                rows={2}
                                                value={step.description}
                                                onChange={(e) =>
                                                    updateStep(
                                                        step.id,
                                                        "description",
                                                        e.target.value
                                                    )
                                                }
                                            />

                                        ) : (

                                            step.description

                                        )
                                    }

                                </td>



                                <td>

                                    {
                                        isEditing ? (

                                            <input
                                                type="time"
                                                step="1"
                                                value={
                                                    step.duration ??
                                                    "00:00:00"
                                                }
                                                onChange={(e) =>
                                                    updateStep(
                                                        step.id,
                                                        "duration",
                                                        e.target.value
                                                    )
                                                }
                                            />

                                        ) : (

                                            step.duration

                                        )
                                    }

                                </td>



                                <td>

                                    {
                                        isEditing ? (

                                            <select
                                                value={
                                                    step.rawMaterialProductId ?? ""
                                                }
                                                onChange={(e) =>
                                                    updateStep(
                                                        step.id,
                                                        "rawMaterialProductId",
                                                        e.target.value
                                                    )
                                                }
                                            >

                                                <option value="">
                                                    None
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

                                            step.rawMaterialName ?? "-"

                                        )
                                    }

                                </td>



                                <td>

                                    {
                                        isEditing ? (

                                            <input
                                                type="number"
                                                step="0.0001"
                                                style={{ width: "75px" }}
                                                value={
                                                    step.quantityPercentage ?? 0
                                                }
                                                onChange={(e) =>
                                                    updateStep(
                                                        step.id,
                                                        "quantityPercentage",
                                                        Number(e.target.value)
                                                    )
                                                }
                                            />

                                        ) : (

                                            Number(
                                                step.quantityPercentage
                                            ).toFixed(4)

                                        )
                                    }

                                </td>



                                {

                                    isEditing && (

                                        <td className="erp-table-actions">

                                            <button
                                                type="button"
                                                className="erp-icon-button"
                                                onClick={() => removeStep(step.id)}
                                                title="Delete Step"
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