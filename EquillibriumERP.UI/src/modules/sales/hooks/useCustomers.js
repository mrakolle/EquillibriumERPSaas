import { useEffect, useState } from "react";

import {
    getAllCustomers
} from "../services/customerService";

export default function useCustomers() {

    const [customers, setCustomers] = useState([]);

    const [isLoading, setIsLoading] = useState(true);

    const [error, setError] = useState(null);

    useEffect(() => {

        loadCustomers();

    }, []);

    async function loadCustomers() {

        try {

            setIsLoading(true);

            setError(null);

            const data = await getAllCustomers();

            setCustomers(data);

        }
        catch (err) {

            console.error(err);

            setError(err);

            setCustomers([]);

        }
        finally {

            setIsLoading(false);

        }

    }

    return {

        customers,

        isLoading,

        error,

        reload: loadCustomers

    };

}