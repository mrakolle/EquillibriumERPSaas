import { useEffect, useState } from "react";

import { FaPlus, FaTrash } from "react-icons/fa";

import {
    ERPButton,
    ERPTable
} from "../../../../components/erp";

import { getAllProducts } from "../../../inventory/services/productService";
import "./EstimateItemsGrid.css";

export default function EstimateItemsGrid({
    items,
    onChange,
    isEditing
}) {

    const estimateItems = items ?? [];

    const [products, setProducts] = useState([]);

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

        }

    }

    function addItem() {

        onChange([

            ...estimateItems,

            {
                id: crypto.randomUUID(),

                productId: "",

                productCode: "",

                productName: "",

                quantity: 1,

                unitPrice: 0,

                discountPercent: 0,

                taxRate: 0,

                lineTotal: 0

            }

        ]);

    }

    function removeItem(id) {

        onChange(

            estimateItems.filter(
                item => item.id !== id
            )

        );

    }

    function updateItem(id, field, value) {

        onChange(

            estimateItems.map(item => {

                if (item.id !== id)
                    return item;

                let updated = {

                    ...item,

                    [field]: value

                };

                if (field === "productId") {

                    const product = products.find(
                        p => String(p.id) === String(value)
                    );

                    updated = {

                        ...updated,

                        productId: value,

                        productCode: product?.productCode ?? "",

                        productName: product?.name ?? "",

                        unitPrice: product?.sellingPrice ?? 0,

                        taxRate: product?.taxRate ?? 0

                    };

                }

                const subtotal =
                    Number(updated.quantity) *
                    Number(updated.unitPrice);

                const discount =
                    subtotal *
                    (Number(updated.discountPercent) / 100);

                const taxable =
                    subtotal - discount;

                const tax =
                    taxable *
                    (Number(updated.taxRate) / 100);

                updated.lineTotal = taxable + tax;

                return updated;

            })

        );

    }

    return (

        <>

            {

                isEditing && (

                    <ERPButton
                        size="small"
                        onClick={addItem}
                    >

                        <FaPlus />

                        Add Product

                    </ERPButton>

                )

            }

            <ERPTable className="estimate-items-grid">

                <thead>

                    <tr>
                        
                        <th>Product</th>

                        <th>Qty</th>

                        <th>Unit Price</th>

                        <th>Discount %</th>

                        <th>Vat %</th>

                        <th>Line Total</th>

                        {

                            isEditing && (

                                <th>Action</th>

                            )

                        }

                    </tr>

                </thead>

                <tbody>

                    {

                        estimateItems.length === 0 ? (

                            <tr>

                                <td
                                    colSpan={7}
                                    className="erp-table-center"
                                >
                                    No products added.
                                </td>

                            </tr>

                        ) : (

                            estimateItems.map(item => (

                                <tr key={item.id}>

                                    <td>

                                        {

                                            isEditing ? (

                                                <select
                                                    value={item.productId}
                                                    onChange={(e) =>
                                                        updateItem(
                                                            item.id,
                                                            "productId",
                                                            e.target.value
                                                        )
                                                    }
                                                >

                                                    <option value="">
                                                        Select Product
                                                    </option>

                                                    {

                                                        products.map(product => (

                                                            <option
                                                                key={product.id}
                                                                value={product.id}
                                                            >
                                                                {product.productCode} - {product.name}
                                                            </option>

                                                        ))

                                                    }

                                                </select>

                                            ) : (

                                                item.productName

                                            )

                                        }

                                    </td>

                                    <td>

                                        <input
                                            type="number"
                                            value={item.quantity}
                                            disabled={!isEditing}
                                            onChange={(e) =>
                                                updateItem(
                                                    item.id,
                                                    "quantity",
                                                    Number(e.target.value)
                                                )
                                            }
                                        />

                                    </td>

                                    <td>

                                        <input
                                            type="number"
                                            step="0.01"
                                            value={item.unitPrice}
                                            disabled={!isEditing}
                                            onChange={(e) =>
                                                updateItem(
                                                    item.id,
                                                    "unitPrice",
                                                    Number(e.target.value)
                                                )
                                            }
                                        />

                                    </td>

                                    <td>

                                        <input
                                            type="number"
                                            step="0.01"
                                            value={item.discountPercent}
                                            disabled={!isEditing}
                                            onChange={(e) =>
                                                updateItem(
                                                    item.id,
                                                    "discountPercent",
                                                    Number(e.target.value)
                                                )
                                            }
                                        />

                                    </td>

                                    <td>

                                        <input
                                            type="number"
                                            step="0.01"
                                            value={item.taxRate}
                                            disabled={!isEditing}
                                            onChange={(e) =>
                                                updateItem(
                                                    item.id,
                                                    "taxRate",
                                                    Number(e.target.value)
                                                )
                                            }
                                        />

                                    </td>

                                    <td>

                                        {Number(item.lineTotal).toFixed(2)}

                                    </td>

                                    {

                                        isEditing && (

                                            <td>

                                                <button
                                                    type="button"
                                                    className="erp-icon-button"
                                                    onClick={() => removeItem(item.id)}
                                                >

                                                    <FaTrash />

                                                </button>

                                            </td>

                                        )

                                    }

                                </tr>

                            ))

                        )

                    }

                </tbody>

            </ERPTable>

        </>

    );

}