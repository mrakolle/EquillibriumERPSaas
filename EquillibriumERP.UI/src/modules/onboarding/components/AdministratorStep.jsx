function AdministratorStep({
    administrator,
    updateField
}) {
    return (

        <div className="form-grid">

            <div className="form-group">

                <label>First Name</label>

                <input
                    value={administrator.firstName}
                    onChange={(e) =>
                        updateField(
                            "administrator",
                            "firstName",
                            e.target.value
                        )
                    }
                />

            </div>

            <div className="form-group">

                <label>Last Name</label>

                <input
                    value={administrator.lastName}
                    onChange={(e) =>
                        updateField(
                            "administrator",
                            "lastName",
                            e.target.value
                        )
                    }
                />

            </div>

            <div className="form-group">

                <label>Email Address</label>

                <input
                    type="email"
                    value={administrator.email}
                    onChange={(e) =>
                        updateField(
                            "administrator",
                            "email",
                            e.target.value
                        )
                    }
                />

            </div>

            <div className="form-group">

                <label>Password</label>

                <input
                    type="password"
                    value={administrator.password}
                    onChange={(e) =>
                        updateField(
                            "administrator",
                            "password",
                            e.target.value
                        )
                    }
                />

            </div>

        </div>

    );
}

export default AdministratorStep;