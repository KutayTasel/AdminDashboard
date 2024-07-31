import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.jsx";
import { BrowserRouter } from "react-router-dom";
import GenaralContextProvider from "./contexts/GenaralContext.jsx";

ReactDOM.createRoot(document.getElementById("root")).render(
  <GenaralContextProvider>
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </GenaralContextProvider>
);
