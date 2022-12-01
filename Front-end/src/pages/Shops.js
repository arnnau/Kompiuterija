import React, { useState, useEffect } from "react";
import Axios from "axios";
import { Stack } from "@mui/system";
import { Card, CardContent, Typography, CardActionArea, CardMedia } from "@mui/material";
import MenuBar from "../MenuBar";
import { Link } from "react-router-dom";

const Shops = () => {
  const [shops, setShops] = useState([]);

  const fetchShops = async () => {
    const token = localStorage.getItem("token");
    const { data } = await Axios.get(
      "https://kompiuterija20221102215702.azurewebsites.net/shops", { headers: { "Authorization": `Bearer ${token}` } }
    );
    const shops = data;
    setShops(shops);
    console.log(shops);
    console.log("../../public/" + shops[1].city + ".jpg");
  };

  useEffect(() => {
    fetchShops();
  }, []);

  return (
    <div>
    <MenuBar />
    <div className="center">
      <Stack spacing={2} direction="column">
        {shops.map((shop) => (
          <Card sx={{ maxWidth: 345 }} key={shop.id}>
            <CardActionArea component={Link} to={"/shops/"+shop.id}>
              <CardMedia
                component="img"
                height="140"
                src={require("../public/" + shop.city + ".jpg")}
                alt={shop.city}
              />
              <CardContent>
                <Typography gutterBottom variant="h5" component="div">
                  {shop.address}, {shop.city}
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

export default Shops;
