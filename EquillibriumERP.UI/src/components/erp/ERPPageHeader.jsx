import "./ERPPageHeader.css";

export default function ERPPageHeader({

    title,

    subtitle,

    actions

}) {

    return (

        <div className="erp-page-header">

            <div>

                <h1 className="erp-page-title">

                    {title}

                </h1>

                {

                    subtitle && (

                        <p className="erp-page-subtitle">

                            {subtitle}

                        </p>

                    )

                }

            </div>

            {

                actions && (

                    <div className="erp-page-actions">

                        {actions}

                    </div>

                )

            }

        </div>

    );

}