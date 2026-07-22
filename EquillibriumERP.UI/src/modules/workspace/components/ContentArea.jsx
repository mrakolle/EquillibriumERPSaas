import { Outlet } from "react-router-dom";

export default function ContentArea() {
    return (
        <section className="workspace-page">
            <Outlet />
        </section>
    );
}
