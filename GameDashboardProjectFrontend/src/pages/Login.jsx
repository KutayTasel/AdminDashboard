import React, { useState, useEffect } from "react";
import { Form, Button } from "antd";
import { useNavigate, Link } from "react-router-dom";
import InputField from "../components/common/InputField";
import "../assets/css/Login.scss";
import Lottie from "react-lottie";
import animationData from "../assets/animation/animation.json";
import AuthValidations from "../validations/Validation";
import AuthService from "../services/AuthService";
import Spinner from "../components/common/Spinner";
import { toast, ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const Login = () => {
  const [loading, setLoading] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    const snowflakes = document.createElement("div");
    snowflakes.classList.add("snowflakes");
    for (let i = 0; i < 50; i++) {
      const snowflake = document.createElement("div");
      snowflake.classList.add("snowflake");
      snowflakes.appendChild(snowflake);
    }
    document.body.appendChild(snowflakes);

    return () => {
      document.body.removeChild(snowflakes);
    };
  }, []);

  const handleLogin = async (values) => {
    setLoading(true);
    setErrorMessage("");

    try {
      const response = await AuthService.LoginServiceAsync({
        userName: values.username.trim(),
        password: values.password.trim(),
      });

      if (response.data.token) {
        sessionStorage.setItem("token", response.data.token);
        toast.success("Başarıyla giriş yapıldı, yönlendiriliyorsunuz...");
        navigate("/configuration");
      } else {
        setErrorMessage(
          response.data.message || "Geçersiz kullanıcı adı veya şifre."
        );
        toast.error(
          response.data.message || "Geçersiz kullanıcı adı veya şifre."
        );
      }
    } catch (error) {
      setErrorMessage(error.response?.data?.message || "Bir hata oluştu.");
      toast.error(
        error.response?.data?.message || "Giriş sırasında bir hata oluştu."
      );
    } finally {
      setLoading(false);
    }
  };

  const handleFormSubmit = async (values) => {
    const usernameErrors = AuthValidations.validateUsername(values.username);
    const passwordErrors = AuthValidations.validatePassword(values.password);

    if (usernameErrors.length) {
      setErrorMessage(usernameErrors[0]);
      return;
    }

    if (passwordErrors.length) {
      setErrorMessage(passwordErrors[0]);
      return;
    }

    handleLogin(values);
  };

  const defaultOptions = {
    loop: true,
    autoplay: true,
    animationData: animationData,
    rendererSettings: {
      preserveAspectRatio: "xMidYMid slice",
    },
  };

  return (
    <div className="login-container">
      <ToastContainer />
      <div className="stars"></div>
      <div className="twinkling"></div>
      <div className="moving-stars"></div>
      <div className="shooting-stars"></div>
      <div className="snowflakes"></div>
      <div className="login-content">
        <div className="lottie-container">
          <Lottie options={defaultOptions} height={350} width={350} />
        </div>
        {loading && <Spinner />}
        {errorMessage && <div className="error-message">{errorMessage}</div>}
        <Form
          layout="vertical"
          onFinish={handleFormSubmit}
          className="login-form"
        >
          <InputField
            name="username"
            placeholder="Kullanıcı Adı"
            type="text"
            rules={[
              {
                validator: (_, value) => {
                  const errors = AuthValidations.validateUsername(value);
                  if (errors.length) {
                    return Promise.reject(errors[0]);
                  }
                  return Promise.resolve();
                },
              },
            ]}
          />
          <InputField
            name="password"
            placeholder="Şifre"
            type="password"
            rules={[
              {
                validator: (_, value) => {
                  const errors = AuthValidations.validatePassword(value);
                  if (errors.length) {
                    return Promise.reject(errors[0]);
                  }
                  return Promise.resolve();
                },
              },
            ]}
          />
          <Form.Item>
            <Button
              type="primary"
              htmlType="submit"
              loading={loading}
              className="login-button w-full py-2 mt-4"
              size="large"
            >
              Giriş
            </Button>
          </Form.Item>
          <div className="login-links flex flex-col items-center mt-4">
            <Link
              to="/register"
              className="login-link text-blue-500 hover:text-blue-700"
            >
              Hesap Oluştur
            </Link>
            <Link
              to="/forgot-password"
              className="login-link text-blue-500 hover:text-blue-700 mt-2"
            >
              Şifrenizi mi unuttunuz?
            </Link>
          </div>
        </Form>
      </div>
    </div>
  );
};

export default Login;
