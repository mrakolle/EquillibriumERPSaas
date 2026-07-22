import "./ERPCard.css";

export default function ERPCard({

    title,

    subtitle,

    actions,

    children

}) {

    return (

        <div className="erp-card">

            {

                (title || actions) && (

                    <div className="erp-card-header">

                        <div>

                            {

                                title && (

                                    <div className="erp-card-title">
                                        {title}
                                    </div>

                                )

                            }

                            {

                                subtitle && (

                                    <div className="erp-card-subtitle">
                                        {subtitle}
                                    </div>

                                )

                            }

                        </div>

                        {actions}

                    </div>

                )

            }

            <div className="erp-card-body">

                {children}

            </div>

        </div>

    );

}