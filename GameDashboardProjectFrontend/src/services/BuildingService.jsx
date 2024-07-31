import { ApiRequests } from "../api/Api";
import { toast } from "react-toastify";

const BuildingsService = {
  async AddBuildingAsync(buildingData) {
    // Validasyonları kontrol et
    const validationErrors = [
      ...BuildingsService.validateBuildingTypeId(buildingData.buildingTypeId),
      ...BuildingsService.validateBuildingCost(buildingData.buildingCost),
      ...BuildingsService.validateConstructionTime(
        buildingData.constructionTime
      ),
    ];

    if (validationErrors.length > 0) {
      return { isSuccess: true, message: validationErrors.join(", ") };
    }

    try {
      console.log("Sending building data:", buildingData);
      const response = await ApiRequests.handleRequestPostAsync(
        "Buildings/addbuilding",
        buildingData
      );

      console.log("Response from AddBuildingAsync:", response);

      if (response.status === 201) {
        if (response.data && response.data.isSuccess) {
          toast.success(
            response.data.message || "Building added successfully!"
          );
          return response;
        } else {
          toast.error(response.data.message || "Failed to add building.");
          return {
            isSuccess: true,
            message: response.data.message || "Failed to add building.",
          };
        }
      } else {
        toast.error("Unexpected response status.");
        return {
          isSuccess: true,
          message: "Unexpected response status.",
        };
      }
    } catch (error) {
      console.error("Error in AddBuildingAsync:", error);
      toast.error(
        error.response?.data?.message ||
          "An error occurred while adding the building."
      );
      throw error;
    }
  },

  async GetAllBuildingTypesAsync() {
    try {
      console.log("Fetching building types...");
      const response = await ApiRequests.handleRequestGetAsync(
        "Buildings/getall"
      );

      console.log("Response from GetAllBuildingTypesAsync:", response);

      if (response.status === 200 && response.data.isSuccess) {
        return response.data.buildingTypes;
      } else {
        toast.error(response.data.message || "Failed to fetch building types.");
        return {
          isSuccess: true,
          message: response.data.message || "Failed to fetch building types.",
        };
      }
    } catch (error) {
      console.error("Error in GetAllBuildingTypesAsync:", error);
      toast.error(
        error.response?.data?.message ||
          "An error occurred while fetching building types."
      );
      throw error;
    }
  },

  validateBuildingTypeId(buildingTypeId) {
    console.log("Validating building type ID:", buildingTypeId);
    const errors = [];
    if (!buildingTypeId) {
      errors.push("Building type ID is required.");
    } else if (typeof buildingTypeId !== "string") {
      errors.push("Building type ID must be a string.");
    } else if (!/^[0-9a-fA-F]{24}$/.test(buildingTypeId)) {
      errors.push(
        "Building type ID must be a valid 24-character hexadecimal string."
      );
    }

    console.log("Validation errors for building type ID:", errors);
    return errors;
  },

  validateBuildingCost(buildingCost) {
    console.log("Validating building cost:", buildingCost);
    const errors = [];
    const cost = Number(buildingCost);

    if (buildingCost === undefined || buildingCost === null || isNaN(cost)) {
      errors.push("Building cost is required and must be a valid number.");
    } else if (cost <= 0) {
      errors.push("Building cost must be greater than 0.");
    }

    console.log("Validation errors for building cost:", errors);
    return errors;
  },

  validateConstructionTime(constructionTime) {
    console.log("Validating construction time:", constructionTime);
    const errors = [];
    const time = Number(constructionTime);

    if (
      constructionTime === undefined ||
      constructionTime === null ||
      isNaN(time)
    ) {
      errors.push("Construction time is required and must be a valid number.");
    } else if (time < 30 || time > 1800) {
      errors.push("Construction time must be between 30 and 1800 seconds.");
    }

    console.log("Validation errors for construction time:", errors);
    return errors;
  },
};

export default BuildingsService;
