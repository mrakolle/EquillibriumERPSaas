import {
    ERPDetailsLayout,
    ERPImageCard
} from "../../../../components/erp";

import { FaTags } from "react-icons/fa";

import CustomerCategoryInformationForm
    from "./CustomerCategoryInformationForm";


export default function CustomerCategoryInformation({

    category,

    onChange,

    isEditing = false,

    isNew = false

}) {

    return (

        <ERPDetailsLayout

            left={

                <CustomerCategoryInformationForm

                    category={category}

                    onChange={onChange}

                    isEditing={isEditing}

                    isNew={isNew}

                />

            }

            right={

                <ERPImageCard

                    title="Customer Category"

                    image={null}

                    placeholderIcon={<FaTags />}

                    caption={
                        category?.name ||
                        "No category selected."
                    }

                />

            }

        />

    );

}