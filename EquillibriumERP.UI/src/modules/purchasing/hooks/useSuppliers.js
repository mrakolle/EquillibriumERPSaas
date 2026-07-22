import { useEffect, useState } from "react";

import {
    getAllSuppliers
} from "../services/supplierService";

export default function useSuppliers() {

    const [suppliers, setSuppliers] = useState([]);

    const [isLoading, setIsLoading] = useState(true);

    const [error, setError] = useState(null);

    useEffect(() => {

        loadSuppliers();

    }, []);

    async function loadSuppliers() {

        try {

            setIsLoading(true);

            setError(null);

            const data = await getAllSuppliers();

            setSuppliers(data);

        }
        catch (err) {

            console.error(err);

            setError(err);

            setSuppliers([]);

        }
        finally {

            setIsLoading(false);

        }

    }

    return {

        suppliers,

        isLoading,

        error,

        reload: loadSuppliers

    };

}