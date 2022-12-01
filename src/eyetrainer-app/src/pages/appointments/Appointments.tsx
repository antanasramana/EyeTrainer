import React, { ReactElement, useEffect, useState } from "react";
import Button from "@mui/material/Button";
import Card from "@mui/material/Card";
import CardActions from "@mui/material/CardActions";
import CardContent from "@mui/material/CardContent";
import CssBaseline from "@mui/material/CssBaseline";
import Grid from "@mui/material/Grid";
import Stack from "@mui/material/Stack";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { AppointmentResponse } from "../../contracts/appointment/AppointmentResponse";
import AppointmentService from "../../services/AppointmentService";
import dayjs from "dayjs";
import { useNavigate } from "react-router-dom";

const cards = [1, 2, 3, 4, 5, 6, 7, 8, 9];

const Appointments: React.FC = (): ReactElement => {
  const [appointments, setAppointments] = useState<AppointmentResponse[]>(
    new Array<AppointmentResponse>()
  );

  const navigate = useNavigate();

  async function fetchAppointments(): Promise<void> {
    const response = await AppointmentService.getAppointments();
    setAppointments(response.appointments);
  }

  useEffect((): void => {
    fetchAppointments();
  }, []);

  return (
    <main>
      <CssBaseline />
      {/* Hero unit */}
      <Box
        sx={{
          bgcolor: "background.paper",
          pt: 8,
          pb: 6,
        }}
      >
        <Container maxWidth="sm">
          <Typography
            component="h1"
            variant="h2"
            align="center"
            color="text.primary"
            gutterBottom
          >
            Appointments
          </Typography>
          <Typography
            variant="h5"
            align="center"
            color="text.secondary"
            paragraph
          >
            Here you can easily see what medical eye exercises are appointed to
            you
          </Typography>
          <Stack
            sx={{ pt: 4 }}
            direction="row"
            spacing={2}
            justifyContent="center"
          >
            <Button
              variant="contained"
              onClick={(): void => {
                navigate("Create");
              }}
            >
              Create Appointment
            </Button>
          </Stack>
        </Container>
      </Box>
      <Container sx={{ py: 8 }} maxWidth="md">
        {/* End hero unit */}
        <Grid container spacing={4}>
          {appointments.length > 0 &&
            appointments.map(
              (appointment): ReactElement => (
                <Grid item key={appointment.id} xs={12} sm={6} md={4}>
                  <Card
                    sx={{
                      height: "100%",
                      display: "flex",
                      flexDirection: "column",
                    }}
                  >
                    <CardContent sx={{ flexGrow: 1 }}>
                      <Typography gutterBottom variant="h5" component="h2">
                        {dayjs(appointment.date).toString()}
                      </Typography>
                      <Typography>{appointment.description}</Typography>
                    </CardContent>
                    <CardActions>
                      <Button
                        size="small"
                        onClick={(): void => {
                          navigate(`/Appointments/${appointment.id}`);
                        }}
                      >
                        View
                      </Button>
                    </CardActions>
                  </Card>
                </Grid>
              )
            )}
        </Grid>
      </Container>
    </main>
  );
};

export default Appointments;
