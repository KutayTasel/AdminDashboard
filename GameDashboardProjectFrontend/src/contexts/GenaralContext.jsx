import { createContext, useState, useEffect } from "react";
import { toast } from "react-toastify";
import Spinner from "../components/common/Spinner";

export const GeneralContext = createContext();

const GeneralContextProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    const checkAuth = () => {
      setLoading(true);
      try {
        const storedUser = JSON.parse(localStorage.getItem("user"));
        if (storedUser) {
          setUser(storedUser);
          setIsAuthenticated(true);
          toast.success("Welcome back!");
        }
      } catch (err) {
        console.error("Failed to retrieve user data:", err);
        toast.error("Failed to retrieve user data.");
      } finally {
        setLoading(false);
      }
    };

    checkAuth();
  }, []);

  const login = (userData) => {
    setUser(userData);
    setIsAuthenticated(true);
    localStorage.setItem("user", JSON.stringify(userData));
    toast.success("Login successful!");
  };

  const logout = () => {
    setUser(null);
    setIsAuthenticated(false);
    localStorage.removeItem("user");
    toast.info("Logged out successfully.");
  };

  return (
    <GeneralContext.Provider
      value={{ user, setUser, isAuthenticated, loading, error, login, logout }}
    >
      {loading && <Spinner />}
      {children}
    </GeneralContext.Provider>
  );
};

export default GeneralContextProvider;
