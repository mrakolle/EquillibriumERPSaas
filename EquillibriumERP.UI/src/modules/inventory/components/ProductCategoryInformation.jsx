import {
    ERPDetailsLayout,
    ERPImageCard
} from "../../../components/erp";

import ProductCategoryInformationForm from "./ProductCategoryInformationForm";

export default function ProductCategoryInformation({

    category,

    onChange,

    isEditing = false,

    isNew = false

}) {

    return (

        <ERPDetailsLayout

            left={

                <ProductCategoryInformationForm

                    category={category}

                    onChange={onChange}

                    isEditing={isEditing}

                    isNew={isNew}

                />

            }

            right={

                <ERPImageCard

                    title="Category"

                    image={null}

                    caption={

                        category?.name ||

                        "No category selected."

                    }

                />

            }

        />

    );

}