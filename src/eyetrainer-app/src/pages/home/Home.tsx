import {
  Box,
  Button,
  Container,
  CssBaseline,
  Stack,
  Typography,
} from "@mui/material";
import React, { ReactElement } from "react";
import { useNavigate } from "react-router-dom";
import MovingEyes from "../../features/moving-eyes/MovingEyes";

const EyeExercises: React.FC = (): ReactElement => {
  const navigate = useNavigate();
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
            Welcome
          </Typography>
          <Typography
            variant="h5"
            align="center"
            color="text.secondary"
            paragraph
          >
            Hi There! Nice to see you here on Eye Training platform.
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
                navigate("/Appointments/1/Trainings");
              }}
            >
              Start training!
            </Button>
            <Button
              variant="outlined"
              onClick={(): void => {
                navigate("/Appointments/Create");
              }}
            >
              Register for an appointment
            </Button>
          </Stack>
        </Container>
        <MovingEyes />
      </Box>
    </main>
  );
};

export default EyeExercises;
