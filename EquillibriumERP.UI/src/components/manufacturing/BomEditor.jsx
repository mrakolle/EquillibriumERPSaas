import { useEffect, useState } from "react";
import {getAllProducts,getProductById} from "../../modules/z-obsolete/services/productApi";
import {createBom,updateBom} from "../../modules/z-obsolete/services/bomApi";
import BomItemsEditor from "./BomItemsEditor";


import BomGeneralInformation from "./BomGeneralInformation";



function BomEditor({bom = null,onSaved,onCancel}) {
    const [code, setCode] =
        useState(bom?.code ?? "");

    const [description, setDescription] =
        useState(bom?.description ?? "");

    const [products, setProducts] =
        useState([]);

    const [manufacturedProductId, setManufacturedProductId] =
        useState(bom?.productId ?? "");

    const [manufacturedProductName, setManufacturedProductName] =
        useState(bom?.name ?? "");

    const [selectedRawMaterialId, setSelectedRawMaterialId] =
        useState("");

    const [items, setItems] =
        useState(bom?.items ?? []);

    const [steps, setSteps] =
        useState(bom?.steps ?? []);

    useEffect(() => {
        if (!bom) return;

        setCode(bom.code);
        setDescription(bom.description);
        setManufacturedProductId(bom.productId);
        setItems(
            (bom.items ?? []).map(item => ({
                id: item.id,
                rawMaterialId: item.rawMaterialProductId,
                rawMaterialName: item.rawMaterialName,
                quantity: item.quantity,
                uom: item.unitOfMeasure
            }))
        );
        setSteps(bom.steps ?? []);

    }, [bom]);
    console.log("Editor items:", items);
    function addItem(rawMaterialId) {
        if (!rawMaterialId) return;

        const exists = items.some(
            item => item.rawMaterialId === rawMaterialId
        );

        if (exists) {
            alert("This raw material has already been added.");
            return;
        }

        const product = products.find(
            p => p.id === rawMaterialId
        );

        if (!product) return;

        setItems(prev => [
            ...prev,
            {
                id: crypto.randomUUID(),
                product,
                rawMaterialId: product.id,
                rawMaterialName: product.name,
                quantity: "0.0000",
                uom: ""
            }
        ]);

        setSelectedRawMaterialId("");
    }

    function addStep() {
        const nextStepNumber =
            steps.length === 0
                ? 1
                : Math.max(...steps.map(s => s.stepNumber)) + 1;

        setSteps(prev => [
            ...prev,
            {
                stepNumber: nextStepNumber,
                description: "",
                durationMinutes: 0,
                type: 0,
                materials: []
            }
        ]);
    }

    function addStepMaterial(stepNumber) {
        setSteps(prev =>
            prev.map(step => {
                if (step.stepNumber !== stepNumber)
                    return step;

                return {
                    ...step,
                    materials: [
                        ...(step.materials ?? []),
                        {
                            rawMaterialProductId: "",
                            quantity: 0,
                            unitOfMeasure: ""
                        }
                    ]
                };
            })
        );
    }
            
    useEffect(() => {
        async function loadProducts() {
            try {
                const data = await getAllProducts();
                console.log(data);
                setProducts(data);
            } catch (error) {
                console.error("Failed to load products:", error);
            }
        }
        loadProducts();
    }, []);

    const longestNameLength =
    items.length === 0
        ? 15
        : Math.max(...items.map(item => item.rawMaterialName.length), 15);
            //<h2>New Bill of Material</h2>
    
    function buildRequest() {
        return {
            productId: manufacturedProductId,
            code,
            description,

            items: items.map(item => ({
                rawMaterialProductId: item.rawMaterialId,
                quantity: Number(item.quantity),
                unitOfMeasure: item.uom
            })),

            steps: steps.map(step => ({
                stepNumber: step.stepNumber,
                description: step.description,
                durationMinutes: step.durationMinutes,
                type: step.type,
                materials: step.materials ?? []
            }))
        };
    }       

    function clearForm() {
        setManufacturedProductId("");
        setCode("");
        setDescription("");

        setSelectedRawMaterialId("");
        setItems([]);
        setSteps([]);
    }
   async function saveBom() {
        const request = buildRequest();

        console.log("BOM Request:", request);

        if (bom) {
            await updateBom(bom.id, request);
        } else {
            await createBom(request);
            clearForm();
        }

        onSaved?.();
    }
   console.log({
        code,
        description
    });

    console.log("Editor Items:", items);
    console.log("manufacturedProductId:", manufacturedProductId);
    console.log("bom.productId:", bom?.productId);
    console.log("products:", products);
    return (
        <>
            <div>
                <label>Product</label>
                <br />
                <select
                    value={manufacturedProductId}
                    onChange={(e) => {
                        const id = e.target.value;
                        setManufacturedProductId(id);

                        const product = products.find(p => p.id === id);

                        setDescription(product?.description ?? "");
                        setCode(product?.productCode ?? "");
                    }}
                    style={{
                        width: "300px"
                    }}
                >
                    <option value="">Select Product...</option>

                    {products
                        .filter(p => p.productType === 1)
                        .map(product => (
                            <option key={product.id} value={product.id}>
                                {product.name}
                            </option>
                        ))}
                </select>
                
            </div>
            <div style={{ marginTop: 20 }}>
                <div style={{ marginTop: 15 }}>
                    <label>Description</label>
                    <br />
                    <textarea
                        value={description}
                        readOnly
                        rows={4}
                        style={{
                            width: "300px",
                            resize: "vertical"
                        }}
                    />
                </div>

                <div style={{ marginTop: 20 }}>
                    <h3>Raw Material</h3>

                    <select
                        value={selectedRawMaterialId}
                        onChange={(e) => {
                        const id = e.target.value;

                        setSelectedRawMaterialId(id);
                        addItem(id);
                        setSelectedRawMaterialId("");
                    }}

                        style={{
                            width: "300px"
                        }}
                    >
                        
                        <option value="">Select Raw Material...</option>

                        {products
                            .filter(p => p.productType === 0) // RawMaterial
                            .map((product) => (
                                <option key={product.id} value={product.id}>
                                    {product.name}
                                </option>
                            ))}
                    </select>
                </div>
                    <BomItemsEditor
                        items={items}
                        setItems={setItems}
                        longestNameLength={longestNameLength}
                />
    <div style={{ marginTop: 40 }}>
    <h3>Manufacturing Steps</h3>

    {steps.length === 0 && (
        <p>No manufacturing steps added.</p>
    )}

    {steps.map(step => (
        <div
            key={step.stepNumber}
            style={{
                border: "1px solid #ddd",
                padding: 15,
                marginBottom: 15
            }}
        >
            <div
                style={{
                    display: "flex",
                    gap: 10,
                    alignItems: "center"
                }}
            >
                <input
                    value={step.stepNumber}
                    readOnly
                    style={{ width: 60 }}
                />

                <input
                    placeholder="Step Description"
                    value={step.description}
                    onChange={(e) => {
                        const value = e.target.value;

                        setSteps(prev =>
                            prev.map(s =>
                                s.stepNumber === step.stepNumber
                                    ? { ...s, description: value }
                                    : s
                            )
                        );
                    }}
                    style={{ width: 300 }}
                />

                <select
                    value={step.type}
                    onChange={(e) => {
                        const value = Number(e.target.value);

                        setSteps(prev =>
                            prev.map(s =>
                                s.stepNumber === step.stepNumber
                                    ? { ...s, type: value }
                                    : s
                            )
                        );
                    }}
                >
                    <option value={0}>Preparation</option>
                    <option value={1}>Add Material</option>
                    <option value={2}>Mixing</option>
                    <option value={3}>Inspection</option>
                    <option value={4}>Packaging</option>
                </select>

                <input
                    type="number"
                    value={step.durationMinutes}
                    onChange={(e) => {
                        const value = Number(e.target.value);

                        setSteps(prev =>
                            prev.map(s =>
                                s.stepNumber === step.stepNumber
                                    ? { ...s, durationMinutes: value }
                                    : s
                            )
                        );
                    }}
                    style={{ width: 80 }}
                />

                <span>minutes</span>

                <button
                    type="button"
                    onClick={() =>
                        setSteps(prev =>
                            prev.filter(s => s.stepNumber !== step.stepNumber)
                        )
                    }
                >
                    Remove
                </button>
            </div>

            {step.type === 1 && (
                <div
                    style={{
                        marginTop: 15,
                        marginLeft: 70
                    }}
                >
                    <h4>Materials</h4>

                    {(step.materials ?? []).map((material, index) => (
                        <div
                            key={index}
                            style={{
                                display: "flex",
                                gap: 10,
                                marginBottom: 10
                            }}
                        >
                            <select
                                value={material.rawMaterialProductId}
                                style={{ width: 250 }}
                            >
                                <option value="">Select Raw Material...</option>

                                {items.map(item => (
                                    <option
                                        key={item.rawMaterialId}
                                        value={item.rawMaterialId}
                                    >
                                        {item.rawMaterialName}
                                    </option>
                                ))}
                            </select>

                            <input
                                type="number"
                                value={material.quantity}
                                style={{ width: 80 }}
                            />

                            <input
                                value={material.unitOfMeasure}
                                readOnly
                                style={{ width: 60 }}
                            />
                        </div>
                    ))}

                    <button
                        type="button"
                        onClick={() => addStepMaterial(step.stepNumber)}
                    >
                        Add Material
                    </button>
                </div>
            )}
        </div>
    ))}

    <button
        type="button"
        onClick={addStep}
        style={{ marginTop: 10 }}
    >
        Add Step
    </button>
</div>

                <div
                    style={{
                        marginTop: 20,
                        display: "flex",
                        gap: 10
                    }}
                >
                    <button
                        type="button"
                        onClick={saveBom}
                    >
                        Save
                    </button>

                    <button
                        type="button"
                        onClick={onCancel}
                    >
                        Cancel
                    </button>
                </div>
            </div>
        </>
    );
}

export default BomEditor;