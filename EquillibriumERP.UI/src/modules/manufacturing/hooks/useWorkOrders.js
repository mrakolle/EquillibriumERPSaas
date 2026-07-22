import { useCallback, useEffect, useState } from "react";
import {
    getWorkOrders,
    createWorkOrder,
    updateWorkOrder,
    deleteWorkOrder,
    changeWorkOrderStatus
} from "../services/workOrdersService";

export default function useWorkOrders() {
    const [workOrders, setWorkOrders] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const loadWorkOrders = useCallback(async () => {
        try {
            setLoading(true);
            setError(null);

            const data = await getWorkOrders();
            setWorkOrders(data);
        } catch (err) {
            setError(err);
        } finally {
            setLoading(false);
        }
    }, []);

    useEffect(() => {
        loadWorkOrders();
    }, [loadWorkOrders]);

    const addWorkOrder = async (request) => {
        const created = await createWorkOrder(request);
        await loadWorkOrders();
        return created;
    };

    const editWorkOrder = async (id, request) => {
        const updated = await updateWorkOrder(id, request);
        await loadWorkOrders();
        return updated;
    };

    const removeWorkOrder = async (id) => {
        await deleteWorkOrder(id);
        await loadWorkOrders();
    };

    const updateStatus = async (id, status) => {
        const result = await changeWorkOrderStatus(id, status);
        await loadWorkOrders();
        return result;
    };

    return {
        workOrders,
        loading,
        error,
        reload: loadWorkOrders,
        addWorkOrder,
        editWorkOrder,
        removeWorkOrder,
        updateStatus
    };
    
}
export async function startStep(workOrderId, stepId) {

    return apiFetch(
        `/manufacturing/workorders/${workOrderId}/steps/${stepId}/start`,
        {
            method: "POST"
        });
}