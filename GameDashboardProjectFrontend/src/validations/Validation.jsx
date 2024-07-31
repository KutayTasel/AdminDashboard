import { toast } from "react-toastify";

const AuthValidations = {
  validateUsername(username) {
    const errors = [];
    if (!username) {
      errors.push("Username is required.");
    } else {
      if (username.length < 3) {
        errors.push("Username must be at least 3 characters long.");
      }
      if (username.length > 20) {
        errors.push("Username cannot exceed 20 characters.");
      }
      if (!/^[a-zA-Z0-9]+$/.test(username)) {
        errors.push("Username contains invalid characters.");
      }
    }

    if (errors.length > 0) {
      toast.error(errors.join(" "));
    }

    return errors;
  },

  validateEmail(email) {
    const errors = [];
    if (!email) {
      errors.push("Email address is required.");
    } else {
      const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
      if (!emailPattern.test(email)) {
        errors.push("Invalid email format.");
      }
    }

    if (errors.length > 0) {
      toast.error(errors.join(" "));
    }

    return errors;
  },

  validatePassword(password) {
    const errors = [];
    if (!password) {
      errors.push("Password is required.");
    } else {
      if (password.length < 6) {
        errors.push("Password must be at least 6 characters long.");
      }
      if (!/[A-Z]/.test(password)) {
        errors.push("Password must contain at least one uppercase letter.");
      }
      if (!/[a-z]/.test(password)) {
        errors.push("Password must contain at least one lowercase letter.");
      }
      if (!/\d/.test(password)) {
        errors.push("Password must contain at least one digit.");
      }
      if (!/[\W_]/.test(password)) {
        errors.push("Password must contain at least one special character.");
      }
    }

    if (errors.length > 0) {
      toast.error(errors.join(" "));
    }

    return errors;
  },

  validateConfirmPassword(password, confirmPassword) {
    const errors = [];
    if (confirmPassword !== password) {
      errors.push("Passwords do not match.");
    }

    if (errors.length > 0) {
      toast.error(errors.join(" "));
    }

    return errors;
  },
};

export default AuthValidations;
