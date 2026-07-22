import { useState } from "react";
import { FaPlus } from "react-icons/fa";

import {
    ERPButton,
    ERPPageHeader
} from "../../../components/erp";

import CustomerCategoryTable from "../components/customer/CustomerCategoryTable";
import CustomerCategoryForm from "../components/customer/CustomerCategoryForm";
import CustomerCategoryViewer from "../components/customer/CustomerCategoryViewer";

import useCustomerCategories from "../hooks/useCustomerCategories";


export default function CustomerCategoriesPage() {

    const {

        categories,

        loadCategories

    } = useCustomerCategories();


    const [view, setView] = useState("list");

    const [selectedCategoryId, setSelectedCategoryId] = useState(null);


    return (

        <div className="erp-page">

            <ERPPageHeader

                title="Customer Categories"

                subtitle="Manage customer categories."

                actions={

                    <ERPButton
                        onClick={() => setView("new")}
                    >

                        <FaPlus />

                        New Category

                    </ERPButton>

                }

            />


            {
                view === "list" && (

                    <CustomerCategoryTable

                        categories={categories}

                        onView={(id) => {

                            setSelectedCategoryId(id);

                            setView("view");

                        }}

                    />

                )
            }


            {
                view === "new" && (

                    <CustomerCategoryForm

                        onCancel={() =>
                            setView("list")
                        }

                        onCreated={() => {

                            setView("list");

                            loadCategories();

                        }}

                    />

                )
            }


            {
                view === "view" && (

                    <CustomerCategoryViewer

                        categoryId={selectedCategoryId}

                        onCancel={() => {

                            setSelectedCategoryId(null);

                            setView("list");

                            loadCategories();

                        }}

                    />

                )
            }


        </div>

    );

}