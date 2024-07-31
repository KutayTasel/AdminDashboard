import { createContext, useState, useEffect } from "react";
import { toast } from "react-toastify";
import Spinner from "../components/common/Spinner";
import BuildingService from "../services/BuildingService";

export const ApiContext = createContext();

const ApiContextProvider = ({ children }) => {
  const [user, setUser] = useState({ status: "", data: "" });
  const [buildings, setBuildings] = useState({ status: "", data: "" });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetchUserData();
    fetchBuildingData();
  }, []);

  const fetchUserData = async () => {
    setLoading(true);
    try {
      const response = await UserService.getUser();
      setUser({ status: "success", data: response.data });
      toast.success("User data fetched successfully!");
    } catch (error) {
      setError("Failed to fetch user data");
      setUser({ status: "error", data: null });
      toast.error("Failed to fetch user data.");
    } finally {
      setLoading(false);
    }
  };

  const fetchBuildingData = async () => {
    setLoading(true);
    try {
      const response = await BuildingService.getBuildings();
      setBuildings({ status: "success", data: response.data });
      toast.success("Building data fetched successfully!");
    } catch (error) {
      setError("Failed to fetch building data");
      setBuildings({ status: "error", data: null });
      toast.error("Failed to fetch building data.");
    } finally {
      setLoading(false);
    }
  };

  const updateUser = async (userData) => {
    setLoading(true);
    try {
      const response = await UserService.updateUser(userData);
      setUser({ status: "success", data: response.data });
      toast.success("User updated successfully!");
    } catch (error) {
      setError("Failed to update user");
      setUser({ status: "error", data: null });
      toast.error("Failed to update user.");
    } finally {
      setLoading(false);
    }
  };

  const deleteUser = async (userId) => {
    setLoading(true);
    try {
      await UserService.deleteUser(userId);
      setUser({ status: "", data: "" });
      toast.success("User deleted successfully!");
    } catch (error) {
      setError("Failed to delete user");
      toast.error("Failed to delete user.");
    } finally {
      setLoading(false);
    }
  };

  const updateBuilding = async (buildingData) => {
    setLoading(true);
    try {
      const response = await BuildingService.updateBuilding(buildingData);
      setBuildings((prevBuildings) => ({
        status: "success",
        data: prevBuildings.data.map((building) =>
          building.id === buildingData.id ? response.data : building
        ),
      }));
      toast.success("Building updated successfully!");
    } catch (error) {
      setError("Failed to update building");
      toast.error("Failed to update building.");
    } finally {
      setLoading(false);
    }
  };

  const deleteBuilding = async (buildingId) => {
    setLoading(true);
    try {
      await BuildingService.deleteBuilding(buildingId);
      setBuildings((prevBuildings) => ({
        status: "success",
        data: prevBuildings.data.filter(
          (building) => building.id !== buildingId
        ),
      }));
      toast.success("Building deleted successfully!");
    } catch (error) {
      setError("Failed to delete building");
      toast.error("Failed to delete building.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <ApiContext.Provider
      value={{
        user,
        setUser,
        buildings,
        setBuildings,
        loading,
        error,
        fetchUserData,
        fetchBuildingData,
        updateUser,
        deleteUser,
        updateBuilding,
        deleteBuilding,
      }}
    >
      {loading && <Spinner />}
      {children}
    </ApiContext.Provider>
  );
};

export default ApiContextProvider;
