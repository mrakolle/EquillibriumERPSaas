export const API_BASE = "http://localhost:5167";

const STORAGE_KEY = "equillibrium.auth";

export async function apiFetch(endpoint, options = {}) {

    const stored = localStorage.getItem(STORAGE_KEY);

    let token = null;

    if (stored) {
        try {
            const session = JSON.parse(stored);
            token = session.accessToken;
        }
        catch {
            localStorage.removeItem(STORAGE_KEY);
        }
    }

    const headers = {
        Accept: "application/json",
        ...(token && { Authorization: `Bearer ${token}` }),
        ...(options.headers ?? {})
    };

    if (options.body) {
        headers["Content-Type"] = "application/json";
    }

    return fetch(`${API_BASE}${endpoint}`, {
        ...options,
        headers
    });
}