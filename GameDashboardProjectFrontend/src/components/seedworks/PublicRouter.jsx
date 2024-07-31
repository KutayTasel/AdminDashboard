import React from "react";
import { Navigate } from "react-router-dom";

const PublicRoute = ({ element }) => {
  const isAuthenticated = !!sessionStorage.getItem("token");
  return isAuthenticated ? <Navigate to="/configuration" replace /> : element;
};

export default PublicRoute;
