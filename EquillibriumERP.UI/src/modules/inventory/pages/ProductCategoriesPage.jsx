import { useEffect, useState } from "react";
import { FaPlus } from "react-icons/fa";

import {
    ERPButton,
    ERPPageHeader
} from "../../../components/erp";

import ProductCategoryTable from "../components/ProductCategoryTable";
import ProductCategoryForm from "../components/ProductCategoryForm";
import ProductCategoryViewer from "../components/ProductCategoryViewer";

import {
    getAllProductCategories
} from "../services/productCategoryService";

export default function ProductCategoriesPage() {

    const [categories, setCategories] = useState([]);

    const [view, setView] = useState("list");

    const [selectedCategoryId, setSelectedCategoryId] = useState(null);

    useEffect(() => {

        loadCategories();

    }, []);

    async function loadCategories() {

        try {

            const data = await getAllProductCategories();

            setCategories(data);

        }
        catch (error) {

            console.error(error);

            setCategories([]);

        }

    }

    return (

        <div className="erp-page">

            <ERPPageHeader

                title="Product Categories"

                subtitle="Manage product categories."

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

                    <ProductCategoryTable

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

                    <ProductCategoryForm

                        onCancel={() => setView("list")}

                        onCreated={() => {

                            setView("list");

                            loadCategories();

                        }}

                    />

                )

            }

            {

                view === "view" && (

                    <ProductCategoryViewer

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