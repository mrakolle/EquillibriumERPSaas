import {
    ERPButton,
    ERPCard,
    ERPField,
    ERPForm
} from "../../../components/erp";

export default function BomForm({

    onCancel

}) {

    return (

        <ERPCard

            title="New Bill of Material"

            subtitle="Create a new formulation."

        >

            <ERPForm>

                <ERPField
                    label="BOM Code"
                    required
                >

                    <input />

                </ERPField>

                <ERPField
                    label="Name"
                    required
                >

                    <input />

                </ERPField>

                <ERPField
                    label="Product"
                    required
                >

                    <select>

                        <option>
                            Select Product...
                        </option>

                    </select>

                </ERPField>

                <ERPField
                    label="Version"
                >

                    <input
                        defaultValue="1.0"
                    />

                </ERPField>

                <ERPField
                    label="Description"
                >

                    <textarea />

                </ERPField>

            </ERPForm>

            <div
                style={{
                    display: "flex",
                    justifyContent: "flex-end",
                    gap: "12px",
                    marginTop: "32px"
                }}
            >

                <ERPButton
                    variant="secondary"
                    onClick={onCancel}
                >
                    Cancel
                </ERPButton>

                <ERPButton>
                    Save BOM
                </ERPButton>

            </div>

        </ERPCard>

    );

}