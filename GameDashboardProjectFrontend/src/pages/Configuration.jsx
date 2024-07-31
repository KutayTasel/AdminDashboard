import React, { useEffect, useState } from "react";
import BuildingConfiguration from "../components/Configuration/BuildingConfiguration";

const Configuration = () => {
  const [someState, setSomeState] = useState(null);

  useEffect(() => {
    console.log("Configuration component mounted");
    setSomeState("Some value");
  }, []);

  return (
    <div>
      <BuildingConfiguration someProp={someState} />
    </div>
  );
};

export default Configuration;
