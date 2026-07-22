function CompanyStep({ company, updateField }) {

    return (

        <div className="form-grid">

            <div className="form-group">

                <label>Company Name</label>

                <input
                    value={company.companyName}
                    onChange={(e) =>
                        updateField(
                            "company",
                            "companyName",
                            e.target.value
                        )
                    }
                />

            </div>

            <div className="form-group">

                <label>Trading Name</label>

                <input
                    value={company.tradingName}
                    onChange={(e) =>
                        updateField(
                            "company",
                            "tradingName",
                            e.target.value
                        )
                    }
                />

            </div>

            <div className="form-group">

                <label>Registration Number</label>

                <input
                    value={company.registrationNumber}
                    onChange={(e) =>
                        updateField(
                            "company",
                            "registrationNumber",
                            e.target.value
                        )
                    }
                />

            </div>

            <div className="form-group">

                <label>VAT Number</label>

                <input
                    value={company.vatNumber}
                    onChange={(e) =>
                        updateField(
                            "company",
                            "vatNumber",
                            e.target.value
                        )
                    }
                />

            </div>

            <div className="form-group full-width">

                <label>Industry</label>

                <select
                    value={company.industry}
                    onChange={(e) =>
                        updateField(
                            "company",
                            "industry",
                            e.target.value
                        )
                    }
                >
                    <option>Chemical Manufacturing</option>
                    <option>Cleaning Chemicals</option>
                    <option>Water Treatment</option>
                    <option>Food Processing</option>
                    <option>Cosmetics</option>
                </select>

            </div>

        </div>

    );
}

export default CompanyStep;