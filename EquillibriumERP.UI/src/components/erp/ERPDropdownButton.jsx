import { useEffect, useRef, useState } from "react";

import "./ERPDropdownButton.css";

export default function ERPDropdownButton({

    label,

    children

}) {

    const [open, setOpen] = useState(false);

    const containerRef = useRef(null);

    useEffect(() => {

        function handleClickOutside(event) {

            if (
                containerRef.current &&
                !containerRef.current.contains(event.target)
            ) {
                setOpen(false);
            }

        }

        document.addEventListener("mousedown", handleClickOutside);

        return () => {

            document.removeEventListener(
                "mousedown",
                handleClickOutside
            );

        };

    }, []);

    return (

        <div
            className="erp-dropdown"
            ref={containerRef}
        >

            <button

                className="erp-button erp-button-primary"

                onClick={() => setOpen(value => !value)}

            >

                {label} ▾

            </button>

            {

                open && (

                    <div
                        className="erp-dropdown-menu"
                        onClick={() => setOpen(false)}
                    >

                        {children}

                    </div>

                )

            }

        </div>

    );

}