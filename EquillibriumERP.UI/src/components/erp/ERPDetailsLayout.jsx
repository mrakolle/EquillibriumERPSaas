import "./ERPDetailsLayout.css";

export default function ERPDetailsLayout({

    left,

    right

}) {

    return (

        <div className="erp-details-layout">

            <div className="erp-details-left">

                {left}

            </div>

            <div className="erp-details-right">

                {right}

            </div>

        </div>

    );

}