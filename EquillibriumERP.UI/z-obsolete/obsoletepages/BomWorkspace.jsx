function BomWorkspace({
    title,
    headerActions,
    footerActions,
    children
}) {
    return (
        <div
            style={{
                maxWidth: 1100,
                margin: "0 auto",
                padding: 24
            }}
        >
            {/* Header */}
            <div
                style={{
                    display: "flex",
                    justifyContent: "space-between",
                    alignItems: "center",
                    marginBottom: 20
                }}
            >
                <h2
                    style={{
                        margin: 0
                    }}
                >
                    {title}
                </h2>

                <div>
                    {headerActions}
                </div>
            </div>

            <hr />

            {/* Content */}
            <div
                style={{
                    marginTop: 24
                }}
            >
                {children}
            </div>

            {/* Footer */}
            {footerActions && (
                <>
                    <hr
                        style={{
                            marginTop: 30
                        }}
                    />

                    <div
                        style={{
                            display: "flex",
                            justifyContent: "flex-end",
                            gap: 10,
                            marginTop: 20
                        }}
                    >
                        {footerActions}
                    </div>
                </>
            )}
        </div>
    );
}

export default BomWorkspace;