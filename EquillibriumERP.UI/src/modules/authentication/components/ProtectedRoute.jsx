import { Navigate } from "react-router-dom";
import { useAuth } from "../context/AuthenticationContext";

export default function ProtectedRoute({ children }) {
    const { isAuthenticated, isInitializing } = useAuth();
    
    if (isInitializing) {
        return null;
    }

    if (!isAuthenticated) {
        return <Navigate to="/login" replace />;
    }

    return children;
}