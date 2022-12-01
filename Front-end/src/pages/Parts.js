import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import Axios from "axios";
import { Button, Card, CardContent, Typography, CardActionArea, CardMedia } from "@mui/material";
import MenuBar from "../MenuBar";
import { CircularProgress, Grid } from "@mui/material";
import { GetRole } from "../Auth";


const Parts = () => {
  const [parts, setParts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [role, setRole] = useState("user");
  const fetchParts = async () => {
    const token = localStorage.getItem("token");
    const url = "https://kompiuterija20221102215702.azurewebsites.net/parts";
    try {
      const { data } = await Axios.get(
        url, { headers: { "Authorization": `Bearer ${token}` } }
      )
      const parts = data;
      setParts(parts);
      setLoading(false);
    }
    catch (err) {
      setParts(0);
      setLoading(false);
    };
    
  };

  useEffect(() => {
    fetchParts();
    setRole(GetRole());
  }, []);
  if(loading) {
    return(
    <div>
      <MenuBar />
      <div className="center"><CircularProgress /></div>
    </div>
      
      );
  }
  if(parts === 0) {
    return (
      <div>
      <MenuBar />
      <div className="center">
      <Typography gutterBottom variant="h5" component="div">
                  No parts found
                </Typography>
      </div>
      </div>
    );
  }
  else return (
    <div>
      <MenuBar />
      <Grid container justifyContent="center" alignItems="center" style={{ paddingBottom: '2vh', paddingTop: '2vh' }} columnSpacing={2}>
          
          {parts.map((part) => (
            <Grid>
            <Card sx={{ maxWidth: 345 }} key={part.computerId}>
              <CardActionArea component={Link} to={"/computers/" + part.computerId}>
                <CardMedia
                  component="img"
                  height="300"
                  src={require("../public/"+part.type+".jpg")}
                  alt={"Part"}
                />
                <CardContent>
                  <Typography gutterBottom variant="h5" component="div">
                    {part.name}
                  </Typography>
                </CardContent>
              </CardActionArea>
              {(role === "employee" || role === "admin") &&
              <Button component={Link} to={"/parts/edit/"+part.id}>
                Edit
              </Button>}
              {(role === "employee" || role === "admin") &&
              <Button component={Link} to={"/parts/delete/"+part.id}>
                Remove
              </Button>}
            </Card>
            
            </Grid>
          ))}
      </Grid>
    </div>
    
  );
};

export default Parts;
