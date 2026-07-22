import { createContext, useContext, useEffect, useMemo, useState } from "react";

const AuthenticationContext = createContext(null);

const STORAGE_KEY = "equillibrium.auth";
function isTokenExpired(token) {

    if (!token) {

        return true;

    }

    try {

        const payload = JSON.parse(atob(token.split(".")[1]));

        return Date.now() >= payload.exp * 1000;

    }
    catch {

        return true;

    }

}

export function AuthProvider({ children }) {
    const [session, setSession] = useState(null);
    const [isInitializing, setIsInitializing] = useState(true);

    useEffect(() => {
        const stored = localStorage.getItem(STORAGE_KEY);

        if (stored) {
            try {
                const auth = JSON.parse(stored);

                if (isTokenExpired(auth.accessToken)) {

                    localStorage.removeItem(STORAGE_KEY);

                }
                else {

                    setSession(auth);

                }
            } catch {
                localStorage.removeItem(STORAGE_KEY);
            }
        }

        setIsInitializing(false);
    }, []);

    const login = (authResult) => {
        localStorage.setItem(STORAGE_KEY, JSON.stringify(authResult));
        setSession(authResult);
    };

    const logout = () => {
        localStorage.removeItem(STORAGE_KEY);
        setSession(null);
    };

    const value = useMemo(
        () => ({
            session,
            isAuthenticated:
                !!session?.accessToken &&
                !isTokenExpired(session.accessToken),
            isInitializing,
            login,
            logout,
        }),
        [session, isInitializing]
    );

    return (
        <AuthenticationContext.Provider value={value}>
            {children}
        </AuthenticationContext.Provider>
    );
}

export function useAuth() {
    const context = useContext(AuthenticationContext);

    if (!context) {
        throw new Error("useAuth must be used inside AuthProvider");
    }

    return context;
}