import { useEffect } from "react";

import {
    ERPActionBar,
    ERPButton,
    ERPCard
} from "../../../../components/erp";

import useCustomerCategories from "../../hooks/useCustomerCategories";


export default function CustomerCategoryViewer({

    categoryId,

    onCancel

}) {

    const {

        selectedCategory,

        loadCategory,

        isLoading

    } = useCustomerCategories();


    useEffect(() => {

        if (categoryId) {

            loadCategory(categoryId);

        }

    }, [categoryId]);


    if (isLoading) {

        return (

            <ERPCard

                title="Customer Category"

            >

                Loading...

            </ERPCard>

        );

    }


    if (!selectedCategory) {

        return (

            <ERPCard

                title="Customer Category"

            >

                No category selected.

            </ERPCard>

        );

    }


    return (

        <ERPCard

            title="Customer Category"

            subtitle={selectedCategory.name}

        >

            <div>

                <p>

                    <strong>
                        Name:
                    </strong>

                    {" "}

                    {selectedCategory.name}

                </p>


            </div>


            <ERPActionBar>

                <ERPButton

                    variant="secondary"

                    onClick={onCancel}

                >

                    Back

                </ERPButton>


            </ERPActionBar>


        </ERPCard>

    );

}