import React from "react";
import { Modal, Form, Input, Select, notification } from "antd";
import BuildingsService from "../../services/BuildingService";
import "../../assets/css/Configuration.scss";

const { Option } = Select;

const BuildingForm = ({ open, onCancel, onSubmit, buildingTypes }) => {
  const [form] = Form.useForm();

  const onFinish = async (values) => {
    const buildingCost = parseFloat(values.buildingCost);
    const constructionTime = parseFloat(values.constructionTime);

    if (isNaN(buildingCost) || buildingCost <= 0) {
      notification.error({
        message: "Building cost must be greater than zero.",
      });
      return;
    }

    if (
      isNaN(constructionTime) ||
      constructionTime < 30 ||
      constructionTime > 1800
    ) {
      notification.error({
        message: "Construction time must be between 30 and 1800 seconds.",
      });
      return;
    }

    const formattedValues = {
      buildingTypeId: values.buildingType,
      buildingCost,
      constructionTime,
    };

    try {
      const response = await BuildingsService.AddBuildingAsync(formattedValues);
      if (response.message === "Building added successfully") {
        notification.success({
          message: "Building added successfully!",
        });
        form.resetFields();
        onSubmit(formattedValues);
      } else {
        notification.error({
          message: response.data.message || "Failed to add building.",
        });
      }
    } catch (error) {
      notification.error({
        message: "An error occurred while submitting the form.",
      });
    }
  };

  return (
    <Modal
      title="Add Building Configuration"
      open={open}
      onCancel={onCancel}
      onOk={() => form.submit()}
      className="custom-modal"
    >
      <Form
        form={form}
        onFinish={onFinish}
        layout="vertical"
        className="custom-form"
      >
        <Form.Item
          label="Building Type"
          name="buildingType"
          rules={[{ required: true, message: "Please select a building type" }]}
        >
          <Select placeholder="Select a building type">
            {buildingTypes.map((type) => (
              <Option key={type.id} value={type.id}>
                {type.name}
              </Option>
            ))}
          </Select>
        </Form.Item>

        <Form.Item
          label="Building Cost"
          name="buildingCost"
          rules={[
            { required: true, message: "Please input the building cost" },
          ]}
        >
          <Input type="number" step="0.01" />
        </Form.Item>
        <Form.Item
          label="Construction Time (seconds)"
          name="constructionTime"
          rules={[
            { required: true, message: "Please input the construction time" },
          ]}
        >
          <Input type="number" />
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default BuildingForm;
