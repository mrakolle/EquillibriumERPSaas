import { useEffect, useState } from "react";

import {
    getAllCustomerCategories,
    getCustomerCategoryById
} from "../services/customerCategoryService";


export default function useCustomerCategories() {

    const [categories, setCategories] = useState([]);

    const [selectedCategory, setSelectedCategory] = useState(null);

    const [isLoading, setIsLoading] = useState(false);

    const [error, setError] = useState(null);


    async function loadCategories() {

        try {

            setIsLoading(true);
            setError(null);

            const data = await getAllCustomerCategories();

            setCategories(data);

        }
        catch (error) {

            console.error(
                "Failed to load customer categories:",
                error
            );

            setError(
                "Failed to load customer categories."
            );

            setCategories([]);

        }
        finally {

            setIsLoading(false);

        }

    }


    async function loadCategory(id) {

        try {

            setIsLoading(true);
            setError(null);

            const data =
                await getCustomerCategoryById(id);

            setSelectedCategory(data);

            return data;

        }
        catch (error) {

            console.error(
                "Failed to load customer category:",
                error
            );

            setError(
                "Failed to load customer category."
            );

            setSelectedCategory(null);

            return null;

        }
        finally {

            setIsLoading(false);

        }

    }


    useEffect(() => {

        loadCategories();

    }, []);


    return {

        categories,

        selectedCategory,

        isLoading,

        error,

        loadCategories,

        loadCategory

    };

}