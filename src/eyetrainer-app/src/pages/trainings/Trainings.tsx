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
import dayjs from "dayjs";
import { useNavigate, useParams } from "react-router-dom";
import { EyeTrainingPlanResponse } from "../../contracts/trainings/EyeTrainingPlanResponse";
import TrainingService from "../../services/TrainingService";
import { CardMedia } from "@mui/material";

const Trainings: React.FC = (): ReactElement => {
  const [eyeTrainingPlans, setEyeTrainingPlans] = useState<
    EyeTrainingPlanResponse[]
  >(new Array<EyeTrainingPlanResponse>());

  const { appointmentId } = useParams();
  const navigate = useNavigate();

  async function fetchTrainings(): Promise<void> {
    const response = await TrainingService.getTrainings(Number(appointmentId));
    setEyeTrainingPlans(response);
  }

  useEffect((): void => {
    fetchTrainings();
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
            Eye Training Plans
          </Typography>
          <Typography
            variant="h5"
            align="center"
            color="text.secondary"
            paragraph
          >
            Here you can easily see what plans are assigned to specified
            appointment
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
              Create Eye Training Plan
            </Button>
          </Stack>
        </Container>
      </Box>
      <Container sx={{ py: 8 }} maxWidth="md">
        {/* End hero unit */}
        <Grid container spacing={4}>
          {eyeTrainingPlans.length > 0 &&
            eyeTrainingPlans.map(
              (trainingPlan): ReactElement => (
                <Grid item key={trainingPlan.id} xs={12} sm={6} md={4}>
                  <Card
                    sx={{
                      height: "100%",
                      display: "flex",
                      flexDirection: "column",
                    }}
                  >
                    <CardMedia
                      height="200"
                      component="img"
                      sx={{
                        16: 9,
                      }}
                      image={trainingPlan.imageLink}
                      alt="random"
                    />
                    <CardContent sx={{ flexGrow: 1 }}>
                      <Typography gutterBottom variant="h5" component="h2">
                        {dayjs(trainingPlan.startDate).toString() +
                          " - " +
                          dayjs(trainingPlan.endDate).toString()}
                      </Typography>
                      <Typography>{trainingPlan.description}</Typography>
                    </CardContent>
                    <CardActions>
                      <Button
                        size="small"
                        onClick={(): void => {
                          navigate(`/Appointments/${trainingPlan.id}`);
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

export default Trainings;
