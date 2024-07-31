import React, { useState, useEffect } from "react";
import { Table } from "antd";
import { ApiRequests } from "../../api/Api";
import "../../assets/css/Configuration.scss";

const BuildingTable = () => {
  const [buildingsData, setBuildingsData] = useState([]);
  const [buildingTypes, setBuildingTypes] = useState([]);

  useEffect(() => {
    const fetchBuildingTypes = async () => {
      try {
        const response = await ApiRequests.handleRequestGetAsync(
          "buildings/getall"
        );
        if (Array.isArray(response?.data?.buildingTypes)) {
          setBuildingTypes(response.data.buildingTypes);
        } else {
          throw new Error("Invalid data format for building types.");
        }
      } catch (error) {
        console.error(`Failed to fetch building types: ${error.message}`);
      }
    };

    fetchBuildingTypes();
  }, []);

  useEffect(() => {
    // Fetch buildings data
    const fetchBuildingsData = async () => {
      try {
        const response = await ApiRequests.handleRequestGetAsync(
          "buildings/getbuilding"
        );
        if (Array.isArray(response?.data?.buildings)) {
          const buildingsWithTypeNames = response.data.buildings.map(
            (building) => {
              const buildingType = buildingTypes.find(
                (type) => type.id === building.buildingTypeId
              );
              return {
                ...building,
                buildingTypeName: buildingType ? buildingType.name : "Unknown",
              };
            }
          );
          setBuildingsData(buildingsWithTypeNames);
        } else {
          throw new Error("Invalid data format for buildings.");
        }
      } catch (error) {
        console.error(`Failed to fetch buildings data: ${error.message}`);
      }
    };

    fetchBuildingsData();
  }, [buildingTypes]);

  const columns = [
    {
      title: "Building Type",
      dataIndex: "buildingTypeName",
      key: "buildingTypeName",
    },
    {
      title: "Building Cost",
      dataIndex: "buildingCost",
      key: "buildingCost",
    },
    {
      title: "Construction Time",
      dataIndex: "constructionTime",
      key: "constructionTime",
    },
  ];

  return (
    <div className="table-container">
      <Table
        dataSource={buildingsData}
        columns={columns}
        rowKey="id"
        pagination={false}
        className="custom-table"
      />
    </div>
  );
};

export default BuildingTable;
