import { useState, useEffect } from "react";
import { Button, Row, Col, message } from "antd";
import { toast } from "react-toastify";
import BuildingForm from "../../components/forms/BuildingForms";
import BuildingTable from "../../components/tables/BuildingTable";
import { ApiRequests } from "../../api/Api";
import "../../assets/css/Configuration.scss";

const BuildingConfiguration = () => {
  const [visible, setVisible] = useState(false);
  const [buildingTypes, setBuildingTypes] = useState([]);
  const [selectedBuildingTypes, setSelectedBuildingTypes] = useState([]);
  const [buildingsData, setBuildingsData] = useState([]);
  const [filteredBuildingTypes, setFilteredBuildingTypes] = useState([]);

  useEffect(() => {
    const fetchBuildingTypes = async () => {
      try {
        const response = await ApiRequests.handleRequestGetAsync(
          "buildings/getall"
        );
        if (Array.isArray(response?.data?.buildingTypes)) {
          setBuildingTypes(response.data.buildingTypes);
        } else {
          throw new Error(
            "Invalid data format. Expected an array of building types."
          );
        }
      } catch (error) {
        message.error(`Failed to fetch building types: ${error.message}`);
      }
    };
    fetchBuildingTypes();
  }, []);

  useEffect(() => {
    setFilteredBuildingTypes(
      buildingTypes.filter((type) => !selectedBuildingTypes.includes(type._id))
    );
  }, [selectedBuildingTypes, buildingTypes]);

  useEffect(() => {
    const fetchBuildingsData = async () => {
      try {
        const response = await ApiRequests.handleRequestGetAsync(
          "buildings/getbuilding"
        );
        if (Array.isArray(response?.data?.buildings)) {
          setBuildingsData(response.data.buildings);
        } else {
          throw new Error(
            "Invalid data format. Expected an array of buildings."
          );
        }
      } catch (error) {
        message.error(`Failed to fetch buildings data: ${error.message}`);
      }
    };
    fetchBuildingsData();
  }, []);

  const handleFormSubmit = async (newBuilding) => {
    try {
      const response = await ApiRequests.handleRequestPostAsync(
        "building-config",
        newBuilding
      );
      if (response.status === 201 && response.data) {
        const buildingTypeName =
          buildingTypes.find((type) => type._id === newBuilding.buildingTypeId)
            ?.name || newBuilding.buildingTypeId;
        const newBuildingData = {
          id: response.data.id || newBuilding.buildingTypeId,
          buildingTypeId: newBuilding.buildingTypeId,
          buildingType: buildingTypeName,
          buildingCost: newBuilding.buildingCost,
          constructionTime: newBuilding.constructionTime,
        };

        setBuildingsData((prevData) => [...prevData, newBuildingData]);
        setSelectedBuildingTypes((prevTypes) => [
          ...prevTypes,
          newBuilding.buildingTypeId,
        ]);
        setVisible(false);
        toast.success("Building configuration saved successfully!");
      } else {
        throw new Error(
          response.data.message || "Failed to save building configuration."
        );
      }
    } catch (error) {
      toast.error(
        `Failed to save building configuration: ${
          error.message || "An error occurred."
        }`
      );
    }
  };

  return (
    <div className="px-4 py-8 md:px-8 lg:px-16 bg-gradient-to-r from-blue-500 to-purple-600 min-h-screen flex items-start justify-center">
      <div className="w-full max-w-7xl mx-auto bg-white p-4 sm:p-6 md:p-8 rounded-lg shadow-lg">
        <Row justify="center" className="mb-4">
          <Col>
            <Button
              type="primary"
              onClick={() => setVisible(true)}
              className="custom-button"
            >
              Add Building Configuration
            </Button>
          </Col>
        </Row>
        <BuildingForm
          open={visible}
          onCancel={() => setVisible(false)}
          onSubmit={handleFormSubmit}
          buildingTypes={filteredBuildingTypes}
        />
        <BuildingTable buildingsData={buildingsData} />
      </div>
    </div>
  );
};

export default BuildingConfiguration;
