import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Register from "../../pages/Register";
import Login from "../../pages/Login";
import Configuration from "../../pages/Configuration";
import PrivateRoute from "../../components/seedworks/PrivateRouter";
import PublicRoute from "../../components/seedworks/PublicRouter";
const RouteComponents = () => {
  return (
    <Routes>
      <Route
        path="/register"
        element={<PublicRoute element={<Register />} />}
      />
      <Route path="/login" element={<PublicRoute element={<Login />} />} />
      <Route
        path="/configuration"
        element={<PrivateRoute element={<Configuration />} />}
      />
      <Route path="/" element={<Navigate to="/login" replace />} />
      <Route path="*" element={<Navigate to="/login" replace />} />
    </Routes>
  );
};

export default RouteComponents;
