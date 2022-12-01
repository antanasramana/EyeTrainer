export type PostAppointmentRequest = {
  date: string;
  isConfirmed: boolean;
  doctorId: number | null;
  patientId: number | null;
  address: string;
  description: string;
};
