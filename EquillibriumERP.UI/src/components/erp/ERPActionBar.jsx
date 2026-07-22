import "./ERPActionBar.css";

export default function ERPActionBar({

    left,

    right,

    children

}) {

    if (children) {

        return (

            <div className="erp-action-bar">

                {children}

            </div>

        );

    }

    return (

        <div className="erp-action-bar">

            <div className="erp-action-bar-left">

                {left}

            </div>

            <div className="erp-action-bar-right">

                {right}

            </div>

        </div>

    );

}