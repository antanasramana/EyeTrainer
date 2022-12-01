import axios, { AxiosRequestConfig } from "axios";
import { AppointmentsResponse } from "../contracts/appointment/AppointmentsResponse";
import { store } from "../app/store";
import { AppointmentResponse } from "../contracts/appointment/AppointmentResponse";
import { PostAppointmentRequest } from "../contracts/appointment/PostAppointmentRequest";
import { PatchAppointmentRequest } from "../contracts/appointment/PatchAppointmentRequest";

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

class AppointmentService {
  async getAppointments(): Promise<AppointmentsResponse> {
    const response = await axios.get(appointmentsUrl, { headers: {} });

    const appointmentsResponse: AppointmentsResponse = {
      appointments: response.data,
    };

    return appointmentsResponse;
  }

  async getAppointment(appointmentId: number): Promise<AppointmentResponse> {
    console.log(appointmentId);
    const response = await axios.get(`${appointmentsUrl}/${appointmentId}`, {
      headers: {},
    });
    console.log(response);
    return response.data;
  }

  async createAppointment(
    postAppointmentRequest: PostAppointmentRequest
  ): Promise<AppointmentResponse> {
    console.log(postAppointmentRequest);
    const response = await axios.post(appointmentsUrl, postAppointmentRequest, {
      headers: {},
    });
    console.log(response);
    return response.data;
  }

  async patchAppointment(
    appointmentId: number,
    patchAppointmentRequest: PatchAppointmentRequest
  ): Promise<boolean> {
    const patch = [
      { op: "replace", path: "date", value: patchAppointmentRequest.date },
      {
        op: "replace",
        path: "isConfirmed",
        value: patchAppointmentRequest.isConfirmed,
      },
      {
        op: "replace",
        path: "doctorId",
        value: patchAppointmentRequest.doctorId,
      },
      {
        op: "replace",
        path: "description",
        value: patchAppointmentRequest.description,
      },
      {
        op: "replace",
        path: "address",
        value: patchAppointmentRequest.address,
      },
    ];

    console.log(patch);
    const url = `${appointmentsUrl}/${appointmentId}`;

    const response = await axios.patch(url, patch, {
      headers: {},
    });
    console.log(response);

    return isExpectedStatus(response.status, 204);
  }

  async deleteAppointment(id: number): Promise<boolean> {
    const uri = `${appointmentsUrl}/${id}`;
    const res = await axios.delete(uri);

    return isExpectedStatus(res.status, 204);
  }
}

export default new AppointmentService();
