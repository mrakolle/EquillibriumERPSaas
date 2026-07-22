import logo from "../../../assets/AppLogo.png";

function LoginBrandPanel() {

    return (

        <div className="login-brand-panel">

            <img
                src="/AppLogo.png"
                alt=""
                className="login-logo"
            />
             <img
                                src={logo}
                                alt="IQuillibrium ERP"
                                style={{
                                    height: "400px",
                                    width: "auto",
                                    objectFit: "contain"
                                }}
                            />

            <h1>

               

            </h1>

            <h2>

                Manufacturing ERP built
                for chemical manufacturers.

            </h2>

            <p>

                Manufacturing • Inventory •
                Sales • Purchasing • Quality •
                Finance

            </p>

        </div>

    );

}

export default LoginBrandPanel;