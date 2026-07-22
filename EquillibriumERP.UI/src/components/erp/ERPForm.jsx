import "./ERPForm.css";

export default function ERPForm({

    children,

    columns = 2

}) {

    return (

        <div
            className="erp-form"
            style={{
                gridTemplateColumns:
                    `repeat(${columns}, minmax(0, 1fr))`
            }}
        >
            {children}
        </div>

    );

}