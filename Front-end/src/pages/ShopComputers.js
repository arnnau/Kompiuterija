import React, { useState, useEffect } from "react";
import { Link, useParams } from "react-router-dom";
import Axios from "axios";
import { CardMedia, Card, CardContent, Typography, CardActionArea, CircularProgress, Grid, Button } from "@mui/material";
import MenuBar from "../MenuBar";
import { GetRole } from "../Auth";


const ShopComputers = () => {
  let { shopId } = useParams();
  const [computers, setComputers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [role, setRole] = useState("user");
  const fetchComputers = async () => {
    const token = localStorage.getItem("token");
    const url = "https://kompiuterija20221102215702.azurewebsites.net/shops/" + shopId + "/computers";
    try {
      const { data } = await Axios.get(
      url, { headers: { "Authorization": `Bearer ${token}` } })
      
      const computers = data;
      setComputers(computers);
      console.log(computers);
      setLoading(false);
    }
    catch (err) {
      setComputers([]);
      setLoading(false);
    }
    
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
  if(computers.length === 0) {
    return (
      <div>
      <MenuBar />
      <Grid container justifyContent="center" alignItems="center" style={{ paddingTop: '2vh' }}><Button component={Link} to={"/computers/create"}>New computer...</Button></Grid>
      <div className="center">
      <Typography gutterBottom variant="h5" component="div">
                  No computers found
                </Typography>
      </div>
      </div>
    );
  }
  else return (
    <div>
      <MenuBar />
      <Grid container justifyContent="center" alignItems="center" style={{ paddingTop: '2vh' }}><Button component={Link} to='/computers/create'>New computer...</Button></Grid>
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
              {(role === "employee" || role === "admin") &&
              <Button component={Link} to={"/computers/edit/"+computer.id}>
                Edit
              </Button>}
              {(role === "admin") &&
              <Button component={Link} to={"/computers/delete/"+computer.id}>
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
