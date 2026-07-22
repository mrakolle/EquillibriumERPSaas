import { useEffect, useState } from "react";
import { FaPlus } from "react-icons/fa";

import {
    ERPButton,
    ERPPageHeader
} from "../../../components/erp";

import ProductList from "../components/ProductList";
import ProductForm from "../components/ProductForm";
import ProductViewer from "../components/ProductViewer";

import { getAllProducts } from "../services/productService";

export default function ProductPage() {

    const [products, setProducts] = useState([]);

    const [view, setView] = useState("list");

    const [selectedProductId, setSelectedProductId] = useState(null);

    useEffect(() => {

        loadProducts();

    }, []);

    async function loadProducts() {

        try {

            const data = await getAllProducts();

            setProducts(data);

        }
        catch (error) {

            console.error(error);

            setProducts([]);

        }

    }

    function returnToList() {

        setSelectedProductId(null);

        setView("list");

        loadProducts();

    }

    return (

        <div className="erp-page">

            <ERPPageHeader

                title="Products"

                subtitle="Manage products and raw materials."

                actions={

                    <ERPButton
                        onClick={() => setView("new")}
                    >

                        <FaPlus />

                        New Product

                    </ERPButton>

                }

            />

            {

                view === "list" && (

                    <ProductList

                        products={products}

                        onView={(id) => {

                            setSelectedProductId(id);

                            setView("view");

                        }}

                    />

                )

            }

            {

                view === "new" && (

                    <ProductForm

                        onCancel={() => setView("list")}

                        onCreated={() => {

                            returnToList();

                        }}

                    />

                )

            }

            {

                view === "view" && (

                    <ProductViewer

                        productId={selectedProductId}

                        onCancel={() => {

                            setSelectedProductId(null);

                            setView("list");

                        }}

                        onUpdated={() => {

                            returnToList();

                        }}

                    />

                )

            }

        </div>

    );

}