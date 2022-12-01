import axios, { AxiosError } from "axios";
import { LoginUserRequest } from "../contracts/user/LoginUserRequest";
import { LoginUserResponse } from "../contracts/user/LoginUserResponse";
import { RegisterUserRequest } from "../contracts/user/RegisterUserRequest";
import { RegisterUserResponse } from "../contracts/user/RegisterUserResponse";

const API_URL = process.env.REACT_APP_BACKEND;

const userUri = API_URL + "/Users";

class UserService {
  async login(loginUserRequest: LoginUserRequest): Promise<LoginUserResponse> {
    let loginResponse: LoginUserResponse = {
      userId: -1,
      token: "",
      name: "",
      role: "",
    };

    const loginUrl = `${userUri}/Login`;

    try {
      const res = await axios.post(loginUrl, loginUserRequest, {
        headers: { "Content-Type": "application/json" },
      });

      loginResponse = res.data;
      return loginResponse;
    } catch (err) {
      if (err instanceof AxiosError) {
        console.log(err.response?.data.message);
      }
      return loginResponse;
    }
  }

  async register(
    registerUserRequest: RegisterUserRequest
  ): Promise<RegisterUserResponse> {
    let registerUserResponse: RegisterUserResponse = {
      email: "",
      role: "",
    };

    const registerUrl = `${userUri}/Register`;

    try {
      const res = await axios.post(registerUrl, registerUserRequest, {
        headers: { "Content-Type": "application/json" },
      });

      registerUserResponse = res.data;
      return registerUserResponse;
    } catch (err) {
      if (err instanceof AxiosError) {
        console.log(err.response?.data.message);
      }
      return registerUserResponse;
    }
  }
}

export default new UserService();
