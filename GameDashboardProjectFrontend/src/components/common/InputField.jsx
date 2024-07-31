import React from "react";
import { Form, Input } from "antd";
import {
  UserOutlined,
  LockOutlined,
  MailOutlined,
  CheckCircleOutlined,
} from "@ant-design/icons";

const InputField = React.forwardRef(
  ({ name, placeholder, type, rules }, ref) => {
    let icon;
    let className = "input-field-default";

    switch (name) {
      case "username":
        icon = <UserOutlined className="site-form-item-icon" />;
        className = "input-field-username";
        break;
      case "password":
        icon = <LockOutlined className="site-form-item-icon" />;
        className = "input-field-password";
        break;
      case "email":
        icon = <MailOutlined className="site-form-item-icon" />;
        className = "input-field-email";
        break;
      case "confirmPassword":
        icon = <CheckCircleOutlined className="site-form-item-icon" />;
        className = "input-field-confirm-password";
        break;
      default:
        icon = null;
    }

    return (
      <Form.Item name={name} rules={rules} className="custom-form-item">
        {type === "password" ? (
          <Input.Password
            ref={ref}
            prefix={icon}
            placeholder={placeholder}
            size="large"
            className={`input-field ${className} bg-gray-100 border-gray-300`}
          />
        ) : (
          <Input
            ref={ref}
            prefix={icon}
            placeholder={placeholder}
            size="large"
            className={`input-field ${className} bg-gray-100 border-gray-300`}
          />
        )}
      </Form.Item>
    );
  }
);

export default InputField;
