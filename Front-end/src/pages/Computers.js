import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import Axios from "axios";
import { Stack } from "@mui/system";
import { CardMedia, Card, CardContent, Typography, CardActionArea, CircularProgress, Grid, Button } from "@mui/material";
import MenuBar from "../MenuBar";
import { GetRole } from "../Auth";


const ShopComputers = () => {
  const [computers, setComputers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [role, setRole] = useState("user");
  const fetchComputers = async () => {
    const token = localStorage.getItem("token");
    const url = "https://kompiuterija20221102215702.azurewebsites.net/computers";
    const { data } = await Axios.get(
      url, { headers: { "Authorization": `Bearer ${token}` } }
    );
    const computers = data;
    setComputers(computers);
    console.log(computers);
    setLoading(false);
  };

  useEffect(() => {
    fetchComputers();
    setRole(GetRole());
  }, []);

  if (loading) {
    return (
      <div>
        <MenuBar />
        <div className="center"><CircularProgress /></div>
      </div>

    );
  }
  else return (
    <div>
      <MenuBar />
      <Grid container justifyContent="center" alignItems="center" style={{ paddingBottom: '2vh', paddingTop: '2vh' }} columnSpacing={2}>
        
          {computers.map((computer) => (
            <Grid>
            <Card sx={{ maxWidth: 345 }} key={computer.id}>
              <CardActionArea component={Link} to={"/computers/" + computer.id}>
                <CardMedia
                  component="img"
                  height="300"
                  src={require("../public/computer.jpg")}
                  alt={"Computers"}
                />
                <CardContent>
                  <Typography gutterBottom variant="h5" component="div">
                    {computer.name}
                  </Typography>
                </CardContent>
              </CardActionArea>
              {(role == "employee" || role == "admin") &&
              <Button>
                Edit
              </Button>}
              {(role == "admin") &&
              <Button>
                Remove
              </Button>}
            </Card>
            </Grid>
          ))}
        

      </Grid>
    </div>

  );
};

export default ShopComputers;
