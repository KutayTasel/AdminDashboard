import { ApiRequests } from "../api/Api";
import { toast } from "react-toastify";

const AuthService = {
  async LoginServiceAsync(data) {
    try {
      console.log("Giriş isteği başlatılıyor:", data);
      const response = await ApiRequests.handleRequestPostAsync(
        "Auth/Login",
        data,
        null,
        false
      );

      if (response.status === 200 && response.data.token) {
        console.log("Giriş başarılı, alınan token:", response.data.token);
        return response;
      } else {
        const errorMessage =
          response.data.message || "Geçersiz kullanıcı adı veya şifre.";
        toast.error(errorMessage);
        console.error("Giriş başarısız:", errorMessage);
      }
      return response;
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Giriş sırasında bir hata oluştu.";
      toast.error(errorMessage);
      console.error("Giriş sırasında bir hata oluştu.", error);
      throw error;
    }
  },

  async RegisterServiceAsync(data) {
    try {
      console.log("Kayıt isteği başlatılıyor:", data);
      const response = await ApiRequests.handleRequestPostAsync(
        "Auth/Register",
        data,
        null,
        false
      );

      if (response.status === 201 && response.data.isSuccess) {
        console.log("Kayıt başarılı:", response.data);
        return response;
      } else {
        const errorMessage = response.data.message || "Kayıt başarısız.";
        toast.error(errorMessage);
        console.error("Kayıt başarısız:", errorMessage);
      }
      return response;
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || "Kayıt sırasında bir hata oluştu.";
      toast.error(errorMessage);
      console.error("Kayıt sırasında bir hata oluştu.", error);
      throw error;
    }
  },

  Logout() {
    sessionStorage.clear();
  },
};

export default AuthService;
