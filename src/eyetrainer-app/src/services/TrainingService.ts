import axios, { AxiosRequestConfig } from "axios";
import { EyeTrainingPlanResponse } from "../contracts/trainings/EyeTrainingPlanResponse";
import { store } from "../app/store";

axios.interceptors.request.use(function (config): AxiosRequestConfig {
  const token = store.getState().user.token;
  if (!(config.headers && token)) return config;
  config.headers.Authorization = `Bearer ${token}`;

  return config;
});

function isExpectedStatus(status: number, expectedStatus: number): boolean {
  if (expectedStatus == status) {
    return true;
  }
  return false;
}

const API_URL = process.env.REACT_APP_BACKEND;

const appointmentsUrl = API_URL + "/Appointments";

class TrainingService {
  async getTrainings(
    appointmentId: number
  ): Promise<EyeTrainingPlanResponse[]> {
    const trainingsUrl = `${appointmentsUrl}/${appointmentId}/EyeTrainingPlans`;
    const response = await axios.get(trainingsUrl, { headers: {} });
    return response.data;
  }
}

export default new TrainingService();
