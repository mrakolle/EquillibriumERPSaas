import BomInformationForm from "./BomInformationForm";

import {
    ERPCard,
    ERPDetailsLayout,
    ERPField,
    ERPForm,
    ERPImageCard
} from "../../../components/erp";

export default function ProductInformationCard({
    bom,
    onChange,
    isEditing
}) {

    return (

    <ERPCard
        title="Product Information"
        subtitle="View or edit the selected product."
    >

        <ERPDetailsLayout

            left={

                    <BomInformationForm

                        bom={bom}

                        onChange={onChange}

                        isEditing={isEditing}

                        isNew={false}

                    />

                }

            right={

                <ERPImageCard

                    title="Product"

                    image={bom.imageUrl}

                    caption={
                        bom.name || "No product selected."
                    }

                />

            }

                />

        </ERPCard>

    );

}