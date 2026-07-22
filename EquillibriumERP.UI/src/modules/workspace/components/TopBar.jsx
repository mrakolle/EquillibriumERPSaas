import logo from "../../../assets/AppLogo.png";

import UserMenu from "./UserMenu";

export default function TopBar() {

    return (

        <>

            <div
                style={{
                    display: "flex",
                    flexDirection: "column",
                    alignItems: "center",
                    justifyContent: "center",
                    height: "100%"
                }}
            >

                <img
                    //src={logo}
                    alt="IQuillibrium ERP"
                    style={{
                        height: "75px",
                        width: "auto",
                        fontSize: "2.1rem",
                        objectFit: "contain"
                    }}
                />

<div
    style={{
        marginTop: "-13px",
        fontSize: "0.9rem",
        opacity: 0.65
    }}
>
    Powered by <strong>CHEM-IQ</strong>
</div>

            </div>


            <div
                style={{
                    marginLeft: "auto"
                }}
            >
                <UserMenu />
            </div>

        </>

    );

}