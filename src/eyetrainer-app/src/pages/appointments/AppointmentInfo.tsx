import React, { ReactElement, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import Paper from "@mui/material/Paper";
import Typography from "@mui/material/Typography";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import Container from "@mui/material/Container";
import { useNavigate } from "react-router-dom";
import Button from "@mui/material/Button";
import Stack from "@mui/material/Stack";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import { useAppSelector } from "../../app/hooks";
import { AppointmentResponse } from "../../contracts/appointment/AppointmentResponse";
import AppointmentService from "../../services/AppointmentService";
import dayjs from "dayjs";

const AppointmentInfo: React.FC = (): ReactElement => {
  // eslint-disable-next-line @typescript-eslint/explicit-function-return-type
  const user = useAppSelector((state) => state.user);
  const navigate = useNavigate();
  const { appointmentId } = useParams();

  const [isOwnedTheater, setIsOwnedTheater] = useState<boolean>(false);
  const [appointment, setAppointment] = useState<AppointmentResponse>();
  const [open, setOpen] = React.useState(false);
  const [successMessage, setSuccessMessage] = React.useState<string>("");

  useEffect((): void => {
    fetchAppointment();
  }, []);

  // methods
  async function fetchAppointment(): Promise<void> {
    const response = await AppointmentService.getAppointment(
      Number(appointmentId)
    );
    setAppointment(response);
  }

  // eslint-disable-next-line @typescript-eslint/no-empty-function
  async function navigateToTrainings(): Promise<void> {
    navigate(`/Appointments/${appointmentId}/Trainings`);
  }

  // eslint-disable-next-line @typescript-eslint/no-empty-function
  async function deleteAppointment(): Promise<void> {
    const isDeleted = await AppointmentService.deleteAppointment(
      Number(appointmentId)
    );
    if (isDeleted) {
      navigate("/Appointments");
    }
  }

  const handleClickOpen = (): void => {
    setOpen(true);
  };

  const handleClose = (): void => {
    setOpen(false);
  };

  return (
    <main>
      <Box
        sx={{
          bgcolor: "background.paper",
          pt: 2,
          pb: 6,
        }}
      ></Box>
      <Container sx={{ py: 1 }} maxWidth="lg">
        <Button
          onClick={(): void => {
            navigate("/Appointments");
          }}
          type="submit"
          variant="contained"
          sx={{ mt: 3, mb: 2 }}
        >
          Back
        </Button>
        <Paper
          sx={{
            borderRadius: "16px",
            position: "relative",
            color: "#fff",
            mb: 4,
            backgroundSize: "cover",
            backgroundRepeat: "no-repeat",
            backgroundPosition: "center",
            backgroundImage: `url(https://images.pexels.com/photos/162583/work-workplace-office-computer-162583.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1)`,
          }}
        >
          {
            <img
              style={{ display: "none" }}
              src="https://images.pexels.com/photos/162583/work-workplace-office-computer-162583.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
              alt="test"
            />
          }
          <Box
            sx={{
              position: "absolute",
              top: 0,
              bottom: 0,
              right: 0,
              left: 0,
              backgroundColor: "rgba(0,0,0,.3)",
            }}
          />
          <Grid container>
            <Grid item md={6}>
              <Box
                sx={{
                  position: "relative",
                  p: { xs: 3, md: 6 },
                  pr: { md: 0 },
                }}
              ></Box>
            </Grid>
          </Grid>
        </Paper>
        <Grid container spacing={5} sx={{ mt: 3 }}>
          <Grid item xs={12} md={8} sx={{ "& .markdown": { py: 3 } }}>
            <Typography
              component="h1"
              variant="h3"
              color="inherit"
              gutterBottom
            >
              {dayjs(appointment?.date).toString()}
            </Typography>
            <Typography variant="subtitle1" fontSize={20} paragraph>
              {appointment?.description}
            </Typography>
          </Grid>
          <Grid item xs={12} md={4}>
            <Paper elevation={0} sx={{ p: 2, bgcolor: "grey.200" }}>
              <Typography variant="h6" gutterBottom>
                Address
              </Typography>
              <Typography>{appointment?.address}</Typography>
              <Typography>
                {appointment?.isConfirmed
                  ? "Appointment was confirmed"
                  : "Appointment was not confirmed yet"}
              </Typography>
            </Paper>
          </Grid>
        </Grid>
        <Stack direction={"row"} spacing={2}>
          <Button
            onClick={navigateToTrainings}
            type="submit"
            variant="contained"
          >
            Assigned Training Plans
          </Button>
          <Button
            onClick={(): void =>
              navigate(`/Appointments/${appointmentId}/Edit`)
            }
            color="success"
            type="submit"
            variant="contained"
          >
            Edit
          </Button>
          <Button
            onClick={handleClickOpen}
            color="error"
            type="submit"
            variant="contained"
          >
            Delete
          </Button>
        </Stack>
        <Typography variant="subtitle1" fontSize={20} paragraph color={"green"}>
          {successMessage}
        </Typography>
        <Dialog
          open={open}
          onClose={handleClose}
          aria-labelledby="alert-dialog-title"
          aria-describedby="alert-dialog-description"
        >
          <DialogTitle id="alert-dialog-title">
            Ar you sure you want to remove appointment?
          </DialogTitle>
          <DialogContent>
            <DialogContentText id="alert-dialog-description">
              Deleting appointment will delete it's training plans and all eye
              exercises!
            </DialogContentText>
          </DialogContent>
          <DialogActions>
            <Button onClick={handleClose}>Cancel</Button>
            <Button
              onClick={(): void => {
                handleClose();
                deleteAppointment();
              }}
              autoFocus
            >
              Delete
            </Button>
          </DialogActions>
        </Dialog>
      </Container>
    </main>
  );
};
export default AppointmentInfo;
