import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { getAllProductCategories } from "../../../services/productCategoryApi";
import {createProduct,updateProduct,getProductById} from "../../../services/productApi";

function ProductAdd() {
    const [name, setName] = useState("");
    const [productType, setProductType] = useState("");
    const [productCode, setProductCode] = useState("");
    const [productCategoryId, setProductCategoryId] = useState("");
    const [sellingPrice, setSellingPrice] = useState(0);
    const [costPrice, setCostPrice] = useState(0);
    const [casNumber, setCasNumber] = useState("");
    const [description, setDescription] = useState("");
    const [isActive, setIsActive] = useState(true);
    const [categories, setCategories] = useState([]);
    const navigate = useNavigate();
    const { id } = useParams();

    useEffect(() => {
    async function loadCategories() {
        const data = await getAllProductCategories();
        setCategories(data);
    }

    loadCategories();
}, []);
    

    useEffect(() => {
        if (!id) return;

        async function loadProduct() {
            const product = await getProductById(id);
            
            setName(product.name);
            setProductCode(product.code);
            setCasNumber(product.casNumber ?? "");
            setDescription(product.description ?? "");
            setSellingPrice(product.sellingPrice);
            setProductType(product.productType);
            setProductCategoryId(product.productCategoryId);
            setCostPrice(product.costPrice);
            setIsActive(product.isActive);
        
        }

        loadProduct();
    }, [id]);
    
    
    async function saveProduct() {
            const request = {
            name,
            productCode,
            productType: Number(productType),
            productCategoryId,
            sellingPrice,
            costPrice,
            casNumber,
            description,
            isActive
        };

        try {
            if (id) {
                await updateProduct(id, request);
            } else {
                await createProduct(request);
            }

            navigate("/products");
        } catch (error) {
    
            alert("Unable to save the product. Please try again.");
        }
    }
    return (
        <div>
            <h2>{id ? "Edit Product" : "Add Product"}</h2>

            <div style={{ marginTop: 20 }}>
                <label>Product Name</label>
                <br />
                <input
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                />
            </div>
            <div style={{ marginTop: 20 }}>
                <label>Product Code</label>
                <br />
                <input
                    value={productCode}
                    onChange={(e) => setProductCode(e.target.value)}
                />
            </div>
            <div style={{ marginTop: 20 }}>
                <label>Product Type</label>
                <br />
                <select
                    value={productType}
                    onChange={(e) => setProductType(e.target.value)}
                >
                    <option value="">Select Type...</option>
                    <option value="0">Raw Material</option>
                    <option value="1">Manufactured Product</option>
                </select>
            </div>
            <div style={{ marginTop: 20 }}>
                <label>Product Category</label>
                <br />
                <select
                    value={productCategoryId}
                    onChange={(e) => setProductCategoryId(e.target.value)}
                >
                    <option value="">Select Category...</option>

                    {categories.map((category) => (
                        <option key={category.id} value={category.id}>
                            {category.name}
                        </option>
                    ))}
                </select>
            </div>

            <div style={{ marginTop: 20 }}>
                <label>Selling Price</label>
                <br />
                <input
                    type="number"
                    value={sellingPrice}
                    onChange={(e) => setSellingPrice(Number(e.target.value))}
                />
            </div>

            <div style={{ marginTop: 20 }}>
                <label>Cost Price</label>
                <br />
                <input
                    type="number"
                    value={costPrice}
                    onChange={(e) => setCostPrice(Number(e.target.value))}
                />
            </div>

            <div style={{ marginTop: 20 }}>
                <label>CAS Number</label>
                <br />
                <input
                    value={casNumber}
                    onChange={(e) => setCasNumber(e.target.value)}
                />
            </div>

            <div style={{ marginTop: 20 }}>
                <label>Description</label>
                <br />
                <textarea
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                />
            </div>

            <div style={{ marginTop: 20 }}>
                <label>
                    <input
                        type="checkbox"
                        checked={isActive}
                        onChange={(e) => setIsActive(e.target.checked)}
                    />
                    {" "}Active
                </label>
            </div>
            <button
                type="button"
                onClick={saveProduct}
            >
                {id ? "Update Product" : "Create Product"}
            </button>
        </div>
    );
}

export default ProductAdd;