import { useEffect, useState } from "react";

import {
    ERPActionBar,
    ERPButton,
    ERPCard,
    ERPDetailsLayout,
    ERPField,
    ERPImageCard
} from "../../../components/erp";

import BomInformationForm from "./BomInformationForm";
import BomTabs from "../components/BomTabs";

import { getAllProducts } from "../../inventory/services/productService";
import { createBom } from "../services/bomService";

export default function BomForm({
    onCancel,
    onCreated
}) {

    const [finalProducts, setFinalProducts] = useState([]);

    const [rawMaterials, setRawMaterials] = useState([]);

    const [bom, setBom] = useState({

        productId: "",

        code: "",

        description: "",

        isActive: true

    });

    const [materials, setMaterials] = useState([]);

    const [processSteps, setProcessSteps] = useState([]);

    useEffect(() => {

        async function loadProducts() {

            try {

                const data = await getAllProducts();

                setFinalProducts(
                    data.filter(x => x.productType === 1)
                );

                setRawMaterials(
                    data.filter(x => x.productType === 0)
                );

            }
            catch (error) {

                console.error(error);

            }

        }

        loadProducts();

    }, []);

    const selectedProduct =
        finalProducts.find(
            x => x.id === bom.productId
        );
    async function saveBom() {
        try {

            const request = {

                productId: bom.productId,

                code: bom.code,

                description: bom.description,

                items: materials.map(item => ({

                    rawMaterialProductId: item.rawMaterialProductId,

                    quantity: Number(item.quantity),

                    unitOfMeasure: item.unitOfMeasure

                }))

            };

            console.log("Create BOM Request", request);

            const bomId = await createBom(request);

            onCreated(bomId);

        }
        catch (error) {

            console.error(error);

            alert("Failed to create BOM.");

        }

    }

    return (

        <ERPCard
            title="New Bill of Material"
            subtitle="Create a new formulation."
        >

            <ERPDetailsLayout

                left={

                        <BomInformationForm

                            bom={bom}

                            products={finalProducts}

                            onChange={setBom}

                            isEditing={true}

                            isNew={true}

                        />

                    }

                right={

                    <ERPImageCard

                        title="Selected Product"

                        image={selectedProduct?.imageUrl}

                        caption={
                            selectedProduct
                                ? selectedProduct.name
                                : "Select a product to preview."
                        }

                    />

                }

            />

            <BomTabs
                bom={bom}
                materials={materials}
                processSteps={processSteps}
                onMaterialsChange={setMaterials}
                onProcessStepsChange={setProcessSteps}
                isEditing={true}
            />

            <ERPActionBar>

                <ERPButton
                    variant="secondary"
                    onClick={onCancel}
                >
                    Cancel
                </ERPButton>

                <ERPButton
                    onClick={saveBom}
                >
                    Save BOM
                </ERPButton>

            </ERPActionBar>

        </ERPCard>

    );

}