import "./ERPImageCard.css";

export default function ERPImageCard({
    title,
    image,
    caption,
    placeholderIcon = null
}) {

    return (

        <div className="erp-image-card">

            <div className="erp-image-card-header">

                {title}

            </div>

            <div className="erp-image-card-body">

                {

                    image ? (

                        <img
                            src={image}
                            alt={caption}
                            className="erp-image-card-image"
                        />

                    ) : (

                        <div className="erp-image-placeholder">

                            {placeholderIcon}

                        </div>

                    )

                }

                <div className="erp-image-caption">

                    {caption}

                </div>

            </div>

        </div>

    );

}