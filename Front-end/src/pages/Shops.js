import React, { useState, useEffect } from "react";
import Axios from "axios";
import { Stack } from "@mui/system";
import { Card, CardContent, Typography, CardActionArea, CardMedia, Grid } from "@mui/material";
import MenuBar from "../MenuBar";
import { Link } from "react-router-dom";
import { CircularProgress } from "@mui/material";

const Shops = () => {
  const [shops, setShops] = useState([]);
  const [loading, setLoading] = useState(true);
  const fetchShops = async () => {
    const token = localStorage.getItem("token");
    const { data } = await Axios.get(
      "https://kompiuterija20221102215702.azurewebsites.net/shops", { headers: { "Authorization": `Bearer ${token}` } }
    );
    const shops = data;
    setShops(shops);
    setLoading(false);
  };

  useEffect(() => {
    fetchShops();
  }, []);
  if (loading) {
    return (
      <div>
        <MenuBar />
        <div className="center"><CircularProgress /></div>
      </div>

    );
  }
  return (
    <div>
    <MenuBar />
    <Grid container justifyContent = "center" alignItems="center" style={{ minHeight: '90vh' }}>
      <Stack spacing={2} direction="row">
        {shops.map((shop) => (
          <Card sx={{ maxWidth: 345 }} key={shop.id}>
            <CardActionArea component={Link} to={"/shops/"+shop.id}>
              <CardMedia
                component="img"
                height="300"
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

    </Grid>
    </div>
  );
};

export default Shops;
