import React from "react";
import { Navigate } from "react-router-dom";

const PrivateRouter = ({ element }) => {
  const isAuthenticated = !!sessionStorage.getItem("token");
  return isAuthenticated ? element : <Navigate to="/login" replace />;
};

export default PrivateRouter;
