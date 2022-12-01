export type PatchAppointmentRequest = {
  date: string;
  isConfirmed: boolean;
  doctorId: number | null;
  address: string;
  description: string;
};
