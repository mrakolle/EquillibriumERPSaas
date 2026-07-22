import {
    ERPDetailsLayout,
    ERPImageCard
} from "../../../components/erp";
import { FaFlask } from "react-icons/fa";

import ProductInformationForm from "./ProductInformationForm";

export default function ProductInformation({
    product,
    onChange,
    isEditing = false,
    isNew = false
}) {

    return (

        <ERPDetailsLayout

            left={

                <ProductInformationForm

                    product={product}

                    onChange={onChange}

                    isEditing={isEditing}

                    isNew={isNew}

                />

            }

            right={

                <ERPImageCard

                    title="Product"

                    image={product.imageUrl}

                    caption={
                        product.name || "No product selected."
                    }

                />

            }

        />

    );

}