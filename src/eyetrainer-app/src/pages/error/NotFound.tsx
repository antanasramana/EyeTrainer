import React, { ReactElement } from "react";
import Container from "@mui/material/Container";
import Typography from "@mui/material/Typography";

const NotFound: React.FC = (): ReactElement => {
  return (
    <main>
      <Container maxWidth="lg">
        <Typography
          component="h1"
          variant="h3"
          color="inherit"
          gutterBottom
          sx={{ p: 4 }}
        >
          {"Page not found"}
        </Typography>
      </Container>
    </main>
  );
};

export default NotFound;
