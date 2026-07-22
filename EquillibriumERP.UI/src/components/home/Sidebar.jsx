import { NavLink } from "react-router-dom";
import "../../App.css";

function Sidebar() {
    
  const menuItems = [
    { to: "/dashboard", label: "Dashboard" },
    { to: "/quotations", label: "Quotations" },
    { to: "/invoices", label: "Invoices" },
    { to: "/purchasing", label: "Purchasing" },
    { to: "/products", label: "Products" },
    { to: "/manufacturing", label: "Manufacturing" },
    { to: "/inventory", label: "Inventory" }
  ];

  return (
    <aside className="sidebar">
      <h2 className="sidebar-title">EquillibriumERP</h2>

      <p className="sidebar-subtitle">
        Smart Chemical Solutions
      </p>

      <hr />

      {menuItems.map((item) => (
        <NavLink
          key={item.to}
          to={item.to}
          className={({ isActive }) =>
            isActive ? "sidebar-item active" : "sidebar-item"
          }
        >
          {item.label}
        </NavLink>
      ))}
    </aside>
  );
}

export default Sidebar;