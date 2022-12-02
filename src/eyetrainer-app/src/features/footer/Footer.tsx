import { Box, Link, Typography } from "@mui/material";
import React, { ReactElement } from "react";

const Copyright: React.FC = (): ReactElement => {
  return (
    <Typography variant="body2" color="text.secondary" align="center">
      {"Copyright © "}
      <Link color="inherit" href="http://localhost:3000/">
        EyeTrainerPortal
      </Link>{" "}
      {new Date().getFullYear()}
      {"."}
    </Typography>
  );
};

const Footer: React.FC = (): ReactElement => {
  return (
    <Box sx={{ bgcolor: "background.paper", p: 6 }} component="footer">
      <Typography variant="h6" align="center" gutterBottom>
        EyeTrainer
      </Typography>
      <Typography
        variant="subtitle1"
        align="center"
        color="text.secondary"
        component="p"
      >
        Powered by React.JS and MaterialUI
      </Typography>
      <Copyright />
    </Box>
  );
};

export default Footer;
