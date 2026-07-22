import {
    ERPDetailsLayout,
    ERPImageCard
} from "../../../../components/erp";
import { FaUserTie } from "react-icons/fa";

import CustomerInformationForm from "./CustomerInformationForm";

export default function CustomerInformation({

    customer,

    onChange,

    isEditing = false,

    isNew = false

}) {

    return (

        <ERPDetailsLayout

            left={

                <CustomerInformationForm

                    customer={customer}

                    onChange={onChange}

                    isEditing={isEditing}

                    isNew={isNew}

                />

            }

            right={

                <ERPImageCard
                    title="Customer"
                    image={null}
                    placeholderIcon={<FaUserTie />}
                    caption={customer?.name || "No customer selected."}
                />

            }

        />

    );

}