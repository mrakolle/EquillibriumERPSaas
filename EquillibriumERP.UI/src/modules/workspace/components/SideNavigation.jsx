import { useState } from "react";
import { NavLink } from "react-router-dom";

import { navigationItems } from "../constants/navigationItems";

export default function SideNavigation() {

    const [expanded, setExpanded] = useState({

        sales: true,
        inventory: true,
        manufacturing: true,
        purchasing: true,
        quality: true,
        lims: true

    });

    function toggle(id) {

        setExpanded(current => ({

            ...current,

            [id]: !current[id]

        }));

    }

    return (

        <nav>

            {
                navigationItems.map(item => {

                    const hasChildren = item.children.length > 0;

                    if (!hasChildren) {

                        return (

                            <NavLink
                                key={item.id}
                                to={item.route}
                                end
                                style={({ isActive }) => ({

                                    display: "block",
                                    padding: "12px 16px",
                                    marginBottom: "4px",
                                    borderRadius: "6px",
                                    textDecoration: "none",

                                    color: isActive
                                        ? "#ffffff"
                                        : "#333333",

                                    backgroundColor: isActive
                                        ? "#1f4e79"
                                        : "transparent",

                                    fontWeight: isActive
                                        ? "600"
                                        : "400"

                                })}
                            >
                                {item.title}
                            </NavLink>

                        );

                    }

                    const isExpanded = expanded[item.id];

                    return (

                        <div key={item.id}>

                            <button
                            onClick={() => toggle(item.id)}
                            style={{
                                width: "100%",

                                display: "flex",

                                alignItems: "center",

                                gap: "8px",

                                padding: "12px 16px",

                                border: "none",

                                background: "transparent",

                                cursor: "pointer",

                                fontWeight: "600",

                                textAlign: "left"
                            }}
                        >

                            <span
                                style={{
                                    width: "16px",
                                    display: "inline-block"
                                }}
                            >
                                {isExpanded ? "▼" : "▶"}
                            </span>

                            <span>
                                {item.title}
                            </span>

                        </button>

                            {

                                isExpanded &&

                                item.children.map(child => (

                                    <NavLink
                                        key={child.id}
                                        to={child.route}
                                        style={({ isActive }) => ({

                                            display: "block",

                                            padding: "10px 16px 10px 32px",

                                            marginBottom: "2px",

                                            borderRadius: "6px",

                                            textDecoration: "none",

                                            color: isActive
                                                ? "#ffffff"
                                                : "#555",

                                            backgroundColor: isActive
                                                ? "#1f4e79"
                                                : "transparent"

                                        })}
                                    >
                                        {child.title}
                                    </NavLink>

                                ))

                            }

                        </div>

                    );

                })

            }

        </nav>

    );

}