import React, { ReactElement, useState } from "react";
import Button from "@mui/material/Button";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import AppointmentService from "../../services/AppointmentService";
import { useNavigate } from "react-router-dom";
import TextField from "@mui/material/TextField";
import { PostAppointmentRequest } from "../../contracts/appointment/PostAppointmentRequest";
import { DateTimePicker, LocalizationProvider } from "@mui/x-date-pickers";
import dayjs, { Dayjs } from "dayjs";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import {
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  SelectChangeEvent,
} from "@mui/material";
import { useAppSelector } from "../../app/hooks";

const CreateAppointment: React.FC = (): ReactElement => {
  const navigate = useNavigate();
  const userId = useAppSelector((state): number => state.user.userId);

  const [date, setDate] = React.useState<Dayjs | null>(dayjs("2022-12-15"));
  const [isConfirmed, setIsConfirmed] = useState<boolean>(false);
  const [description, setDescription] = useState<string>("");
  const [address, setAddress] = useState<string>("");
  const [successMessage, setSuccessMessage] = useState<string>("");

  async function createAppointment(): Promise<void> {
    if (date == null) return;
    const postAppointmentRequest: PostAppointmentRequest = {
      date: date.toISOString(),
      isConfirmed: isConfirmed,
      description: description,
      address: address,
      doctorId: null,
      patientId: userId,
    };
    const isCreated = await AppointmentService.createAppointment(
      postAppointmentRequest
    );

    if (isCreated) {
      setSuccessMessage("Appointment created sucessfully");
    }
  }

  return (
    <main>
      {/* Hero unit */}
      <Box
        sx={{
          bgcolor: "background.paper",
          pt: 2,
          pb: 6,
        }}
      ></Box>
      <Container sx={{ py: 1 }} maxWidth="md">
        <Button
          onClick={(): void => {
            navigate("/Appointments");
          }}
          type="submit"
          variant="contained"
          sx={{ mt: 3, mb: 2 }}
        >
          Back to Appointment List
        </Button>
        <Typography variant="h4">New appointment</Typography>
        <Box sx={{ mt: 3 }}>
          <Grid container spacing={2}>
            <Grid item xs={12} sm={6}>
              <LocalizationProvider dateAdapter={AdapterDayjs}>
                <DateTimePicker
                  label="Appointment Time"
                  renderInput={(params): JSX.Element => (
                    <TextField {...params} />
                  )}
                  value={date}
                  onChange={(newDate): void => {
                    setDate(newDate);
                  }}
                />
              </LocalizationProvider>
            </Grid>
            <Grid item xs={12}>
              <FormControl fullWidth>
                <InputLabel id="demo-simple-select-label">
                  Is Confirmed?
                </InputLabel>
                <Select
                  labelId="demo-simple-select-label"
                  id="demo-simple-select"
                  value={isConfirmed ? "True" : "False"}
                  label="Is Confirmed?"
                  onChange={(event: SelectChangeEvent): void => {
                    (event.target.value as string) === "True"
                      ? setIsConfirmed(true)
                      : setIsConfirmed(false);
                  }}
                >
                  <MenuItem value="False">False</MenuItem>
                  <MenuItem value="True">True</MenuItem>
                </Select>
              </FormControl>
            </Grid>
            <Grid item xs={12}>
              <TextField
                onChange={(e): void => {
                  setDescription(e.target.value);
                  setSuccessMessage("");
                }}
                multiline
                required
                fullWidth
                label="Description"
                value={description}
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                onChange={(e): void => {
                  setAddress(e.target.value);
                  setSuccessMessage("");
                }}
                multiline
                required
                fullWidth
                label="Address"
                value={address}
              />
            </Grid>
          </Grid>
          <Typography fontSize={20} color={"red"}>
            {""}
          </Typography>
          <Typography fontSize={20} color={"green"}>
            {successMessage}
          </Typography>
          <Button
            onClick={createAppointment}
            disabled={!(date && description && address)}
            type="submit"
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
          >
            Create
          </Button>
        </Box>
      </Container>
    </main>
  );
};
export default CreateAppointment;
