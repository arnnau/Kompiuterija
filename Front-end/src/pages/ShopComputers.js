import React, { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";
import Axios from "axios";
import { Stack } from "@mui/system";
import { Button, Card, CardContent, Typography, CardActionArea, CardMedia } from "@mui/material";
import MenuBar from "../MenuBar";


const ShopComputers = () => {
  const [computers, setComputers] = useState([]);
  let { shopId } = useParams();
  const fetchComputers = async () => {
    const token = localStorage.getItem("token");
    const url = "https://kompiuterija20221102215702.azurewebsites.net/shops/" + shopId + "/computers";
    const { data } = await Axios.get(
      url, { headers: { "Authorization": `Bearer ${token}` } }
    );
    const computers = data;
    setComputers(computers);
    console.log(computers);
  };

  useEffect(() => {
    fetchComputers();
  }, []);

  return (
    <div>
    <MenuBar />
    <div className="center">
      <Stack spacing={2} direction="column">
        {computers.map((computer) => (
          <Card sx={{ maxWidth: 345 }} key={computer.id}>
            <CardActionArea component={Link} to={"/computers/" + computer.id}>
              <CardContent>
                <Typography gutterBottom variant="h5" component="div">
                  {computer.name}
                </Typography>
              </CardContent>
            </CardActionArea>
            
          </Card>
        ))}
      </Stack>

    </div>
    </div>
  );
};

export default ShopComputers;
