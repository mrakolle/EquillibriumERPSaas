import { useState } from "react";
import { createTenant } from "../../../services/onboardingApi";

function CreateERPForm({ onCreate }) {
  const [form, setForm] = useState({
    companyName: "",
    companyCode: "",
    administratorName: "",
    email: "",
    password: "",
    confirmPassword: ""
  });

  const [error, setError] = useState("");

  const handleChange = (e) => {
    setForm({
      ...form,
      [e.target.name]: e.target.value
    });
  };

  const validate = () => {
    if (!form.companyName.trim()) {
      return "Company Name is required.";
    }

    if (!form.administratorName.trim()) {
      return "Administrator Name is required.";
    }

    if (!form.email.trim()) {
      return "Email Address is required.";
    }

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!emailRegex.test(form.email)) {
      return "Please enter a valid email address.";
    }

    if (!form.password) {
      return "Password is required.";
    }

    if (form.password.length < 8) {
      return "Password must be at least 8 characters.";
    }

    if (!form.confirmPassword) {
      return "Please confirm your password.";
    }

    if (form.password !== form.confirmPassword) {
      return "Passwords do not match.";
    }

    return null;
  };

  const handleCreate = () => {
    const validationError = validate();

    if (validationError) {
      setError(validationError);
      return;
    }

    setError("");

    onCreate(form);
  };

  return (
    <div>
      <h1 className="erp-title">Welcome to EquillibriumERP</h1>

      <p className="erp-subtitle">
        Let's create your Free Chemical Manufacturing ERP.
      </p>

      <div className="erp-form">
        <input
          name="companyName"
          placeholder="Company Name"
          value={form.companyName}
          onChange={handleChange}
        />

        <input
          name="administratorName"
          placeholder="Administrator Name"
          value={form.administratorName}
          onChange={handleChange}
        />

        <input
          type="email"
          name="email"
          placeholder="Email Address"
          value={form.email}
          onChange={handleChange}
        />

        <input
          type="password"
          name="password"
          placeholder="Password"
          value={form.password}
          onChange={handleChange}
        />

        <input
          type="password"
          name="confirmPassword"
          placeholder="Confirm Password"
          value={form.confirmPassword}
          onChange={handleChange}
        />

        {error && (
          <div
            style={{
              color: "#dc2626",
              background: "#fef2f2",
              border: "1px solid #fecaca",
              padding: "10px",
              borderRadius: "8px",
              fontSize: "14px"
            }}
          >
            {error}
          </div>
        )}

        <button onClick={async () => {

                const validationError = validate();

                if (validationError) {
                    setError(validationError);
                    return;
                }

                setError("");

                try {

                    await createTenant({
                        companyName: form.companyName,
                        administratorName: form.administratorName,
                        emailAddress: form.email,
                        password: form.password,
                        confirmPassword: form.confirmPassword
                    });

                    onCreate();

                } catch (err) {
                    setError(err.message);
                }

            }}
        >
          Create ERP
        </button>
      </div>
    </div>
  );
}

export default CreateERPForm;