// vite.config.js
import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import vitePluginImp from "vite-plugin-imp";

export default defineConfig({
  plugins: [
    react(),
    vitePluginImp({
      libList: [
        {
          libName: "antd",
          style: (name) => `antd/es/${name}/style`,
        },
      ],
    }),
  ],
  resolve: {
    alias: {
      "@": "/src",
      "~antd": "node_modules/antd",
    },
  },
});
