import React, { ReactElement } from "react";
import "./MovingEyes.css";

const App: React.FC = (): ReactElement => {
  return (
    <div className="eyes-container">
      <div className="eye">
        <div className="upper-pupil"></div>
        <div className="iris"></div>
        <div className="lower-pupil"></div>
      </div>
      <div className="eye">
        <div className="upper-pupil"></div>
        <div className="iris"></div>
        <div className="lower-pupil"></div>
      </div>
    </div>
  );
};

export default App;
