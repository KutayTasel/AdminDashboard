import axios from "axios";
import { toast } from "react-toastify";

const api = axios.create({
  baseURL: "https://gamedashboardproject.azurewebsites.net/api/",
});

const addTokenHeader = () => {
  const token = sessionStorage.getItem("token");
  if (token) {
    api.defaults.headers.common["Authorization"] = `Bearer ${token}`;
  }
};

export const ApiRequests = {
  async handleRequestGetAsync(requestUrl, setLoading = null) {
    try {
      if (setLoading) setLoading(true);
      addTokenHeader();
      const res = await api.get(requestUrl);
      toast.success("GET request successful!");
      return { data: res.data, status: res.status };
    } catch (error) {
      toast.error(
        "Error in GET request: " +
          (error.response?.data?.message || error.message)
      );
      return error.response
        ? {
            data: error.response.data.message || error.response.data.title,
            status: error.response.status,
          }
        : { data: error.message, status: error.code };
    } finally {
      if (setLoading) setLoading(false);
    }
  },

  async handleRequestPostAsync(
    requestUrl,
    requestData,
    setLoading = null,
    hasToken = true
  ) {
    try {
      if (setLoading) setLoading(true);
      if (hasToken) addTokenHeader();
      const res = await api.post(requestUrl, requestData, {
        headers: { "Content-Type": "application/json" },
      });
      toast.success("POST request successful!");
      return { data: res.data, status: res.status };
    } catch (error) {
      toast.error(
        "Error in POST request: " +
          (error.response?.data?.message || error.message)
      );
      return error.response
        ? {
            data: error.response.data.message || error.response.data.title,
            status: error.response.status,
          }
        : { data: error.message, status: error.code };
    } finally {
      if (setLoading) setLoading(false);
    }
  },

  async handleRequestPutAsync(requestUrl, requestData, setLoading = null) {
    try {
      if (setLoading) setLoading(true);
      addTokenHeader();
      const res = await api.put(requestUrl, requestData);
      toast.success("PUT request successful!");
      return { data: res.data, status: res.status };
    } catch (error) {
      toast.error(
        "Error in PUT request: " +
          (error.response?.data?.message || error.message)
      );
      return error.response
        ? {
            data: error.response.data.message || error.response.data.title,
            status: error.response.status,
          }
        : { data: error.message, status: error.code };
    } finally {
      if (setLoading) setLoading(false);
    }
  },

  async handleRequestDeleteAsync(requestUrl, setLoading = null) {
    try {
      if (setLoading) setLoading(true);
      addTokenHeader();
      const res = await api.delete(requestUrl);
      toast.success("DELETE request successful!");
      return { data: res.data, status: res.status };
    } catch (error) {
      toast.error(
        "Error in DELETE request: " +
          (error.response?.data?.message || error.message)
      );
      return error.response
        ? {
            data: error.response.data.message || error.response.data.title,
            status: error.response.status,
          }
        : { data: error.message, status: error.code };
    } finally {
      if (setLoading) setLoading(false);
    }
  },

  async handleRequestPatchAsync(requestUrl, requestData, setLoading = null) {
    try {
      if (setLoading) setLoading(true);
      addTokenHeader();
      const res = await api.patch(requestUrl, requestData);
      toast.success("PATCH request successful!");
      return { data: res.data, status: res.status };
    } catch (error) {
      toast.error(
        "Error in PATCH request: " +
          (error.response?.data?.message || error.message)
      );
      return error.response
        ? {
            data: error.response.data.message || error.response.data.title,
            status: error.response.status,
          }
        : { data: error.message, status: error.code };
    } finally {
      if (setLoading) setLoading(false);
    }
  },
};
