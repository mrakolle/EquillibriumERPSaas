import "./ERPField.css";

export default function ERPField({

    label,

    required = false,

    children

}) {

    return (

        <div className="erp-field">

            <label className="erp-field-label">

                {label}

                {

                    required && (

                        <span className="erp-required">

                            *

                        </span>

                    )

                }

            </label>

            {children}

        </div>

    );

}