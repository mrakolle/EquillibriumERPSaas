import { useAuth } from "../../context/AuthContext";

function TopBar() {
    const { setIsLoggedIn } = useAuth();
  return (
    <div
      style={{
        width: "100%",
        height: "50px",
        background: "#0b1f3a",
        color: "white",
        display: "flex",
        justifyContent: "space-between",
        alignItems: "center",
        padding: "0 16px"
      }}
    >
      <h3>IQuillibriumERP</h3>

      <button
        onClick={() => setIsLoggedIn(false)}
        style={{
          background: "transparent",
          border: "1px solid white",
          color: "white",
          padding: "6px 12px",
          cursor: "pointer"
        }}
      >
        Logout again
      </button>
    </div>
  );
}

export default TopBar;