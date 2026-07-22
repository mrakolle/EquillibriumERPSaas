import "./ERPButton.css";

export default function ERPButton({

    children,

    onClick,

    type = "button",

    variant = "primary",

    size = "normal",

    disabled = false,

    className = ""

}) {

    return (

        <button

            type={type}

            onClick={onClick}

            disabled={disabled}

            className={
                `erp-button
                 erp-button-${variant}
                 erp-button-${size}
                 ${className}`
            }

        >

            {children}

        </button>

    );

}