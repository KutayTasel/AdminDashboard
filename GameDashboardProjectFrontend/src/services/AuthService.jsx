import { ApiRequests } from "../api/Api";
import { toast } from "react-toastify";

const AuthService = {
  async LoginServiceAsync(data) {
    try {
      console.log("Starting login request:", data);
      const response = await ApiRequests.handleRequestPostAsync(
        "Auth/Login",
        data,
        null,
        false
      );

      if (response.status === 200 && response.data.token) {
        console.log("Login successful, received token:", response.data.token);
        return response;
      } else {
        const errorMessage =
          response.data.message || "Invalid username or password.";
        toast.error(errorMessage);
        console.error("Login failed:", errorMessage);
        return { status: response.status, error: errorMessage };
      }
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "An error occurred during login.";
      toast.error(errorMessage);
      console.error("An error occurred during login:", error);
      return { status: error.response?.status, error: errorMessage };
    }
  },

  async RegisterServiceAsync(data) {
    try {
      console.log("Starting registration request:", data);
      const response = await ApiRequests.handleRequestPostAsync(
        "Auth/Register",
        data,
        null,
        false
      );

      if (response.status === 201 && response.data.isSuccess) {
        console.log("Registration successful:", response.data);
        return response;
      } else {
        const errorMessage = response.data.message || "Registration failed.";
        toast.error(errorMessage);
        console.error("Registration failed:", errorMessage);
        return { status: response.status, error: errorMessage };
      }
    } catch (error) {
      const errorMessage =
        error.response?.data?.message ||
        "An error occurred during registration.";
      toast.error(errorMessage);
      console.error("An error occurred during registration:", error);
      return { status: error.response?.status, error: errorMessage };
    }
  },

  Logout() {
    sessionStorage.clear();
    console.log("User logged out, session cleared.");
  },
};

export default AuthService;
