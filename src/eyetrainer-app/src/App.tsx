import React, { useEffect, ReactElement } from "react";
import { BrowserRouter, Routes, Route, Outlet } from "react-router-dom";
import Home from "./pages/home/Home";
import "./App.css";
import Exercises from "./pages/exercises/Exercises";
import Appointments from "./pages/appointments/Appointments";
import Trainings from "./pages/trainings/Trainings";
import SignUp from "./pages/signup/SignUp";
import SignIn from "./pages/signin/SignIn";
import { theme } from "./features/ThemeStyling";
import { ThemeProvider } from "@mui/material/styles";
import NavigationMenu from "./features/navigation-menu/NavigationMenu";
import Footer from "./features/footer/Footer";
import AuthenticatedRoute from "./features/custom-routes/AuthenticatedRoute";
import NotFound from "./pages/error/NotFound";
import AppointmentInfo from "./pages/appointments/AppointmentInfo";
import CreateAppointment from "./pages/appointments/CreateAppointment";
import EditAppointment from "./pages/appointments/EditAppointment";

const App: React.FC = (): ReactElement => {
  useEffect((): void => {
    document.title = "Eye Trainer Portal";
  }, []);

  return (
    <ThemeProvider theme={theme}>
      <BrowserRouter>
        <Routes>
          <Route element={<AuthenticatedRoute />}>
            <Route
              index
              element={
                <>
                  <NavigationMenu />
                  <Home />
                  <Footer />
                </>
              }
            />
            <Route
              element={
                <>
                  <NavigationMenu />
                  <Outlet />
                  <Footer />
                </>
              }
              path="/Appointments"
            >
              <Route index element={<Appointments />} />
              <Route path="Create" element={<CreateAppointment />} />
              <Route path=":appointmentId">
                <Route index element={<AppointmentInfo />} />
                <Route path="Edit" element={<EditAppointment />} />
                <Route path="Trainings">
                  <Route index element={<Trainings />} />
                </Route>
              </Route>
            </Route>
            <Route>
              <Route
                element={
                  <>
                    <NavigationMenu />
                    <Exercises />
                    <Footer />
                  </>
                }
                path="/Exercises"
              />
            </Route>
          </Route>
          <Route
            element={
              <>
                <SignUp />
                <Footer />
              </>
            }
            path="/SignUp"
          />
          <Route
            element={
              <>
                <SignIn />
                <Footer />
              </>
            }
            path="/SignIn"
          />
          <Route
            path="*"
            element={
              <>
                <NavigationMenu />
                <NotFound />
                <Footer />
              </>
            }
          />
        </Routes>
      </BrowserRouter>
    </ThemeProvider>
  );
};

export default App;
